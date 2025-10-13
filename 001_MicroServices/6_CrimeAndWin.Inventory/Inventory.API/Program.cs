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

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<InventoryDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("InventoryConnection"));
});

// MediatR & AutoMapper
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IApplicationAssemblyMarker).Assembly));
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new GeneralMapping());
});

//DI Registrations
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
builder.Services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
builder.Services.AddScoped<IEventPublisher, EventPublisher>();

//FluentValidation
builder.Services.AddScoped<IValidator<AddItemCommand>, AddItemCommandValidator>();

//RabbitMQ & MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ActionPerformedIntegrationEventConsumer>();
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h => { /* credentials */ });
        cfg.ReceiveEndpoint("inventory.action-performed", e =>
        {
            e.ConfigureConsumer<ActionPerformedIntegrationEventConsumer>(ctx);
        });
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
