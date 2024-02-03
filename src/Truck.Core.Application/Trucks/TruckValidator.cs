using FluentValidation;
using System.Text.RegularExpressions;
using Truck.Core.Application.Trucks;
using Truck.Core.Entities.Trucks;

namespace Truck.Core.Domain.Trucks
{
    internal class TruckValidator : AbstractValidator<TruckDto>
    {
        public TruckValidator(ITruckRepository truckRepository)
        {
            var alphanumeric = new Regex("^[a-zA-Z0-9]*$");

            RuleFor(x => x.Code)
                .Matches(alphanumeric)
                .WithMessage(x => $"Code {x.Code} is not an alphanumeric code")
                .Custom((code, context) =>
                {
                    var truck = truckRepository.GetTruck(code);
                    if (truck != null && truck.Id != context.InstanceToValidate.Id)
                        context.AddFailure(nameof(TruckDto.Code), $"Truck with code {code} already exists");
                });

            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
