using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using CrimeAndWin.Saga.Data;
using CrimeAndWin.Saga.StateMachines;
using CrimeAndWin.Saga.States;

var builder = WebApplication.CreateBuilder(args);

// Serilog Config
builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(context.Configuration);
});

// DbContext
builder.Services.AddDbContext<SagaDbContext>(opt =>
{
    //opt.UseSqlServer(builder.Configuration.GetConnectionString("PlayerProfileConnection"));
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SagaDb"));
});

// MassTransit Config
builder.Services.AddMassTransit(x =>
{
    // Configure Entity Framework Outbox
    x.AddEntityFrameworkOutbox<SagaDbContext>(o =>
    {
        o.UseSqlServer();
        o.UseBusOutbox();
    });

    // Register State Machines
    x.AddSagaStateMachine<CrimeRewardStateMachine, CrimeRewardState>()
        .EntityFrameworkRepository(r =>
        {
            r.ExistingDbContext<SagaDbContext>();
            r.UseSqlServer();
        });

    x.AddSagaStateMachine<CrimeActionStateMachine, CrimeActionState>()
        .EntityFrameworkRepository(r =>
        {
            r.ExistingDbContext<SagaDbContext>();
            r.UseSqlServer();
        });

    x.AddSagaStateMachine<PurchaseStateMachine, PurchaseState>()
        .EntityFrameworkRepository(r =>
        {
            r.ExistingDbContext<SagaDbContext>();
            r.UseSqlServer();
        });

    x.AddSagaStateMachine<RankUpdateStateMachine, RankUpdateState>()
        .EntityFrameworkRepository(r =>
        {
            r.ExistingDbContext<SagaDbContext>();
            r.UseSqlServer();
        });

    // Transport configuration
    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitMqSettings = builder.Configuration.GetSection("RabbitMq");
        //cfg.Host(rabbitMqSettings["Host"] ?? "localhost", rabbitMqSettings["VirtualHost"] ?? "/", h =>
        //{
        //    h.Username(rabbitMqSettings["Username"] ?? "guest");
        //    h.Password(rabbitMqSettings["Password"] ?? "guest");
        //});

        // Setup endpoints automatically based on registered State Machines
        cfg.ConfigureEndpoints(context);
    });
});

// Add controllers
builder.Services.AddControllers();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");

app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();


