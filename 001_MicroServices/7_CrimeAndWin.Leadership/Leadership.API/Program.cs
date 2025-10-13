using FluentValidation;
using Leadership.Application;
using Leadership.Application.DTOs.LeaderboardDTOs;
using Leadership.Application.DTOs.LeaderboardEntryDTOs;
using Leadership.Application.Mapping;
using Leadership.Application.Messaging.Consumers;
using Leadership.Application.ValidationRules.LeaderboardEntryValidations;
using Leadership.Application.ValidationRules.LeaderboardValidations;
using Leadership.Infrastructure.Persistance.Context;
using Leadership.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;
using Shared.Domain.Time;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<LeadershipDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("LeadershipConnection"));
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
builder.Services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();

//FluentValidation
builder.Services.AddScoped<IValidator<CreateLeaderboardDTO>, CreateLeaderboardDTOValidator>();
builder.Services.AddScoped<IValidator<CreateLeaderboardEntryDTO>, CreateLeaderboardEntryDTOValidator>();

////RabbitMQ & MassTransit
//builder.Services.AddMassTransit(x =>
//{
//    x.AddConsumer<ActionPerformedConsumer>();
//    x.UsingRabbitMq((ctx, cfg) =>
//    {
//        cfg.Host("rabbitmq", "/", h => { /* credentials */ });
//        cfg.ReceiveEndpoint("leadership.action-performed", e =>
//        {
//            e.ConfigureConsumer<ActionPerformedConsumer>(ctx);
//        });
//    });

//    x.AddConsumer<PlayerBannedConsumer>();
//    x.UsingRabbitMq((ctx, cfg) =>
//    {
//        cfg.Host("rabbitmq", "/", h => { /* credentials */ });
//        cfg.ReceiveEndpoint("leadership.player-banned", e =>
//        {
//            e.ConfigureConsumer<PlayerBannedConsumer>(ctx);
//        });
//    });
//});

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
