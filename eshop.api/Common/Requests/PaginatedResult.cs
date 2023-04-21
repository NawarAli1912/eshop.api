namespace eshop.api.Common.Requests;

public record PaginatedResult<T>
{

    public required IEnumerable<T> Items { get; set; }

    public required Pagination Pagination { get; set; }

}

public record Pagination
{
    public string FirstPageUrl { get; private set; }

    public string NextPageUrl { get; private set; }

    public string PrevPageUrl { get; private set; }

    public int TotalCount { get; set; }

    public static Pagination Create(
        IHttpContextAccessor httpContextAccessor,
        PaginationRequest paginationRequest,
        bool isLastPage)
    {
        ArgumentNullException.ThrowIfNull(httpContextAccessor);

        // Build Url using request object in http context
        UriBuilder uriBuilder = new()
        {
            Scheme = httpContextAccessor.HttpContext!.Request.Scheme,

            // Getting the host value with port will add brackets [], get them in separate lines
            Host = httpContextAccessor.HttpContext.Request.Host.Host,
        };

        if (httpContextAccessor.HttpContext.Request.Host.Port.HasValue)
        {
            uriBuilder.Port = httpContextAccessor.HttpContext.Request.Host.Port.Value;
        }

        uriBuilder.Path = httpContextAccessor.HttpContext.Request.Path.ToString();

        // Getting the current query strings, and remove the current pageindex and pagesize
        var query = httpContextAccessor.HttpContext.Request.Query
            .Where(q => q.Key.ToLower() != "pageindex" && q.Key.ToLower() != "pagesize");

        // Keep the old query strings if any (like search..)
        string baseQuery = "?";
        foreach (var kv in query)
        {
            baseQuery += $"{kv.Key}={kv.Value}&";
        }

        // build the new query strings
        string
            firstPageQueryString = string.Empty,
            nextPageQueryString = string.Empty,
            prevPageQueryString = string.Empty;

        firstPageQueryString = baseQuery + $"PageIndex=1&PageSize={paginationRequest.PageSize}";
        if (!isLastPage)
        {
            nextPageQueryString = baseQuery + $"PageIndex={paginationRequest.PageIndex + 1}&PageSize={paginationRequest.PageSize}";
        }

        if (paginationRequest.PageIndex > 1)
        {
            prevPageQueryString = baseQuery + $"PageIndex={paginationRequest.PageIndex - 1}&PageSize={paginationRequest.PageSize}";
        }

        // Build the final Urls
        string baseUrl = uriBuilder.ToString();
        var page = new Pagination();
        page.FirstPageUrl = baseUrl + firstPageQueryString;

        if (!string.IsNullOrEmpty(nextPageQueryString))
        {
            page.NextPageUrl = baseUrl + nextPageQueryString;
        }

        if (!string.IsNullOrEmpty(prevPageQueryString))
        {
            page.PrevPageUrl = baseUrl + prevPageQueryString;
        }

        return page;
    }
}
