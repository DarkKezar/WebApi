using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.Context;

public class PetContext : IdentityDbContext<User, Role, Guid>
{
    public DbSet<Pet> Pets { get; set; }
    public DbSet<PetStats> PetStats { get; set; }
    public DbSet<Farm> Farms { get; set; }
    public DbSet<UserAction> Actions { get; set; }

    public PetContext(DbContextOptions<PetContext> options) : base(options)
    { }
}