using Truck.Core.Application.Trucks;
using Truck.Core.Entities.Trucks;

namespace Truck.Core.Domain.Trucks
{
    public interface IUpdateTruckStatus
    {
        void Update(int id, ETruckStatus status);
    }

    internal class UpdateTruckStatus : IUpdateTruckStatus
    {
        private readonly ITruckRepository _truckRepository;
        private readonly IUpdateStatus _updateStatus;

        public UpdateTruckStatus(ITruckRepository truckRepository, IUpdateStatus updateStatus)
        {
            _truckRepository = truckRepository;
            _updateStatus = updateStatus;
        }

        public void Update(int id, ETruckStatus status)
        {
            var truck = _truckRepository.GetTruck(id)
                ?? throw new TruckNotFoundException(id);

            _updateStatus.Update(truck, status);

            _truckRepository.UpdateTruck(truck);
        }
    }
}