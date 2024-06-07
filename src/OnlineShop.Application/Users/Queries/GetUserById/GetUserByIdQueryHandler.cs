using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Abstractions.Authentication;
using OnlineShop.Application.Abstractions.Data;
using OnlineShop.Application.Abstractions.Messaging;
using OnlineShop.Contracts.Users;
using OnlineShop.Domain.Abstractions.Maybe;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Users.Queries.GetUserById;

/// <summary>
/// Represents the <see cref="GetUserByIdQuery"/> handler.
/// </summary>
internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, Maybe<UserResponse>>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider;
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="userIdentifierProvider">The user identifier provider.</param>
    /// <param name="dbContext">The database context.</param>
    public GetUserByIdQueryHandler(IUserIdentifierProvider userIdentifierProvider, IDbContext dbContext)
    {
        _userIdentifierProvider = userIdentifierProvider;
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<Maybe<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.UserId == Guid.Empty || request.UserId != _userIdentifierProvider.UserId)
        {
            return Maybe<UserResponse>.None;
        }

        UserResponse response = await _dbContext.Set<User>()
            .Where(x => x.Id == request.UserId)
            .Select(user => new UserResponse
            {
                Id = user.Id,
                Email = user.Email.Value,
                FullName = user.FirstName.Value + " " + user.LastName.Value,
                FirstName = user.FirstName.Value,
                LastName = user.LastName.Value,
                CreatedOnUtc = user.CreatedOnUtc
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (response is null)
        {
            return Maybe<UserResponse>.None;
        }

        return response;
    }
}