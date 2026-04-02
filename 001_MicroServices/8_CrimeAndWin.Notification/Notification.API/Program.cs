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
using Shared.Infrastructure;
using Shared.Infrastructure.Filters;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<NotificationDbContext>(opt =>
{
    //opt.UseSqlServer(builder.Configuration.GetConnectionString("NotificationConnection"));
    opt.UseSqlServer(builder.Configuration.GetConnectionString("NotificationLaptopConnection"));
});

// MediatR & Mapperly & Validation
builder.Services.AddMediator();
builder.Services.AddScoped<NotificationMapper>();
builder.Services.AddSharedValidation(typeof(IApplicationAssemblyMarker).Assembly);

//DI Registrations
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
builder.Services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();

// builder.Services.AddScoped<IValidator<CreateNotificationCommand>, CreateNotificationValidator>();

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

