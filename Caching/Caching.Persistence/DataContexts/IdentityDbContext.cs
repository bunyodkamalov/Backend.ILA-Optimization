using Caching.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Caching.Persistence.DataContexts;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<User> Users => Set<User>();
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }
}