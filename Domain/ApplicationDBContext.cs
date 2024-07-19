using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Helpers;

namespace Domain;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext() { }
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

    public DbSet<App> Apps { get; set; }
    public DbSet<User> Users { get; set; }


    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    // Configure App entity


    //    base.OnModelCreating(modelBuilder);
    //}
}
