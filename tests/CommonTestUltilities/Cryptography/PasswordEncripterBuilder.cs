using MyRecipeBook.Application.Services.Cryptography;

namespace CommonTestUltilities.Cryptography;

public class PasswordEncripterBuilder
{
    public static PasswordEncripter Build ()
    {
        return new PasswordEncripter("ABC1234");
    }
}