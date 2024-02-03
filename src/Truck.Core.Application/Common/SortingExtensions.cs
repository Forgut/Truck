namespace Truck.Core.Application.Common
{
    public static class SortingExtensions
    {
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> elements, ISortParameters sortParams)
        {
            if (string.IsNullOrEmpty(sortParams.OrderByFieldName))
                return elements;

            if (sortParams.OrderByAscending)
                return elements.OrderBy(x => x!.GetType().GetProperty(sortParams.OrderByFieldName)?.GetValue(x, null));
            else
                return elements.OrderByDescending(x => x!.GetType().GetProperty(sortParams.OrderByFieldName)?.GetValue(x, null));
        }
    }
}
