using System.Diagnostics.CodeAnalysis;

namespace MyRecipeBook.Domain.Extensions;

public static class StringExtensions
{
    //vai garantir que não vai ser nulo e se ele retornar true, o compilador não reclama que vai ser nulo.
    public static bool NotEmpty ( [NotNullWhen(true)] this string? value ) => string.IsNullOrWhiteSpace(value).IsFalse();
}