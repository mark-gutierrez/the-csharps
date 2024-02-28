# the-csharps

## Starting SQL Server

```powershell
$sa_password = "Pass@word123"
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -e "MSSQL_PID=Evaluation" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d --rm --name mssql mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04
```

## Setting the connection string to secret manager

```powershell
$sa_password = "Pass@word123"
dotnet user-secrets set "ConnectionStrings:GameStoreContext" "Server=localhost; Database=GameStore; User Id=sa; Password=$sa_password;TrustServerCertificate=True"
```
