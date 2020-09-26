namespace ScooterRental.Infrastructure.IoC
{
    using System.Configuration;

    using Microsoft.Extensions.DependencyInjection;

    using ScooterRental.Domain.Interfaces.Data.Repositories;
    using ScooterRental.Domain.Interfaces.IoC;
    using ScooterRental.Domain.Interfaces.Services;
    using ScooterRental.Domain.Services;
    using ScooterRental.Infrastructure.Data.Repositories;
    using ScooterRental.Infrastructure.IoC.Setting;

    public static class Injector
    {
        public static IServiceCollection Start(IServiceCollection services)
        {
            services.AddScoped<ITrackingService, TrackingService>();
            services.AddScoped<ITrackingRepository, TrackingRepository>();

            services.AddSingleton<ISettings>(new Settings()
            {
                AppSettings = ConfigurationManager.AppSettings
            });

            return services;
        }
    }
}
