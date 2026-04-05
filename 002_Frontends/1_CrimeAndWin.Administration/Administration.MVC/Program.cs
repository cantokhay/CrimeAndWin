var builder = WebApplication.CreateBuilder(args);

// Named clients for controllers
builder.Services.AddHttpClient("IdentityApi", client =>
{
    client.BaseAddress = new Uri($"{builder.Configuration["ServiceEndpoints:Identity"]?.TrimEnd('/')}/api/IdentityAdmins/");
});

builder.Services.AddHttpClient("PlayerProfileApi", client =>
{
    client.BaseAddress = new Uri($"{builder.Configuration["ServiceEndpoints:Player"]?.TrimEnd('/')}/api/PlayerAdmins/");
});

builder.Services.AddHttpClient("GameWorldApi", client =>
{
    client.BaseAddress = new Uri($"{builder.Configuration["ServiceEndpoints:GameWorld"]?.TrimEnd('/')}/api/");
});

builder.Services.AddHttpClient("ActionApi", client =>
{
    client.BaseAddress = new Uri($"{builder.Configuration["ServiceEndpoints:Action"]?.TrimEnd('/')}/api/");
});

builder.Services.AddHttpClient("EconomyApi", client =>
{
    client.BaseAddress = new Uri($"{builder.Configuration["ServiceEndpoints:Economy"]?.TrimEnd('/')}/api/EconomyAdmins/");
});

builder.Services.AddHttpClient("InventoryApi", client =>
{
    client.BaseAddress = new Uri($"{builder.Configuration["ServiceEndpoints:Inventory"]?.TrimEnd('/')}/api/InventoryAdmins/");
});

builder.Services.AddHttpClient("LeadershipApi", client =>
{
    client.BaseAddress = new Uri($"{builder.Configuration["ServiceEndpoints:Leadership"]?.TrimEnd('/')}/api/LeadershipAdmins/");
});

builder.Services.AddHttpClient("NotificationApi", client =>
{
    client.BaseAddress = new Uri($"{builder.Configuration["ServiceEndpoints:Notification"]?.TrimEnd('/')}/api/NotificationAdmins/");
});

builder.Services.AddHttpClient("ModerationApi", client =>
{
    client.BaseAddress = new Uri($"{builder.Configuration["ServiceEndpoints:Moderation"]?.TrimEnd('/')}/api/ModerationAdmins/");
});

builder.Services.AddHttpClient("EnergyApi", client =>
{
    client.BaseAddress = new Uri($"{builder.Configuration["ServiceEndpoints:Action"]?.TrimEnd('/')}/api/action/admin/");
});

builder.Services.AddHttpClient("CooldownApi", client =>
{
    client.BaseAddress = new Uri($"{builder.Configuration["ServiceEndpoints:Action"]?.TrimEnd('/')}/api/action/admin/");
});

builder.Services.AddHttpClient("ActionLogApi", client =>
{
    client.BaseAddress = new Uri($"{builder.Configuration["ServiceEndpoints:Action"]?.TrimEnd('/')}/api/action/admin/");
});

builder.Services.AddHttpClient("SystemApi", client =>
{
    client.BaseAddress = new Uri($"{builder.Configuration["ServiceEndpoints:Action"]?.TrimEnd('/')}/api/admin/settings/");
});

builder.Services.AddHttpClient("HealthApi", client =>
{
    client.BaseAddress = new Uri($"{builder.Configuration["ServiceEndpoints:Action"]?.TrimEnd('/')}/api/action/admin/");
});

builder.Services.AddHttpClient("SagaApi", client =>
{
    client.BaseAddress = new Uri($"{builder.Configuration["ServiceEndpoints:Saga"]?.TrimEnd('/')}/api/saga/admin/");
});

// Typed HttpClients
builder.Services.AddHttpClient<Administration.MVC.Services.ActionApiClient>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ServiceEndpoints:Action"]?.TrimEnd('/') + "/"));

builder.Services.AddHttpClient<Administration.MVC.Services.SagaApiClient>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ServiceEndpoints:Saga"]?.TrimEnd('/') + "/"));

builder.Services.AddHttpClient<Administration.MVC.Services.InventoryApiClient>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ServiceEndpoints:Inventory"]?.TrimEnd('/') + "/"));

builder.Services.AddHttpClient<Administration.MVC.Services.HealthApiClient>(c => 
    c.BaseAddress = new Uri(builder.Configuration["ServiceEndpoints:Gateway"]?.TrimEnd('/') + "/")); // Use gateway or any host

builder.Services.AddHttpClient<Administration.MVC.Services.GatewayApiClient>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ServiceEndpoints:Gateway"]?.TrimEnd('/') + "/"));

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
