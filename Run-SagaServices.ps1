$projects = @(
    "001_MicroServices\10_CrimeAndWin.Saga\CrimeAndWin.Saga.csproj",
    "001_MicroServices\4_CrimeAndWin.Action\Action.API\Action.API.csproj",
    "001_MicroServices\5_CrimeAndWin.Economy\Economy.API\Economy.API.csproj",
    "001_MicroServices\6_CrimeAndWin.Inventory\Inventory.API\Inventory.API.csproj",
    "001_MicroServices\2_CrimeAndWin.PlayerProfile\PlayerProfile.API\PlayerProfile.API.csproj"
)

Write-Host "RabbitMQ veya Docker'in acik olduguna emin olun..." -ForegroundColor Yellow
Start-Sleep -Seconds 2

foreach ($proj in $projects) {
    $title = $proj.Split("\")[-1]
    Write-Host "Baslatiliyor: $title" -ForegroundColor Cyan
    # Windows'ta her projeyi kendi terminal (command prompt) penceresinde açar, loglari izlemek kolaylasir.
    Start-Process wt.exe -ArgumentList "new-tab -p `"Windows PowerShell`" -d . dotnet run --project `"$proj`"" -ErrorAction Ignore
    if (!$?) {
        Start-Process cmd -ArgumentList "/k dotnet run --project `"$proj`""
    }
}

Write-Host "Tüm servisler baslatma emrini aldi. Saga testine baslayabilirsiniz!" -ForegroundColor Green
