using System.ComponentModel.DataAnnotations;

namespace Domain.Desks.Dto;

public class DeskDto
{
    public DeskDto(int id, string code, bool isBooked, DateTime? bookedAt, DateTime? bookedUntil, int? userId)
    {
        Id = id;
        Code = code;
        IsBooked = isBooked;
        BookedAt = bookedAt;
        BookedUntil = bookedUntil;
        UserId = userId;
    } 
        
    public int Id { get; set; }
    [Required]
    public string Code { get; set; }
    [Required]
    public bool IsBooked { get; set; }
    public DateTime? BookedAt { get; set; }
    public DateTime? BookedUntil { get; set; }
    public int? UserId { get; set; }
}