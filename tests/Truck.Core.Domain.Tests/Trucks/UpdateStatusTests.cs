using NSubstitute;
using Truck.Core.Application.Trucks;
using Truck.Core.Domain.Trucks;
using Truck.Core.Entities.Trucks;
using Xunit;

namespace Truck.Core.Domain.Tests.Trucks
{
    public class UpdateStatusTests
    {
        private readonly ITruckRepository _truckRepository;
        private readonly UpdateStatus _updateStatus;

        public UpdateStatusTests()
        {
            _truckRepository = Substitute.For<ITruckRepository>();
            _updateStatus = new UpdateStatus(_truckRepository);
        }

        [Theory]
        [InlineData(ETruckStatus.OutOfService)]
        [InlineData(ETruckStatus.Loading)]
        [InlineData(ETruckStatus.ToJob)]
        [InlineData(ETruckStatus.AtJob)]
        [InlineData(ETruckStatus.Returning)]
        public void Update_should_throw_if_new_state_is_the_same_as_old_state(ETruckStatus status)
        {
            var truck = GetTruck(status);

            Assert.Throws<StatusUpdateException>(() => _updateStatus.Update(truck, status));
        }

        [Theory]
        [InlineData(ETruckStatus.Loading)]
        [InlineData(ETruckStatus.ToJob)]
        [InlineData(ETruckStatus.AtJob)]
        [InlineData(ETruckStatus.Returning)]
        public void Update_should_allow_OutOfService_status_regardless_of_current_status(ETruckStatus currentStatus)
        {
            var truck = GetTruck(currentStatus);

            _updateStatus.Update(truck, ETruckStatus.OutOfService);

            Assert.Equal(ETruckStatus.OutOfService, truck.Status);
        }

        [Theory]
        [InlineData(ETruckStatus.Loading)]
        [InlineData(ETruckStatus.ToJob)]
        [InlineData(ETruckStatus.AtJob)]
        [InlineData(ETruckStatus.Returning)]
        public void Update_should_allow_any_new_status_if_current_status_is_OutOfService(ETruckStatus newStatus)
        {
            var truck = GetTruck(ETruckStatus.OutOfService);

            _updateStatus.Update(truck, newStatus);

            Assert.Equal(newStatus, truck.Status);
        }

        [Fact]
        public void Update_should_allow_Loading_status_if_current_status_is_Returning()
        {
            var truck = GetTruck(ETruckStatus.Returning);

            _updateStatus.Update(truck, ETruckStatus.Loading);

            Assert.Equal(ETruckStatus.Loading, truck.Status);
        }

        [Theory]
        [InlineData(ETruckStatus.ToJob)]
        [InlineData(ETruckStatus.AtJob)]
        public void Update_should_throw_if_current_status_is_Returning_and_new_status_is_not_Loading(ETruckStatus newStatus)
        {
            var truck = GetTruck(ETruckStatus.Returning);

            Assert.Throws<StatusUpdateException>(() => _updateStatus.Update(truck, newStatus));
        }

        [Fact]
        public void Update_should_allow_ToJob_status_if_current_status_is_Loading()
        {
            var truck = GetTruck(ETruckStatus.Loading);

            _updateStatus.Update(truck, ETruckStatus.ToJob);

            Assert.Equal(ETruckStatus.ToJob, truck.Status);
        }

        [Theory]
        [InlineData(ETruckStatus.Returning)]
        [InlineData(ETruckStatus.AtJob)]
        public void Update_should_throw_if_current_status_is_Loading_and_new_status_is_not_ToJob(ETruckStatus newStatus)
        {
            var truck = GetTruck(ETruckStatus.Loading);

            Assert.Throws<StatusUpdateException>(() => _updateStatus.Update(truck, newStatus));
        }

        [Fact]
        public void Update_should_allow_AtJob_status_if_current_status_is_ToJob()
        {
            var truck = GetTruck(ETruckStatus.ToJob);

            _updateStatus.Update(truck, ETruckStatus.AtJob);

            Assert.Equal(ETruckStatus.AtJob, truck.Status);
        }

        [Theory]
        [InlineData(ETruckStatus.Loading)]
        [InlineData(ETruckStatus.Returning)]
        public void Update_should_throw_if_current_status_is_ToJob_and_new_status_is_not_AtJob(ETruckStatus newStatus)
        {
            var truck = GetTruck(ETruckStatus.ToJob);

            Assert.Throws<StatusUpdateException>(() => _updateStatus.Update(truck, newStatus));
        }

        [Fact]
        public void Update_should_allow_Returning_status_if_current_status_is_AtJob()
        {
            var truck = GetTruck(ETruckStatus.AtJob);

            _updateStatus.Update(truck, ETruckStatus.Returning);

            Assert.Equal(ETruckStatus.Returning, truck.Status);
        }

        [Theory]
        [InlineData(ETruckStatus.Loading)]
        [InlineData(ETruckStatus.ToJob)]
        public void Update_should_throw_if_current_status_is_AtJob_and_new_status_is_not_Returning(ETruckStatus newStatus)
        {
            var truck = GetTruck(ETruckStatus.AtJob);

            Assert.Throws<StatusUpdateException>(() => _updateStatus.Update(truck, newStatus));
        }

        private TruckDto GetTruck(ETruckStatus status)
        {
            return new TruckDto()
            {
                Code = "TEST123",
                Name = "name",
                Status = status,
            };
        }
    }
}
