using Autofac;

namespace Domain.Locations;

public class LocationsModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
    
        builder.RegisterType<Repositories.LocationRepository>().AsImplementedInterfaces();
        builder.RegisterType<Services.LocationService>().AsImplementedInterfaces();
    }
}