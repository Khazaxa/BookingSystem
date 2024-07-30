using System.ComponentModel.DataAnnotations;
using Domain.Locations.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Desks.Entities;

internal class Desk
{
    private const int NameMaxLength = 10;
    private const int CodeMinLength = 3;
    private const int CodeMaxLength = 6;
    
    
    private Desk() {}

    public Desk(string name, string code, bool isBooked, DateTime? bookedAt, DateTime? bookedUntil)
    {
        Name = name;
        Code = code;
        IsBooked = isBooked;
        BookedAt = bookedAt;
        BookedUntil = bookedUntil;
    }
    
    public int Id { get; private set; }
    [MaxLength(NameMaxLength)]
    public string Name { get; private set; }
    [MinLength(CodeMinLength)]
    [MaxLength(CodeMaxLength)]
    public string Code { get; private set; }
    public bool IsBooked { get; private set; }
    public DateTime? BookedAt { get; private set; }
    public DateTime? BookedUntil { get; private set; }
    public int LocationId { get; private set; }
    public Location Location { get; private set; }
    
    public static void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Desk>().HasIndex(x => x.Code).IsUnique();
    }
}