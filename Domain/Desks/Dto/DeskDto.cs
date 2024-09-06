using System.ComponentModel.DataAnnotations;

namespace Domain.Desks.Dto;

public class DeskDto(
    int id,
    string code,
    bool isAvailable,
    bool isBooked,
    DateTime? bookedAt,
    DateTime? bookedUntil,
    int? userId)
{
    public int Id { get; set; } = id;

    [Required]
    public string Code { get; set; } = code;

    [Required]
    public bool IsAvailable { get; set; } = isAvailable;

    [Required]
    public bool IsBooked { get; set; } = isBooked;

    public DateTime? BookedAt { get; set; } = bookedAt;
    public DateTime? BookedUntil { get; set; } = bookedUntil;
    public int? UserId { get; set; } = userId;
}