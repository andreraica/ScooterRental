namespace ScooterRental.Infrastructure.IoC
{
    using global::Infrastructure.CrossCutting.Interfaces;
    using global::Infrastructure.CrossCutting.MessageBroker;

    using Microsoft.Extensions.DependencyInjection;

    using ScooterRental.Domain.Interfaces.Data.Repositories;
    using ScooterRental.Domain.Interfaces.IoC;
    using ScooterRental.Domain.Interfaces.Services;
    using ScooterRental.Domain.Services;
    using ScooterRental.Infrastructure.Data.Repositories;
    using ScooterRental.Infrastructure.IoC.Setting;

    using System.Configuration;

    public static class Injector
    {
        public static IServiceCollection Start(IServiceCollection services)
        {
            services.AddScoped<IScooterService, ScooterService>();
            services.AddScoped<IScooterRepository, ScooterRepository>();

            // Message Broker
            services.AddScoped<IQueue, Queue>();

            services.AddSingleton<ISettings>(new Settings()
            {
                AppSettings = ConfigurationManager.AppSettings
            });

            return services;
        }
    }
}
