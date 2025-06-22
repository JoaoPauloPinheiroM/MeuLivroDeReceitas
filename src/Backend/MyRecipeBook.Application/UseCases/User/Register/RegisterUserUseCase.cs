using AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnitOfWork _uniOfWork;
    private readonly IMapper _mapper;
    private readonly PasswordEncripter _passwordEncripter;

    //Injection de dependências dos repositórios
    public RegisterUserUseCase (
        IUserWriteOnlyRepository userWriteOnlyRepository ,
        IUserReadOnlyRepository userReadOnlyRepository ,
        IMapper mapper ,
        PasswordEncripter passwordEncripter ,
        IUnitOfWork uniOfWork )
    {
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _uniOfWork = uniOfWork;
    }

    public async Task<ResponseRegisteredUserJson> Execute ( RequestsRegisterUserJson request )
    {
        await Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);

        user.Password = _passwordEncripter.Encrypt(request.Password);

        await _userWriteOnlyRepository.Add(user);
        await _uniOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name
        };
    }

    //Função de utilidade para poder validar dados de input emails, senha e nome.
    private async Task Validate ( RequestsRegisterUserJson request )
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        var emailExiste = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
        if (emailExiste)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty , ResourceMessagesException.EMAIL_ALREADY_REGISTRED));
        }

        if (result.IsValid == false)
        {
            var erroMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(erroMessages);
        }
    }
}