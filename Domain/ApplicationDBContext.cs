using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public class ApplicationDBContext : DbContext, IApplicationDBContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

    public DbSet<App> Apps { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Waitlist> Waitlists { get; set; }
    public DbSet<UserSubscription> UserSubscriptions { get; set; }


    public DbSet<TEntity> Set<TEntity>() where TEntity : class
    {
        return base.Set<TEntity>();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasQueryFilter(_ => !_.IsDeleted);
        modelBuilder.Entity<App>().HasQueryFilter(_ => !_.IsDeleted);

        // Email : sa@mailinator.com, Password : pass123
        modelBuilder.Entity<User>().HasData(
           new User { Id = 1, Name = "Super Admin", Email = "sa@mailinator.com", Role = Shared.Commons.enRole.Admin, Password = "$2a$11$LF.jO5445FGwpoGW9PGgR.TKNymOmleYKS2vPhTcpqanjMM9stbIC" });
    }
}
