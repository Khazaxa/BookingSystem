using Domain.Desks.Dto;

namespace Domain.Locations.Dto;

public class LocationDto
{
    string Name { get; set; }
    public List<DeskDto>? Desks { get; set; }
}