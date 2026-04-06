using Shared.Application.Abstractions.Messaging;
using Shared.Infrastructure;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Economy.Application;
using Economy.Application.Mapping;
using Economy.Infrastructure.Persistance.Context;
using Economy.Infrastructure.Repositories;
using Economy.API.Sagas;
using Shared.Domain.Repository;
using Shared.Domain.Time;
using Shared.Infrastructure.Filters;
using Economy.API.Consumers;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<EconomyDbContext>(opt =>
{
    //opt.UseSqlServer(builder.Configuration.GetConnectionString("EconomyConnection"));
    opt.UseSqlServer(builder.Configuration.GetConnectionString("EconomyLaptopConnection"));
});

//DI Registrations
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
builder.Services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

//MediatR & Mapperly & Validation
builder.Services.AddScoped<IMediator, Shared.Application.Abstractions.Messaging.Mediator>();
builder.Services.AddRequestHandlers(typeof(IApplicationAssemblyMarker).Assembly);
builder.Services.AddScoped<EconomyMapper>();
builder.Services.AddSharedValidation(typeof(IApplicationAssemblyMarker).Assembly);

// MassTransit Config
builder.Services.AddMassTransit(x =>
{
    // Register Laundering Saga
    x.AddSagaStateMachine<LaunderingStateMachine, LaunderingSagaState>()
        .InMemoryRepository();

    x.AddConsumersFromNamespaceContaining<DeductMoneyCommandConsumer>(); // Fix: Use existing consumer

    x.UsingRabbitMq((ctx, cfg) =>
    {
        var rabbit = builder.Configuration.GetSection("Rabbit");
        cfg.Host(rabbit["Host"] ?? "localhost", rabbit["VirtualHost"] ?? "/", h =>
        {
            h.Username(rabbit["User"] ?? "guest");
            h.Password(rabbit["Pass"] ?? "guest");
        });

        cfg.ConfigureEndpoints(ctx);
    });
});

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<GlobalExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }

