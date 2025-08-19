using Action.Application;
using Action.Application.Abstract;
using Action.Application.DTOs;
using Action.Application.Mapping;
using Action.Application.ValidationRules.ActionDefinitionValidations;
using Action.Application.ValidationRules.PlayerActionValidations;
using Action.Infrastructure.Messaging;
using Action.Infrastructure.Persistance.Context;
using Action.Infrastructure.Repositories;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<ActionDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ActionConnection"));
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

// FluentValidation
builder.Services.AddScoped<IValidator<PlayerActionAttemptDTO>, PerformPlayerActionAttemptCommandValidator>();
builder.Services.AddScoped<IValidator<CreateActionDefinitionDTO>, CreateActionDefinitionCommandValidator>();

// With this line:
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
