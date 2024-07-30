using Core.Configuration.Config;
using Microsoft.Extensions.Configuration;

namespace Core.Configuration;

public class AppConfiguration : IAppConfiguration
{
    public ReservationsConfig Reservations { get; }

    public AppConfiguration(IConfiguration configuration)
    {
        Reservations = configuration.GetSection("App:Reservations").Get<ReservationsConfig>();
    }
}