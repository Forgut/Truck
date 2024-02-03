using Newtonsoft.Json;
using System.Diagnostics;
using Truck.Core.Entities.Trucks;

namespace Truck.Infrastructure.Trucks
{
    public interface ITrucksDataSet
    {
        public IEnumerable<TruckDto> Trucks { get; }
        void Add(TruckDto truck);
        void Update(TruckDto truck);
        void Delete(int id);
    }

    /// <summary>
    /// This is mocked collection for trucks to avoid implementing database at this point
    /// </summary>
    internal class TrucksDataSet : ITrucksDataSet
    {
        public IEnumerable<TruckDto> Trucks => _trucksSerialized.Select(x => JsonConvert.DeserializeObject<TruckDto>(x)!).ToList();
        private List<string> _trucksSerialized;

        public TrucksDataSet()
        {
            _trucksSerialized = new List<string>()
            {
                JsonConvert.SerializeObject(new TruckDto()
                {
                    Id = 1,
                    Code = "TEST1",
                    Name = "Truck 1",
                    Status = ETruckStatus.OutOfService,
                    Description = "This is truck 1"
                }),
                JsonConvert.SerializeObject(new TruckDto()
                {
                    Id = 2,
                    Code = "TEST2",
                    Name = "Truck 2",
                    Status = ETruckStatus.AtJob,
                    Description = "This is truck 2"
                }),
                JsonConvert.SerializeObject(new TruckDto()
                {
                    Id = 3,
                    Code = "TEST3",
                    Name = "Truck 3",
                    Status = ETruckStatus.ToJob,
                    Description = "This is truck 3"
                }),
                JsonConvert.SerializeObject(new TruckDto()
                {
                    Id = 4,
                    Code = "TEST4",
                    Name = "Truck 4",
                    Status = ETruckStatus.Returning,
                    Description = "This is truck 4"
                })
            };
        }

        public void Update(TruckDto truck)
        {
            var truckToUpdate = _trucksSerialized.Single(x => JsonConvert.DeserializeObject<TruckDto>(x)!.Id == truck.Id);
            var index = _trucksSerialized.IndexOf(truckToUpdate);
            _trucksSerialized[index] = JsonConvert.SerializeObject(truck);
        }

        public void Add(TruckDto truck)
        {
            var newId = Trucks.Max(x => x.Id) + 1;
            truck.Id = newId;
            _trucksSerialized.Add(JsonConvert.SerializeObject(truck));
        }

        public void Delete(int id)
        {
            var truckToDelete = _trucksSerialized.Single(x => JsonConvert.DeserializeObject<TruckDto>(x)!.Id == id);
            var index = _trucksSerialized.IndexOf(truckToDelete);
            _trucksSerialized.RemoveAt(index);
        }
    }
}
