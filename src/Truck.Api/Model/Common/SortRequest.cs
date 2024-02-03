using Truck.Core.Application.Common;

namespace Truck.Api.Model.Common
{
    public class SortRequest : ISortParameters
    {
        public string? OrderByFieldName { get; set; }
        public bool OrderByAscending { get; set; } = true;
    }
}
