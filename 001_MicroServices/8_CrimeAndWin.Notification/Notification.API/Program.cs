using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Notification.Application;
using Notification.Application.Features.Notification.Commands.CreateNotification;
using Notification.Application.Mapping;
using Notification.Application.ValidationRules.NotificationValidations;
using Notification.Infrastructure.Persistance.Context;
using Notification.Infrastructure.Repositories;
using Shared.Domain.Repository;
using Shared.Domain.Time;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<NotificationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("NotificationConnection"));
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
builder.Services.AddScoped<IValidator<CreateNotificationCommand>, CreateNotificationValidator>();

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
