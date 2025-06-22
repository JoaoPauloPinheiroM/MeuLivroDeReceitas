using AutoMapper;
using MyRecipeBook.Application.Services.AutoMapper;

namespace CommonTestUltilities.Mapper;

public class MapperBuilder
{
    public static IMapper Build ()
    {
        return new MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper();
    }
}