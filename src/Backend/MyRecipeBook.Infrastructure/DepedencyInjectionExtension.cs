using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;

namespace MyRecipeBook.Infrastructure;

public static class DepedencyInjectionExtension
{
    //extension method for IServiceCollection
    public static void AddInfrastructure ( this IServiceCollection services , IConfiguration configuration )
    {
        AddDbContext_SqlServices(services , configuration);
        AddRepositories(services);
    }

    private static void AddDbContext_SqlServices ( IServiceCollection services , IConfiguration configuration )
    {
        var connectionString = configuration.GetConnectionString("Connection");

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
}