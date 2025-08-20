using Economy.Application;
using Economy.Application.Features.Wallet.Commands;
using Economy.Application.Mapping;
using Economy.Application.ValidationRules.WalletValidations;
using Economy.Infrastructure.Persistance.Context;
using Economy.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<EconomyDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("EconomyConnection"));
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

//FluentValidation
builder.Services.AddScoped<IValidator<DepositMoneyCommand>, DepositMoneyValidator>();
builder.Services.AddScoped<IValidator<WithdrawMoneyCommand>, WithdrawMoneyValidator>();

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
