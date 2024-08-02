using System.ComponentModel.DataAnnotations;
using Domain.Locations.Entities;
using Domain.Users.Entities;
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
    public int? UserId { get; private set; }
    public User? User { get; private set; }
    
    public void ChangeStatus()
    {
        IsAvailable = !IsAvailable;
    }
    
    public void Book(DateTime bookedAt, DateTime bookedUntil, int userId)
    {
        IsBooked = true;
        IsAvailable = false;
        BookedAt = bookedAt;
        BookedUntil = bookedUntil;
        UserId = userId;
    }
    
    public void Unbook()
    {
        IsBooked = false;
        IsAvailable = true;
        BookedAt = null;
        BookedUntil = null;
    }
    
    public static void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Desk>().HasIndex(x => x.Code).IsUnique();
        builder.Entity<Desk>()
            .HasOne(d => d.Location)
            .WithMany(l => l.Desks)
            .HasForeignKey(d => d.LocationId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Desk>()
            .HasOne(d => d.User)
            .WithOne(u => u.Desk)
            .HasForeignKey<User>(u => u.DeskId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}