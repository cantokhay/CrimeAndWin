using FluentValidation;
using Inventory.Application;
using Inventory.Application.Features.Item.Commands.AddItem;
using Inventory.Application.Mapping;
using Inventory.Application.Messaging.Abstract;
using Inventory.Application.Messaging.Concrete;
using Inventory.Application.ValidationRules.ItemValidations;
using Inventory.Infrastructure.Persistance.Context;
using Inventory.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;
using Shared.Domain.Time;
using Shared.Infrastructure;
using Shared.Infrastructure.Filters;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<InventoryDbContext>(opt =>
{
    //opt.UseSqlServer(builder.Configuration.GetConnectionString("InventoryConnection"));
    opt.UseSqlServer(builder.Configuration.GetConnectionString("InventoryLaptopConnection"));
});

// MediatR & Mapperly & Validation
builder.Services.AddMediator();
builder.Services.AddScoped<InventoryMapper>();
builder.Services.AddSharedValidation(typeof(IApplicationAssemblyMarker).Assembly);

//DI Registrations
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
builder.Services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
builder.Services.AddScoped<IEventPublisher, EventPublisher>();

// builder.Services.AddScoped<IValidator<AddItemCommand>, AddItemCommandValidator>();

//RabbitMQ & MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<Inventory.API.Consumers.GrantItemCommandConsumer>();
    x.AddConsumer<ActionPerformedIntegrationEventConsumer>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        var rabbit = builder.Configuration.GetSection("Rabbit");
        cfg.Host(rabbit["Host"] ?? "localhost", rabbit["VirtualHost"] ?? "/", h =>
        {
            h.Username(rabbit["User"] ?? "guest");
            h.Password(rabbit["Pass"] ?? "guest");
        });

        // Retain original explicit endpoint if needed
        cfg.ReceiveEndpoint("inventory.action-performed", e =>
        {
            e.ConfigureConsumer<ActionPerformedIntegrationEventConsumer>(ctx);
        });

        // Default mass transit endpoint auto setup
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

