using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security;

namespace Domain;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext() { }
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

    public DbSet<App> Apps { get; set; }
    public DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasQueryFilter(_ => !_.IsDeleted);
        modelBuilder.Entity<App>().HasQueryFilter(_ => !_.IsDeleted);

        // Email : sa@mailinator.com, Password : pass123
        modelBuilder.Entity<User>().HasData(
           new User { Id = 1, Name = "Super Admin", Email = "sa@mailinator.com", Role = Shared.Commons.enRole.Admin, Password = "$2a$11$LF.jO5445FGwpoGW9PGgR.TKNymOmleYKS2vPhTcpqanjMM9stbIC" });
    }
}
