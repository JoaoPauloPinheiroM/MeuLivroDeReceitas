using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;
using MyRecipeBook.Infrastructure.Extensions;
using System.Reflection;

namespace MyRecipeBook.Infrastructure;

public static class DepedencyInjectionExtension
{
    //extension method for IServiceCollection
    public static void AddInfrastructure ( this IServiceCollection services , IConfiguration configuration )
    {
        AddRepositories(services);
        if (configuration.IsUniTest())
            return;

        AddDbContext_SqlServices(services , configuration);
        AddFluentMrigator_SqlServer(services , configuration);
    }

    private static void AddDbContext_SqlServices ( IServiceCollection services , IConfiguration configuration )
    {
        var connectionString = configuration.ConnectionString();

        services.AddDbContext<MyRecipeBookDbContext>(
            options =>
            {
                options.UseSqlServer(connectionString);
            });
    }

    private static void AddRepositories ( IServiceCollection services )
    {
        services.AddScoped<IUnitOfWork , UnitOfWork>();
        services.AddScoped<IUserWriteOnlyRepository , UserRepository>();
        services.AddScoped<IUserReadOnlyRepository , UserRepository>();
    }

    private static void AddFluentMrigator_SqlServer ( IServiceCollection services , IConfiguration configuration )
    {
        var connectionString = configuration.ConnectionString();
        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            options.AddSqlServer()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(Assembly.Load("MyRecipeBook.Infrastructure")).For.All();
        });
    }
}