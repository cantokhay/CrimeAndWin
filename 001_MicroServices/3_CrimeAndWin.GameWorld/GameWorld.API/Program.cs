using MassTransit;
using Shared.Application.Abstractions.Messaging;
using Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;
using GameWorld.Application;
using GameWorld.Application.Mapping;
using GameWorld.Infrastructure.Persistance.Context;
using GameWorld.Infrastructure.Repositories;
using GameWorld.API.Sagas;
using Shared.Domain.Repository;
using Shared.Domain.Time;
using Shared.Infrastructure.Filters;
using GameWorld.Application.Abstract;
using GameWorld.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<GameWorldDbContext>(opt =>
{
    //opt.UseSqlServer(builder.Configuration.GetConnectionString("GameWorldConnection"));
    opt.UseSqlServer(builder.Configuration.GetConnectionString("GameWorldLaptopConnection"));
});

//DI Registrations
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
builder.Services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
builder.Services.AddScoped<IEventBus, EventBusStub>();

//MediatR & Mapperly & Validation
builder.Services.AddScoped<IMediator, Shared.Application.Abstractions.Messaging.Mediator>();
builder.Services.AddRequestHandlers(typeof(IApplicationAssemblyMarker).Assembly);
builder.Services.AddScoped<GameWorldMapper>();
builder.Services.AddSharedValidation(typeof(IApplicationAssemblyMarker).Assembly);

// MassTransit Config
builder.Services.AddMassTransit(x =>
{
    // Auction Saga
    x.AddSagaStateMachine<AuctionStateMachine, AuctionSagaState>()
        .InMemoryRepository();

    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbit = builder.Configuration.GetSection("Rabbit");
        cfg.Host(rabbit["Host"] ?? "localhost", rabbit["VirtualHost"] ?? "/", h =>
        {
            h.Username(rabbit["User"] ?? "guest");
            h.Password(rabbit["Pass"] ?? "guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<GlobalExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");

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
