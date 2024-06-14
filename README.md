### Migrations
```bash
cd ./RunningApp/
Dotnet ef migrations add init --context ApplicationDbContext
Dotnet ef database update --context ApplicationDbContext
dotnet ef migrations add InitialCreate --context AuthDbContext
dotnet ef database update --context AuthDbContext     
```
