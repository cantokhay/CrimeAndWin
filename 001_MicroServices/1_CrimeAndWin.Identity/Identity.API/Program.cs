using Identity.Application;
using Identity.Application.Auth;
using Identity.Application.Auth.Abstract;
using Identity.Infrastructure.Persistence.Context;
using Identity.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Domain.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<IdentityDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
});

// MediatR
builder.Services.AddMediatR(cfg =>
{
cfg.RegisterServicesFromAssembly(typeof(IApplicationAssemblyMarker).Assembly);
    });

//JWT
var jwtSection = builder.Configuration.GetSection("Jwt");
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["SigningKey"]!));
var issuer = jwtSection["Issuer"];
var audience = jwtSection["Audience"];
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

//DI Registrations
builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

// Swagger + JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Identity API", Version = "v1" });

    var jwtScheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Bearer ile giriþ yapýn. Sadece token'ý girin (eyJ...), 'Bearer ' yazmayýn."
    };
    c.AddSecurityDefinition("Bearer", jwtScheme);
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        [jwtScheme] = Array.Empty<string>()
    });
});


//Default
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Authentication
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.SaveToken = true;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(2), // test için
            NameClaimType = "unique_name",
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        };

        opt.IncludeErrorDetails = true;
        opt.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = ctx =>
            {
                Console.WriteLine($"[JWT FAIL] {ctx.Exception.GetType().Name}: {ctx.Exception.Message}");
                return Task.CompletedTask;
            },
            OnChallenge = ctx =>
            {
                Console.WriteLine($"[JWT CHALLENGE] Error={ctx.Error}, Desc={ctx.ErrorDescription}");
                return Task.CompletedTask;
            },
            OnTokenValidated = ctx =>
            {
                Console.WriteLine("[JWT OK] " + ctx.Principal?.Identity?.Name);
                return Task.CompletedTask;
            }
        };
    });

        // (Opsiyonel) SignalR / WebSocket senaryolarýnda header yerine query’den alma:
        // opt.Events = new JwtBearerEvents
        // {
        //     OnMessageReceived = ctx =>
        //     {
        //         var accessToken = ctx.Request.Query["access_token"];
        //         var path = ctx.HttpContext.Request.Path;
        //         if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs/game"))
        //             ctx.Token = accessToken;
        //         return Task.CompletedTask;
        //     }
        // };

// Authorization (rol politikalarý)
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("PlayerOnly", p => p.RequireRole("Player"));
    opt.AddPolicy("ModeratorOnly", p => p.RequireRole("Moderator"));
    opt.AddPolicy("AdminOnly", p => p.RequireRole("Admin")); // “Oyun Yöneticisi”
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();   // <— ÖNCE
app.UseAuthorization();    // <— SONRA

app.MapControllers();

app.Run();

//comment to push
