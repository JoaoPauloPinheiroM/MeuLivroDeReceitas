using CommonTestUltilities.Cryptography;
using CommonTestUltilities.Mapper;
using CommonTestUltilities.Repositories;
using CommonTestUltilities.Requests;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace UseCases.Test.Use.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Sucess ()
    {
        var request = RequestsRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase();
        var result = await useCase.Execute(request);
        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
    }

    [Fact]
    public async Task Error_Email_Already_Registered ()
    {
        var request = RequestsRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase(request.Email);
        Func<Task> act = async () => await useCase.Execute(request);
        //ele deveria lançar de forma async um exception.
        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorsMessages.Count == 1 && e.ErrorsMessages.Contains(ResourceMessagesException.EMAIL_ALREADY_REGISTRED));
    }

    [Fact]
    public async Task Error_Name_Empyt ()
    {
        var request = RequestsRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;
        var useCase = CreateUseCase();
        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorsMessages.Count == 1 && e.ErrorsMessages.Contains(ResourceMessagesException.NAME_EMPTY));
    }

    //usei para ser um método utilitário para as builds e instanciaçoes necess. vai ser melhorado ao longo do desenvolvimento
    private static RegisterUserUseCase CreateUseCase ( string? email = null )
    {
        var mapper = MapperBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var writeRepository = UserWriteOnlyRepositorBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
        if (string.IsNullOrEmpty(email).IsFalse())
        {
            readRepositoryBuilder.ExistActiveUserWithEmail(email!);
        }
        return new RegisterUserUseCase(writeRepository , readRepositoryBuilder.Build() , mapper , passwordEncripter , unitOfWork);
    }
}