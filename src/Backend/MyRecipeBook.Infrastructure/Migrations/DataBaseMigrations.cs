using Dapper;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace MyRecipeBook.Infrastructure.Migrations;

public static class DataBaseMigrations
{
    public static void Migrate ( string connectionString , IServiceProvider serviceProvider )
    {
        EnsureDataBaseCreated(connectionString);

        MigrationsDatabase(serviceProvider);
    }

    private static void EnsureDataBaseCreated ( string connectionString )
    {
        var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        var dataBaseName = connectionStringBuilder.InitialCatalog;

        connectionStringBuilder.Remove("Initial Catalog");

        using var dbConnection = new SqlConnection(connectionStringBuilder.ConnectionString);

        var parameters = new DynamicParameters();

        parameters.Add("name" , dataBaseName);

        var records = dbConnection.Query("SELECT name FROM sys.databases WHERE name = @name" , parameters);
        if (!records.Any())
        {
            dbConnection.Execute($"CREATE DATABASE [{dataBaseName}]");
        }
    }

    private static void MigrationsDatabase ( IServiceProvider serviceProvider )
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.ListMigrations();
        runner.MigrateUp();
    }
}