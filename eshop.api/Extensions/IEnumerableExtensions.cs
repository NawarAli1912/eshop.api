using eshop.api.Common.Requests;

namespace eshop.api.Extensions;

public static class IEnumerableExtensions
{
    public static PaginatedResult<T> ToPagedList<T>(
        this IEnumerable<T> items,
        PaginationRequest paginationRequest,
        IHttpContextAccessor httpContextAccessor)
    {
        bool isLastPage = true;

        if (items.Count() > paginationRequest.PageSize)
        {
            isLastPage = false;

            // skip the last item
            items = items.SkipLast(1);
        }

        PaginatedResult<T> pagedList = new()
        {
            Items = items,
            Pagination = Pagination.Create(
            httpContextAccessor,
            paginationRequest,
            isLastPage)
        };

        pagedList.Pagination.TotalCount = items.Count();

        return pagedList;
    }
}
