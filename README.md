### Migrations
```bash
cd ./RunningApp/
dotnet ef migrations add InitIdentityDb --context AuthDbContext
dotnet ef database update --context AuthDbContext     
```
