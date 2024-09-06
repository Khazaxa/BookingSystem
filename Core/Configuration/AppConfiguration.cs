using Core.Configuration.Config;
using Microsoft.Extensions.Configuration;

namespace Core.Configuration;

public class AppConfiguration(IConfiguration configuration) : IAppConfiguration
{
    public ReservationsConfig? Reservations { get; } = configuration
        .GetSection("App:Reservations")
        .Get<ReservationsConfig>();
}