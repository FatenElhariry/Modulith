param(
    [Parameter(Mandatory = $true)]
    [string]$MigrationName
)

$projectPath = ".\EShop.Catalog.csproj"

if (-not (Test-Path $projectPath)) {
    Write-Error "Project file '$projectPath' not found. Please run this script from the Catalog project directory."
    exit 1
}

dotnet ef migrations add $MigrationName --project $projectPath --startup-project API --output-dir "Data\Migrations" --context CatalogDbContext