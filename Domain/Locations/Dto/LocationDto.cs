using Domain.Desks.Dto;

namespace Domain.Locations.Dto;

public class LocationDto
{
    public LocationDto(int id, string name, List<DeskDto>? desks)
    {
        Id = id;
        Name = name;
        Desks = desks;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public List<DeskDto>? Desks { get; set; }
}