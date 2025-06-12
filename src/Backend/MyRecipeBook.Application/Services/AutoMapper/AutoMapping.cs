using AutoMapper;
using MyRecipeBook.Communication.Requests;
using System.Runtime.InteropServices;

namespace MyRecipeBook.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping ()
    {
        RequestToDomain();
    }

    //Fiz isso para configurar as resquisições do Domain
    private void RequestToDomain ()
    {
        CreateMap<RequestsRegisterUserJson , Domain.Entities.User>()
            .ForMember(dest => dest.Password , opt => opt.Ignore());
    }
}