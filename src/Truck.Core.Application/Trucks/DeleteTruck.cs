using Truck.Core.Application.Trucks;
using Truck.Core.Entities.Trucks;

namespace Truck.Core.Domain.Trucks
{
    public interface IDeleteTruck
    {
        void Delete(int id);
    }

    internal class DeleteTruck : IDeleteTruck
    {
        private readonly ITruckRepository _truckRepository;

        public DeleteTruck(ITruckRepository truckRepository)
        {
            _truckRepository = truckRepository;
        }

        public void Delete(int id)
        {
            var _ = _truckRepository.GetTruck(id)
                ?? throw new TruckNotFoundException(id);

            _truckRepository.DeleteTruck(id);
        }
    }
}
