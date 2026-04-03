using Leadership.Application;
using Leadership.Application.Mapping;
using Leadership.Infrastructure.Persistance.Context;
using Leadership.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;
using Shared.Domain.Time;
using Shared.Infrastructure;
using Shared.Infrastructure.Filters;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<LeadershipDbContext>(opt =>
{
    //opt.UseSqlServer(builder.Configuration.GetConnectionString("LeadershipConnection"));
    opt.UseSqlServer(builder.Configuration.GetConnectionString("LeadershipLaptopConnection"));
});

// MediatR & Mapperly & Validation
builder.Services.AddMediator((Mediator.MediatorOptions options) =>
{
    options.ServiceLifetime = ServiceLifetime.Scoped;
});
builder.Services.AddScoped<LeadershipMapper>();
builder.Services.AddSharedValidation(typeof(IApplicationAssemblyMarker).Assembly);

//DI Registrations
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
builder.Services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();

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

