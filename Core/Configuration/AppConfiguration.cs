using Core.Configuration.Config;
using Microsoft.Extensions.Configuration;

namespace Core.Configuration;

public class AppConfiguration(IConfiguration configuration) : IAppConfiguration
{
    public ReservationsConfig? Reservations { get; } = configuration
        .GetSection("App:Reservations")
        .Get<ReservationsConfig>();
    
    public AzureConfig? Azure { get; } = configuration
        .GetSection("App:Azure")
        .Get<AzureConfig>();
}