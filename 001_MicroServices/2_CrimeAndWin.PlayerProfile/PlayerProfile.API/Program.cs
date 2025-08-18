using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PlayerProfile.Application;
using PlayerProfile.Application.Features.Player.Commands.CreatePlayer;
using PlayerProfile.Application.Mapping;
using PlayerProfile.Application.ValidationRules.PlayerValidations;
using PlayerProfile.Infrastructure.Persistance.Context;
using PlayerProfile.Infrastructure.Repositories;
using Shared.Domain.Repository;
using Shared.Domain.Time;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<PlayerProfileDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("PlayerProfileConnection"));
});

//DI Registrations
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IApplicationAssemblyMarker).Assembly));
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new GeneralMapping());
});
builder.Services.AddScoped<IValidator<CreatePlayerCommand>, CreatePlayerCommandValidator>();
builder.Services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

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
