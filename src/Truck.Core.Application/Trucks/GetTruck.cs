using Truck.Core.Application.Trucks;
using Truck.Core.Entities.Trucks;

namespace Truck.Core.Domain.Trucks
{
    public interface IGetTruck
    {
        TruckDto Get(int id);
    }

    internal class GetTruck : IGetTruck
    {
        private readonly ITruckRepository _truckRepository;

        public GetTruck(ITruckRepository truckRepository)
        {
            _truckRepository = truckRepository;
        }

        public TruckDto Get(int id)
        {
            var truck = _truckRepository.GetTruck(id)
                ?? throw new TruckNotFoundException(id);

            return truck;
        }
    }
}
