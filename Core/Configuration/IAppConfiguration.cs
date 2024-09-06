using Core.Configuration.Config;

namespace Core.Configuration;

public interface IAppConfiguration
{
    ReservationsConfig? Reservations { get; }
}