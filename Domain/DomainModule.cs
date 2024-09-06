using System.Reflection;
using Autofac;
using Domain.Authentication.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module = Autofac.Module;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;

namespace Domain;

public class DomainModule(IConfigurationRoot _configuration) : Module
{
    private const string ConnectionStringName = nameof(BookingSystemDbContext);

    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
    
        builder.RegisterInstance(_configuration).As<IConfigurationRoot>();
        builder.RegisterModule<Users.UsersModule>();
        builder.RegisterModule<Authentication.AuthenticationModule>();
        builder.RegisterModule<Locations.LocationsModule>();
        builder.RegisterModule<Desks.DesksModule>();
    
        builder.RegisterType<UserContextService>().As<IUserContextService>().InstancePerLifetimeScope();
    
        RegisterDatabaseProviders(builder);
        RegisterMediator(builder);
    }
    
    public static void MigrateDatabase(IServiceScope scope)
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<BookingSystemDbContext>();
        dbContext.Database.Migrate();
    }
    
    private void RegisterDatabaseProviders(ContainerBuilder builder)
    {
        builder
            .Register(_ =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<BookingSystemDbContext>();
                var connectionString = _configuration.GetConnectionString(ConnectionStringName);
                if (connectionString != null)
                    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                return new BookingSystemDbContext(optionsBuilder.Options);
            })
            .As<DbContext>()
            .AsSelf()
            .InstancePerDependency();
    }
    
    private static void RegisterMediator(ContainerBuilder builder)
    {
        var mediatorConfiguration = MediatRConfigurationBuilder
            .Create(Assembly.GetExecutingAssembly())
            .WithAllOpenGenericHandlerTypesRegistered()
            .WithRegistrationScope(RegistrationScope.Scoped)
            .Build();

        builder.RegisterMediatR(mediatorConfiguration);
    }
}

