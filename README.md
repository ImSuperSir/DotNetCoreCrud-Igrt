# ASP.NET CORE 8 WebApi- Clean Architecture, CRUD, Sqlite

Packages
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.AspNetCore.Identity.EntityFrameworkCore

Microsoft.EntityFrameworkCore.Tools   -- Migrations
Microsoft.EntityFrameworkCore.Sqlite or SqlServer

##Secrets
IdentitySettings:DataBaseAdminPassword
    Pass@word1

Docker:sqlInstance 
docker run -e "ACCEPT-EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -p 1433:1433 -d -v sqlvolume:/var/opt/mssql --rm --name mySqlIgrt mcr.microsoft.com/mssql/server:2022-latest


To create database identity tables

path: src

Paso 1
dotnet ef migrations add AddIdeintity -p .\TasksApi.Infrastructure\TasksApi.Infrastructure.csproj -s .\TaskApi.Api\TaskApi.Api.csproj

Paso 2
dotnet ef database update -p .\TasksApi.Infrastructure\TasksApi.Infrastructure.csproj -s .\TaskApi.Api\TaskApi.Api.csproj