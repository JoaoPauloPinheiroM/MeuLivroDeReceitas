using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
{
    private readonly MyRecipeBookDbContext _dbcontext;

    public UserRepository ( MyRecipeBookDbContext dbcontext ) => _dbcontext = dbcontext;

    public async Task Add ( User user ) => await _dbcontext.Users.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail ( string email ) => await _dbcontext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);
}