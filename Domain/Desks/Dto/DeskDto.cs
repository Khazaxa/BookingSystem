using System.ComponentModel.DataAnnotations;

namespace Domain.Desks.Dto;

public class DeskDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Code { get; set; }
    [Required]
    public bool IsBooked { get; set; }
    public DateTime? BookedAt { get; set; }
    public DateTime? BookedUntil { get; set; }
}