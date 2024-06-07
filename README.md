### Migrations
```bash
cd ./RunningApp/
Dotnet ef migrations add InitDb --context ApplicationDbContext
Dotnet ef database update --context ApplicationDbContext
dotnet ef migrations add InitIdentityDb --context AuthDbContext
dotnet ef database update --context AuthDbContext     
```
