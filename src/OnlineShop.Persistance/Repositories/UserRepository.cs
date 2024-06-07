using OnlineShop.Application.Abstractions.Data;
using OnlineShop.Domain.Abstractions.Maybe;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Repositories;
using OnlineShop.Domain.ValueObjects;
using OnlineShop.Persistance.Specifications;

namespace OnlineShop.Persistance.Repositories;

/// <summary>
/// Represents the user repository.
/// </summary>
internal sealed class UserRepository : GenericRepository<User>, IUserRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public UserRepository(IDbContext dbContext)
        : base(dbContext)
    {
    }

    /// <inheritdoc />
    public async Task<Maybe<User>> GetByEmailAsync(Email email) => await FirstOrDefaultAsync(new UserWithEmailSpecification(email));

    /// <inheritdoc />
    public async Task<bool> IsEmailUniqueAsync(Email email) => !await AnyAsync(new UserWithEmailSpecification(email));
}