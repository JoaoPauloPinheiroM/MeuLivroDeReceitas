using System.Globalization;

namespace MyRecipeBook.API.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    //Fiz esse construtor para fazer a injeção de dependência do RequestDelegate para poder permitir que o middleware seja encadeado na pipeline de requisições do ASP.NET Core.
    public CultureMiddleware ( RequestDelegate next )
    {
        _next = next;
    }

    public async Task Invoke ( HttpContext context )
    {
        var supportedLangues = CultureInfo.GetCultures(CultureTypes.AllCultures);

        var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();
        var cultureInfo = new CultureInfo("en");

        if (string.IsNullOrWhiteSpace(requestedCulture) == false &&
            supportedLangues.Any(c => c.Name.Equals(requestedCulture , StringComparison.OrdinalIgnoreCase)))
        {
            cultureInfo = new CultureInfo(requestedCulture);
        }

        CultureInfo.CurrentCulture = cultureInfo;

        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context); // Chama o próximo middleware na pipeline de requisições
    }
}