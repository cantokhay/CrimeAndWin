using MassTransit;
using Microsoft.EntityFrameworkCore;
using PlayerProfile.Application;
using PlayerProfile.Application.Mapping;
using PlayerProfile.Infrastructure.Persistance.Context;
using PlayerProfile.Infrastructure.Repositories;
using Shared.Domain.Repository;
using Shared.Domain.Time;
using Shared.Infrastructure;
using Shared.Infrastructure.Filters;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<PlayerProfileDbContext>(opt =>
{
    //opt.UseSqlServer(builder.Configuration.GetConnectionString("PlayerProfileConnection"));
    opt.UseSqlServer(builder.Configuration.GetConnectionString("PlayerProfileLaptopConnection"));
});

//DI Registrations
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
builder.Services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

//MediatR & Mapperly & Validation
builder.Services.AddMediator((Mediator.MediatorOptions options) =>
{
    options.ServiceLifetime = ServiceLifetime.Scoped;
});
builder.Services.AddScoped<PlayerProfileMapper>();
builder.Services.AddSharedValidation(typeof(IApplicationAssemblyMarker).Assembly);

// builder.Services.AddScoped<IValidator<CreatePlayerCommand>, CreatePlayerCommandValidator>();

// MassTransit Config
builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<PlayerProfile.API.Consumers.UpdatePlayerStatsCommandConsumer>();

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

