using Microsoft.EntityFrameworkCore;

namespace Domain.Locations.Entities;

internal class Location
{
    private Location() {}
    
    public Location(string name)
    {
        Name = name;
    }
    
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    
    public static void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Location>().HasIndex(x => x.Name).IsUnique();
    }
}