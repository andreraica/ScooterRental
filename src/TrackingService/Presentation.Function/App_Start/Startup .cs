using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Presentation.Function.DependencyInjection;
using ScooterRental.Infrastructure.IoC;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Presentation.Function.DependencyInjection
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            Injector.Start(builder.Services);
        }
    }
}
