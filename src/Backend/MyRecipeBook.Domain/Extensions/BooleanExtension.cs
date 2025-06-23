using System.Runtime.CompilerServices;

namespace MyRecipeBook.Domain.Extensions;

public static class BooleanExtension
{
    public static bool IsFalse ( this bool value ) => !value;
}