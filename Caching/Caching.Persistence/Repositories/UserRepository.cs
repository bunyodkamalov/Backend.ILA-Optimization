using System.Linq.Expressions;
using Caching.Domain.Common.Caching;
using Caching.Domain.Common.Query;
using Caching.Domain.Entities;
using Caching.Persistence.Caching;
using Caching.Persistence.DataContexts;
using Caching.Persistence.Repositories.Interfaces;

namespace Caching.Persistence.Repositories;

public class UserRepository(IdentityDbContext dbContext, ICacheBroker cacheBroker) : EntityRepositoryBase<User, IdentityDbContext>(dbContext, cacheBroker, new CacheEntryOptions()), IUserRepository
{
    public new IQueryable<User> Get(Expression<Func<User, bool>>? predicate, bool asNoTracking = false) => 
        base.Get(predicate, asNoTracking);


    public ValueTask<IList<User>> GetAsync(QuerySpecification<User> querySpecification, bool asNoTracking = false,
        CancellationToken cancellationToken = default) => 
        base.GetAsync(querySpecification, asNoTracking, cancellationToken);

    public new ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false, CancellationToken cancellationToken = default) =>
        base.GetByIdAsync(userId, asNoTracking, cancellationToken);


    public new ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default) =>
        base.CreateAsync(user, saveChanges, cancellationToken);


    public new ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default) =>
        base.UpdateAsync(user, saveChanges, cancellationToken);


    public new ValueTask<User?> DeleteByIdAsync(Guid userId, bool saveChanges = true, CancellationToken cancellationToken = default) =>
        base.DeleteByIdAsync(userId, saveChanges, cancellationToken);

}