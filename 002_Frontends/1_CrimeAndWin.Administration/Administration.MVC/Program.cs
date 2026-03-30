var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("IdentityApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:6101/api/IdentityAdmins/");
});

builder.Services.AddHttpClient("PlayerProfileApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:6102/api/PlayerAdmins/");
});

builder.Services.AddHttpClient("GameWorldApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:6103/api/");
    // GameWorldsController => /api/GameWorlds
    // SeasonsController   => /api/Seasons
});

builder.Services.AddHttpClient("ActionApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:6104/api/");
    // ActionDefinitionsController => /api/ActionDefinitionAdmins
    // ActionAttemptsController    => /api/ActionAttemptAdmins
});

builder.Services.AddHttpClient("EconomyApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:6105/api/EconomyAdmins/");
});

builder.Services.AddHttpClient("InventoryApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:6106/api/InventoryAdmins/");
});

builder.Services.AddHttpClient("LeadershipApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:6107/api/LeadershipAdmins/");
});

builder.Services.AddHttpClient("NotificationApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:6108/api/NotificationAdmins/");
});

builder.Services.AddHttpClient("ModerationApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:6109/api/ModerationAdmins/");
});

// Add services to the container.
builder.Services.AddControllersWithViews();

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
