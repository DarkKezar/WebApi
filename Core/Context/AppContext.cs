using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.Context;

public class AppContext : IdentityDbContext<User, Role, Guid>
{
    public DbSet<Pet> Pets { get; set; }
    public DbSet<PetStats> PetStats { get; set; }
    public DbSet<Farm> Farms { get; set; }
    
    public AppContext(DbContextOptions<AppContext> options) : base(options)
    { }
}