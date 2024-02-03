using Truck.Core.Application.Trucks;
using Truck.Core.Entities.Trucks;

namespace Truck.Core.Domain.Trucks
{
    public interface IUpdateStatus
    {
        void Update(TruckDto truck, ETruckStatus status);
    }

    internal class UpdateStatus : IUpdateStatus
    {
        private readonly ITruckRepository _truckRepository;

        public UpdateStatus(ITruckRepository truckRepository)
        {
            _truckRepository = truckRepository;
        }

        public void Update(TruckDto truck, ETruckStatus status)
        {
            if (status == truck.Status)
                throw new StatusUpdateException(truck.Status, status);

            if (status == ETruckStatus.OutOfService)
            {
                truck.Status = ETruckStatus.OutOfService;
                _truckRepository.UpdateTruck(truck);
                return;
            }

            if (truck.Status == ETruckStatus.OutOfService)
            {
                truck.Status = status;
                _truckRepository.UpdateTruck(truck);
                return;
            }

            if ((int)status == (int)(truck.Status + 1) % 4)
            {
                truck.Status = status;
                _truckRepository.UpdateTruck(truck);
                return;
            }


            throw new StatusUpdateException(truck.Status, status);
        }
    }
}
