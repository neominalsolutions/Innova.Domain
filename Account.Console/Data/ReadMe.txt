
Migration Klasörünü silip yeniden migration alabiliriz


Add-Migration BankInit -context BankContext -o "Data/Migrations"
Update-Database -context BankContext

Add-Migration OrderInit -context OrderContext -o "Data/Migrations"
Update-Database -context OrderContext
