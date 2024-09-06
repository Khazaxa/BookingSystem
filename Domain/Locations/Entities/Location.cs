using System.ComponentModel.DataAnnotations;
using Domain.Desks.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Locations.Entities;

internal class Location
{
    private const int NameMaxLength = 8;
    
    private Location() {}
    
    public Location(string name)
    {
        Name = name;
    }
    
    
    public int Id { get; private init; }
    [MaxLength(NameMaxLength)]
    public string Name { get; private init; }
    public List<Desk>? Desks { get; private init; } = new();
    
    public static void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Location>().HasIndex(x => x.Name).IsUnique();
        builder.Entity<Location>()
            .HasMany(l => l.Desks)
            .WithOne()
            .HasForeignKey(d => d.LocationId);
    }
}