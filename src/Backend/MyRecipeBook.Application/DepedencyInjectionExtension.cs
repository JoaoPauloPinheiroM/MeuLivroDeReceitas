using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Application.UseCases.User.Register;
using Microsoft.Extensions.Configuration;

namespace MyRecipeBook.Application;

//Vai cuidar da injeçao de dependência dos serviços da aplicação
public static class DepedencyInjectionExtension
{
    public static void AddApplication ( this IServiceCollection services , IConfiguration configuration )
    {
        AddUseCases(services);
        AddAutoMapper(services);
        AddPasswordEncrypter(services , configuration);
    }

    private static void AddAutoMapper ( IServiceCollection services )
    {
        services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper());
    }

    private static void AddUseCases ( IServiceCollection services )
    {
        services.AddScoped<IRegisterUserUseCase , RegisterUserUseCase>();
    }

    private static void AddPasswordEncrypter ( IServiceCollection services , IConfiguration configuration )
    {
        //usei o getvalue para caso mudar o tipo de valor do campo Password
        var keyAdditional = configuration.GetValue<string>("Settings:Password:AdditionalKey");
        services.AddScoped(option => new PasswordEncripter(keyAdditional!));
    }
}