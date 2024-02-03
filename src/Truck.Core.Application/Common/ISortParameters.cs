namespace Truck.Core.Application.Common
{
    public interface ISortParameters
    {
        string? OrderByFieldName { get; set; }
        bool OrderByAscending { get; set; }
    }
}
