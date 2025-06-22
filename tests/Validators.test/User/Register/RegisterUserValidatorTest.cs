using CommonTestUltilities.Requests;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exceptions;

namespace Validators.test.User.Register;

public class RegisterUserValidatorTest
{
    [Fact]
    public void Sucess ()
    {
        var validator = new RegisterUserValidator();
        var request = RequestsRegisterUserJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Name_Empy ()
    {
        var validator = new RegisterUserValidator();
        var request = RequestsRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;
        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e =>
        e.ErrorMessage.Equals(ResourceMessagesException.NAME_EMPTY));
    }

    [Fact]
    public void Error_Email_Empy ()
    {
        var validator = new RegisterUserValidator();
        var request = RequestsRegisterUserJsonBuilder.Build();
        request.Email = string.Empty;
        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e =>
        e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_EMPTY));
    }

    [Fact]
    public void Error_Email_Invalid ()
    {
        var validator = new RegisterUserValidator();
        var request = RequestsRegisterUserJsonBuilder.Build();
        request.Email = "email.com";
        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e =>
        e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_INVALID));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Error_Password_Invalid ( int passwordLength )
    {
        var validator = new RegisterUserValidator();
        var request = RequestsRegisterUserJsonBuilder.Build(passwordLength);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e =>
        e.ErrorMessage.Equals(ResourceMessagesException.PASSWORD_EMPTY));
    }
}