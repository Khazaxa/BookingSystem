using Domain.Desks.Entities;
using Domain.Locations.Entities;
using Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain;

internal class BookingSystemDbContext : DbContext
{
    public BookingSystemDbContext(DbContextOptions<BookingSystemDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; }
    public DbSet<Location> Locations { get; }
    public DbSet<Desk> Desks { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        User.OnModelCreating(modelBuilder);
        Location.OnModelCreating(modelBuilder);
        Desk.OnModelCreating(modelBuilder);
    }
}