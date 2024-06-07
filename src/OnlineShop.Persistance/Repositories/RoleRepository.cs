using OnlineShop.Application.Abstractions.Data;
using OnlineShop.Domain.Abstractions.Maybe;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Repositories;
using OnlineShop.Domain.ValueObjects;
using OnlineShop.Persistance.Specifications;

namespace OnlineShop.Persistance.Repositories;

internal sealed class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public RoleRepository(IDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Maybe<Role>> GetByNameAsync(Name name) =>
        await FirstOrDefaultAsync(new RoleNameSpecification(name));

    public async Task<bool> IsRoleExist(Name name) =>
        await AnyAsync(new RoleNameSpecification(name));
}
