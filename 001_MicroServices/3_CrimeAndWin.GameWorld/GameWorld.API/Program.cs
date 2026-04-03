using Shared.Application.Abstractions.Messaging;
using Shared.Infrastructure;
using GameWorld.Application;
using GameWorld.Application.Abstract;
using GameWorld.Application.Mapping;
using GameWorld.Infrastructure.Messaging;
using GameWorld.Infrastructure.Persistance.Context;
using GameWorld.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;
using Shared.Domain.Time;
using Shared.Infrastructure;
using Shared.Infrastructure.Filters;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<GameWorldDbContext>(opt =>
{
    //opt.UseSqlServer(builder.Configuration.GetConnectionString("GameWorldConnection"));
    opt.UseSqlServer(builder.Configuration.GetConnectionString("GameWorldLaptopConnection"));
});

// MediatR & Mapperly & Validation
builder.Services.AddScoped<IMediator, Mediator>();
builder.Services.AddRequestHandlers(typeof(IApplicationAssemblyMarker).Assembly);
builder.Services.AddScoped<GameWorldMapper>();
builder.Services.AddSharedValidation(typeof(IApplicationAssemblyMarker).Assembly);

//DI Registrations
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
builder.Services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
builder.Services.AddSingleton<IEventBus, EventBusStub>();

// builder.Services.AddScoped<IValidator<CreateGameWorldCommand>, CreateGameWorldValidator>();
// builder.Services.AddScoped<IValidator<UpdateGameWorldCommand>, UpdateGameWorldValidator>();
// builder.Services.AddScoped<IValidator<CreateSeasonCommand>, CreateSeasonValidator>();
// builder.Services.AddScoped<IValidator<UpdateSeasonCommand>, UpdateSeasonValidator>();

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

