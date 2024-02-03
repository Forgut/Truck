using Truck.Core.Application.Common;
using Truck.Core.Application.Trucks;
using Truck.Core.Entities.Trucks;

namespace Truck.Infrastructure.Trucks
{
    internal class TruckRepository : ITruckRepository
    {
        //mocked in memory collection - in normal implementation connection to Db would sit here
        private readonly ITrucksDataSet _dataSet;

        public TruckRepository(ITrucksDataSet dataSet)
        {
            _dataSet = dataSet;
        }

        public void AddTruck(TruckDto truck)
        {
            _dataSet.Add(truck);
        }

        public void DeleteTruck(int id)
        {
            _dataSet.Delete(id);
        }

        public TruckDto? GetTruck(int id)
        {
            return _dataSet.Trucks.SingleOrDefault(x => x.Id == id);
        }

        public TruckDto? GetTruck(string code)
        {
            return _dataSet.Trucks.SingleOrDefault(x => x.Code == code);
        }

        public IEnumerable<TruckDto> GetTrucks(string? code, string? name, ETruckStatus? status, ISortParameters sortParams)
        {
            var result = _dataSet.Trucks.AsEnumerable();

            if (!string.IsNullOrEmpty(code))
                result = result.Where(x => x.Code.Contains(code));

            if (!string.IsNullOrEmpty(name))
                result = result.Where(x => x.Name.Contains(name));

            if (status.HasValue)
                result = result.Where(x => x.Status == status.Value);

            result = result.OrderBy(sortParams);

            return result;
        }

        public void UpdateTruck(TruckDto truck)
        {
            _dataSet.Update(truck);
        }
    }
}
