using Economy.Application.Features.Wallet.Commands.DepositMoney;
using MassTransit;
using Economy.Application.Features.Wallet.Commands.WithdrawMoney;
using Economy.Application.Mapping;
using Economy.Application.ValidationRules.WalletValidations;
using Economy.Infrastructure.Persistance.Context;
using Economy.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;
using Shared.Domain.Time;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<EconomyDbContext>(opt =>
{
    //opt.UseSqlServer(builder.Configuration.GetConnectionString("EconomyConnection"));
    opt.UseSqlServer(builder.Configuration.GetConnectionString("EconomyLaptopConnection"));
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
builder.Services.AddScoped<IValidator<DepositMoneyCommand>, DepositMoneyValidator>();
builder.Services.AddScoped<IValidator<WithdrawMoneyCommand>, WithdrawMoneyValidator>();

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
