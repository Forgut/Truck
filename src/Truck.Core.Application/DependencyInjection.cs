using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Truck.Core.Domain.Trucks;
using Truck.Core.Entities.Trucks;

namespace Truck.Core.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationLogic(this IServiceCollection services)
        {
            services.AddTransient<IUpdateStatus, UpdateStatus>();
            services.AddTransient<IValidator<TruckDto>, TruckValidator>();

            services.AddTransient<IUpdateTruckStatus, UpdateTruckStatus>();
            services.AddTransient<IUpdateTruckData, UpdateTruckData>();
            services.AddTransient<IGetTruck, GetTruck>();
            services.AddTransient<IGetTrucks, GetTrucks>();
            services.AddTransient<ICreateTruck, CreateTruck>();
            services.AddTransient<IDeleteTruck, DeleteTruck>();
        }
    }
}
