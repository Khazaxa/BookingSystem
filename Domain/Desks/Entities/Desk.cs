using System.ComponentModel.DataAnnotations;
using Domain.Locations.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Desks.Entities;

internal class Desk
{
    private const int CodeLength = 3;
    
    private Desk() {}

    public Desk(string code, int locationId)
    {
        Code = code;
        LocationId = locationId;
    }
    
    public int Id { get; private set; }
    [MaxLength(CodeLength)]
    public string Code { get; private set; }
    public bool IsAvailable { get; private set; }
    public bool IsBooked { get; private set; }
    public DateTime? BookedAt { get; private set; }
    public DateTime? BookedUntil { get; private set; }
    public int LocationId { get; private set; }
    public Location Location { get; private set; }
    
    public void ChangeStatus()
    {
        IsAvailable = !IsAvailable;
    }
    
    public void Book(DateTime bookedAt, DateTime bookedUntil)
    {
        IsBooked = true;
        BookedAt = bookedAt;
        BookedUntil = bookedUntil;
    }
    
    public static void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Desk>().HasIndex(x => x.Code).IsUnique();
    }
}