using Microsoft.Extensions.DependencyInjection;
using Truck.Core.Application.Trucks;
using Truck.Infrastructure.Trucks;

namespace Truck.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<ITrucksDataSet, TrucksDataSet>();
            services.AddTransient<ITruckRepository, TruckRepository>();
        }
    }
}
