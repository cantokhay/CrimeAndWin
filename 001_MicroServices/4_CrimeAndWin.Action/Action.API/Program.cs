using Shared.Application.Abstractions.Messaging;
using Shared.Infrastructure;
using CrimeAndWin.Action.GameMechanics;
using CrimeAndWin.Action.BackgroundServices;
using Action.Application;
using Action.Application.Abstract;
using Action.Infrastructure.Services;
using Action.Application.Mapping;
using Action.Infrastructure.Messaging;
using Action.Infrastructure.Persistance.Context;
using Action.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;
using Shared.Domain.Time;
using Shared.Infrastructure.Filters;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<ActionDbContext>(opt =>
{
    //opt.UseSqlServer(builder.Configuration.GetConnectionString("ActionConnection"));
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ActionLaptopConnection"));
});

// MediatR & Mapperly & Validation
builder.Services.AddScoped<IMediator, Shared.Application.Abstractions.Messaging.Mediator>();
builder.Services.AddRequestHandlers(typeof(IApplicationAssemblyMarker).Assembly);
builder.Services.AddScoped<ActionMapper>();
builder.Services.AddSharedValidation(typeof(IApplicationAssemblyMarker).Assembly);

//DI Registrations
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
builder.Services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

//RabbitMQ & MassTransit
builder.Services.AddScoped<IEventPublisher, MassTransitEventPublisher>();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbit = builder.Configuration.GetSection("Rabbit");

        cfg.Host(rabbit["Host"], rabbit["VirtualHost"], h =>
        {
            h.Username(rabbit["User"]);
            h.Password(rabbit["Pass"]);
        });

        cfg.ConfigureEndpoints(context);
    });
});

// builder.Services.AddScoped<IValidator<PlayerActionAttemptDTO>, PerformPlayerActionAttemptCommandValidator>();
// builder.Services.AddScoped<IValidator<CreateActionDefinitionDTO>, CreateActionDefinitionCommandValidator>();

// With this line:
builder.Services.AddScoped<IGameSettingsService, GameSettingsService>();
builder.Services.AddScoped<IPlayerProfileService, PlayerProfileService>();
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<GlobalExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddHostedService<EnergyRefillBackgroundService>();
builder.Services.AddScoped<SuccessRateCalculator>();

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


