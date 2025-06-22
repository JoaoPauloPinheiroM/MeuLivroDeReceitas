using Moq;
using MyRecipeBook.Domain.Repositories.User;

namespace CommonTestUltilities.Repositories;

public class UserWriteOnlyRepositorBuilder
{
    public static IUserWriteOnlyRepository Build ()
    {
        var mock = new Mock<IUserWriteOnlyRepository>();
        return mock.Object;
    }
}