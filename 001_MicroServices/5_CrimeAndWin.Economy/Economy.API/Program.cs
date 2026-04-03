using MassTransit;
using Economy.Application.Mapping;
using Economy.Infrastructure.Persistance.Context;
using Economy.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;
using Shared.Domain.Time;
using Economy.Application;
using Shared.Infrastructure;
using Shared.Infrastructure.Filters;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<EconomyDbContext>(opt =>
{
    //opt.UseSqlServer(builder.Configuration.GetConnectionString("EconomyConnection"));
    opt.UseSqlServer(builder.Configuration.GetConnectionString("EconomyLaptopConnection"));
});

// MediatR & Mapperly & Validation
builder.Services.AddMediator((Mediator.MediatorOptions options) =>
{
    options.ServiceLifetime = ServiceLifetime.Scoped;
});
builder.Services.AddScoped<EconomyMapper>();
builder.Services.AddSharedValidation(typeof(IApplicationAssemblyMarker).Assembly);

//DI Registrations
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
builder.Services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();

// builder.Services.AddScoped<IValidator<DepositMoneyCommand>, DepositMoneyValidator>();
// builder.Services.AddScoped<IValidator<WithdrawMoneyCommand>, WithdrawMoneyValidator>();

// MassTransit setup
builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<Economy.API.Consumers.RewardMoneyCommandConsumer>();

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

