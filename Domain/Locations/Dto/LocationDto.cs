using Domain.Desks.Dto;

namespace Domain.Locations.Dto;

public class LocationDto(int id, string name, List<DeskDto>? desks)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public List<DeskDto>? Desks { get; set; } = desks;
}