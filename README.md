## QAccess - Condominium Management

Web application developed for condominium management. **[Under development]**

## Configuring environment

This application was developed in .NET Core, using Entity Framework and Razor Page.

### Configuring dependencies:

Installing dependencies: 

```csharp
dotnet tool install --global dotnet-ef
dotnet tool install --global dotnet-aspnet-codegenerator
```

Adding dependencies to csproj.

```csharp
dotnet add package Pomelo.EntityFrameworkCore.Mysql 
dotnet add package Pomelo.EntityFrameworkCore.MySql.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.VisualStudio.Web.CodeGenerators.Mvc
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
```

### Configuring database:

In this project we use MySQL as the database. Make sure you have it running on your machine. Otherwise, you can upload an instance to Docker for use with the command below:

```docker
docker run -p 3306:3306 --name mysql-database -e MYSQL_ROOT_PASSWORD={PASSWORD} -d mysql
```

| Environment Variables | Description |
| --- | --- |
| `MYSQL_ROOT_PASSWORD` | Password for the database root user. |

Access your database and create a database called `qaccess`

Having an instance of the database running, we need to configure it in the [appsettings.json](./appsettings.json) file.

Change a `DefaultDatabas`e property according to your database access.

```json
 "ConnectionStrings": {
    "DefaultDatabase": "server=localhost; database=qaccess; user=root; password=4?lm@ei?D&A",
    "QaccessContext": "Server=(localdb)\\mssqllocaldb;Database=;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
```

| Environment Variables | Description |
| --- | --- |
| `server` | Database execution address.  |
| `database` | Name of the previously created database..  |
| `user` | User to access the database.  |
| `password` | User password..  |

### Running the application:

Before running the application, run the dotnet command below to create the tables in our database

```csharp
dotnet-ef migrations add CreateTables
```

Make the build:

```csharp
dotnet build
```

Run the application:

```csharp
dotnet run
```

Access:

`localhost:5000`

The application will be running on port 5000. You can change this by changing the property in the [launchSettings.json](./Properties/launchSettings.json) file
