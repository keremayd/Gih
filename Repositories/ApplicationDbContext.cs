using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions context) : base(context)
    {
    }
    public DbSet<Person> Persons { get; set; } 
    public DbSet<Role> Roles { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Advert> Adverts { get; set; }
    public DbSet<Cso> Csos { get; set; }
}