var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("IdentityApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:6001/api/IdentityAdmins/");
});

builder.Services.AddHttpClient("PlayerProfileApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:6002/api/PlayerAdmins/");
});

builder.Services.AddHttpClient("GameWorldApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:6003/api/");
    // GameWorldsController => /api/GameWorlds
    // SeasonsController   => /api/Seasons
});

builder.Services.AddHttpClient("ActionApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:6004/api/");
    // ActionDefinitionsController => /api/ActionDefinitionAdmins
    // ActionAttemptsController    => /api/ActionAttemptAdmins
});

builder.Services.AddHttpClient("EconomyApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:6005/api/EconomyAdmins/");
});

builder.Services.AddHttpClient("InventoryApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:6006/api/InventoryAdmins/");
});

builder.Services.AddHttpClient("LeadershipApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:6007/api/LeadershipAdmins/");
});

builder.Services.AddHttpClient("NotificationApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:6008/api/NotificationAdmins/");
});

builder.Services.AddHttpClient("ModerationApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:6009/api/ModerationAdmins/");
});

// Typed HttpClients for Adım 1c
builder.Services.AddHttpClient<Administration.MVC.Services.ActionApiClient>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ServiceEndpoints:Action"]!));

builder.Services.AddHttpClient<Administration.MVC.Services.SagaApiClient>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ServiceEndpoints:Saga"]!));

builder.Services.AddHttpClient<Administration.MVC.Services.InventoryApiClient>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ServiceEndpoints:Inventory"]!));

builder.Services.AddHttpClient<Administration.MVC.Services.HealthApiClient>(); // BaseAddress yok

builder.Services.AddHttpClient<Administration.MVC.Services.GatewayApiClient>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ServiceEndpoints:Gateway"]!));

// SSL Bypass for Development (Adım 1d)
if (builder.Environment.IsDevelopment())
{
    builder.Services.ConfigureHttpClientDefaults(b =>
        b.ConfigurePrimaryHttpMessageHandler(() =>
            new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            }));
}

// Add services to the container.
builder.Services.AddControllersWithViews(opt => {
    opt.Filters.Add<Administration.MVC.Filters.AdminExceptionFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AdminGameWorld}/{action=GameWorlds}");

app.Run();
