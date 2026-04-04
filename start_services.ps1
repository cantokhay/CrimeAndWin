$projects = @(
    "001_MicroServices\1_CrimeAndWin.Identity\Identity.API\Identity.API.csproj",
    "001_MicroServices\2_CrimeAndWin.PlayerProfile\PlayerProfile.API\PlayerProfile.API.csproj",
    "001_MicroServices\3_CrimeAndWin.GameWorld\GameWorld.API\GameWorld.API.csproj",
    "001_MicroServices\4_CrimeAndWin.Action\Action.API\Action.API.csproj",
    "001_MicroServices\5_CrimeAndWin.Economy\Economy.API\Economy.API.csproj",
    "001_MicroServices\6_CrimeAndWin.Inventory\Inventory.API\Inventory.API.csproj",
    "001_MicroServices\7_CrimeAndWin.Leadership\Leadership.API\Leadership.API.csproj",
    "001_MicroServices\8_CrimeAndWin.Notification\Notification.API\Notification.API.csproj",
    "001_MicroServices\9_CrimeAndWin.Moderation\Moderation.API\Moderation.API.csproj",
    "001_MicroServices\10_CrimeAndWin.Saga\CrimeAndWin.Saga.csproj",
    "0_Gateway\CrimeAndWin.Gateway\CrimeAndWin.Gateway.csproj",
    "002_Frontends\1_CrimeAndWin.Administration\Administration.MVC\Administration.MVC.csproj"
)

foreach ($project in $projects) {
    Write-Host "Starting $project..."
    Start-Process dotnet "run --project $project --no-build" -WindowStyle Hidden
}
