using Microsoft.Extensions.Configuration;

namespace MyRecipeBook.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static string ConnectionString ( this IConfiguration configuration )
    {
        return configuration.GetConnectionString("Connection")!;
    }

    public static bool IsUniTest ( this IConfiguration configuration )
    {
        return configuration.GetValue<bool>("InMemoryTest");
    }
}