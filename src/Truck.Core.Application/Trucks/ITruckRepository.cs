using Truck.Core.Application.Common;
using Truck.Core.Entities.Trucks;

namespace Truck.Core.Application.Trucks
{
    public interface ITruckRepository
    {
        void AddTruck(TruckDto truck);
        TruckDto? GetTruck(int id);
        TruckDto? GetTruck(string code);
        IEnumerable<TruckDto> GetTrucks(string? code, string? name, ETruckStatus? status, ISortParameters sortParams);
        void UpdateTruck(TruckDto truck);
        void DeleteTruck(int id);
    }
}
