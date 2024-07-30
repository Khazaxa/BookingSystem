namespace Domain.Desks.Dto;

public record DeskParams(
  string Name,
  string Code,
  bool IsBooked,
  int LocationId
);