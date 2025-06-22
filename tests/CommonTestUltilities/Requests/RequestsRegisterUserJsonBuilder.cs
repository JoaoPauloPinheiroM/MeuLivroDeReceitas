using Bogus;
using MyRecipeBook.Communication.Requests;

namespace CommonTestUltilities.Requests;

public class RequestsRegisterUserJsonBuilder
{
    public static RequestsRegisterUserJson Build ( int passwordlength = 10 )
    {
        return new Faker<RequestsRegisterUserJson>()
            .RuleFor(user => user.Name , ( f ) => f.Person.FirstName)
            .RuleFor(user => user.Email , ( f , user ) => f.Internet.Email(user.Name))
            .RuleFor(user => user.Password , ( f ) => f.Internet.Password(passwordlength));
    }
}