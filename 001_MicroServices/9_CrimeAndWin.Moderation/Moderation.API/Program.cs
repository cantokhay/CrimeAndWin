using Shared.Application.Abstractions.Messaging;
using Shared.Infrastructure;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Moderation.Application;
using Moderation.Application.Mapping;
using Moderation.Application.Messaging.Abstract;
using Moderation.Application.Messaging.Concrete;
using Moderation.Infrastructure.Persistance.Context;
using Moderation.Infrastructure.Repositories;
using Shared.Domain.Repository;
using Shared.Domain.Time;
using Shared.Infrastructure.Filters;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<ModerationDbContext>(opt =>
{
    //opt.UseSqlServer(builder.Configuration.GetConnectionString("ModerationConnection"));
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ModerationLaptopConnection"));
});

// MediatR & Mapperly & Validation
builder.Services.AddScoped<IMediator, Shared.Application.Abstractions.Messaging.Mediator>();
builder.Services.AddRequestHandlers(typeof(IApplicationAssemblyMarker).Assembly);
builder.Services.AddScoped<ModerationMapper>();
builder.Services.AddSharedValidation(typeof(IApplicationAssemblyMarker).Assembly);

//// FluentValidation
//builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddValidatorsFromAssembly(typeof(GeneralMapping).Assembly);



//DI Registrations
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
builder.Services.AddScoped<IEventPublisher, MassTransitEventPublisher>();
builder.Services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();

// MassTransit & RabbitMQ
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
    });
});

// Controllers + Filters
// builder.Services.AddControllers();
builder.Services.AddControllers(o => o.Filters.Add(new GlobalExceptionFilter()));
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

