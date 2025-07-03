Add-Migration Intitial -Context OrderingDbContext -StartupProject API -OutputDir "Data/Migrations" -Project Eshop.Ordering

Remove-Migration -Context BasketDbContext -Project EShop.Basket

Update-Database -Context BasketDbContext -Project EShop.Basket