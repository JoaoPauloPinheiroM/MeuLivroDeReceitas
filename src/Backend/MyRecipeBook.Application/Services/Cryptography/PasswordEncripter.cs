using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services.Cryptography;

public class PasswordEncripter
{
    private readonly string _additionalKey;

    public PasswordEncripter ( string additionalKey ) => _additionalKey = additionalKey;

    public string Encrypt ( string password )
    {
        //Fiz uma camda extra de hash para aumentar a segurança da senha.

        var newpassword = $"{password}{_additionalKey}";

        var bytes = Encoding.UTF8.GetBytes(newpassword);

        var hashBytes = SHA512.HashData(bytes);

        return StringBytes(hashBytes);
    }

    private static string StringBytes ( byte [] bytes )
    {
        var sb = new StringBuilder();
        foreach (var b in bytes)
        {
            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
    }
}