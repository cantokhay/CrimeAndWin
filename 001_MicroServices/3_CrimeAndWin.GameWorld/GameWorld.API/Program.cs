using FluentValidation;
using GameWorld.Application;
using GameWorld.Application.Abstract;
using GameWorld.Application.Features.GameWorld.Commands.CreateGameWorld;
using GameWorld.Application.Features.GameWorld.Commands.UpdateGameWorld;
using GameWorld.Application.Features.Season.Commands.CreateSeason;
using GameWorld.Application.Mapping;
using GameWorld.Application.ValidationRules.GameWorldValidations;
using GameWorld.Application.ValidationRules.SeasonValidations;
using GameWorld.Infrastructure.Messaging;
using GameWorld.Infrastructure.Persistance.Context;
using GameWorld.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<GameWorldDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("GameWorldConnection"));
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
builder.Services.AddSingleton<IEventBus, EventBusStub>();

// FluentValidation
builder.Services.AddScoped<IValidator<CreateGameWorldCommand>, CreateGameWorldValidator>();
builder.Services.AddScoped<IValidator<UpdateGameWorldCommand>, UpdateGameWorldValidator>();
builder.Services.AddScoped<IValidator<CreateSeasonCommand>, CreateSeasonValidator>();

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
