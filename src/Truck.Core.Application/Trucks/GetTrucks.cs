using Truck.Core.Application.Common;
using Truck.Core.Application.Trucks;
using Truck.Core.Entities.Trucks;

namespace Truck.Core.Domain.Trucks
{
    public record GetTrucksParameters(string? Code, string? Name, ETruckStatus? Status, ISortParameters SortParams);

    public interface IGetTrucks
    {
        IEnumerable<TruckDto> Get(GetTrucksParameters parameters);
    }

    internal class GetTrucks : IGetTrucks
    {
        private readonly ITruckRepository _truckRepository;

        public GetTrucks(ITruckRepository truckRepository)
        {
            _truckRepository = truckRepository;
        }

        public IEnumerable<TruckDto> Get(GetTrucksParameters parameters)
        {
            return _truckRepository.GetTrucks(parameters.Code,
                                              parameters.Name,
                                              parameters.Status,
                                              parameters.SortParams);
        }
    }
}
