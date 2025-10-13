using MassTransit;
using Microsoft.EntityFrameworkCore;
using Moderation.Application;
using Moderation.Application.Mappings;
using Moderation.Application.Messaging.Abstract;
using Moderation.Application.Messaging.Concrete;
using Moderation.Infrastructure.Persistance.Context;
using Moderation.Infrastructure.Repositories;
using Shared.Domain.Repository;
using Shared.Domain.Time;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<ModerationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ModerationConnection"));
});

// MediatR & AutoMapper
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IApplicationAssemblyMarker).Assembly));
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new GeneralMapping());
});

//// FluentValidation
//builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddValidatorsFromAssembly(typeof(GeneralMapping).Assembly);



//DI Registrations
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
builder.Services.AddScoped<IEventPublisher, MassTransitEventPublisher>();
builder.Services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();

// MassTransit & RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbit = builder.Configuration.GetSection("Rabbit");
        cfg.Host(rabbit["Host"], rabbit["VirtualHost"], h =>
        {
            h.Username(rabbit["User"]);
            h.Password(rabbit["Pass"]);
        });
    });
});

// Controllers + Filters
builder.Services.AddControllers(o => o.Filters.Add(new Moderation.API.Filters.ApiExceptionFilter()));
//builder.Services.AddControllers();
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
