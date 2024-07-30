using Autofac;

namespace Domain.Desks;

public class DesksModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
    
        builder.RegisterType<Repositories.DeskRepository>().AsImplementedInterfaces();
        builder.RegisterType<Services.DeskService>().AsImplementedInterfaces();
    }
}