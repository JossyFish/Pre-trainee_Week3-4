Используется MS SQL LocalDB
1. Вставить в appsettings.json в строку "LibraryContext" строку с подключением "Server=(localdb)\\mssqllocaldb;Database=LibraryDB;Trusted_Connection=true;TrustServerCertificate=true;" - ТОЛЬКО ЭТУ!!!
2. Если не установлен, скачать Microsoft.EntityFrameworkCore.
БД создастся при запуске программы, хотя и можно создать самим перед запуском - 1. Add-Migration Initial. 2. Update-Database.
