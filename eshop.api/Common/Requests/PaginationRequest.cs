namespace eshop.api.Common.Requests;

public record PaginationRequest(int PageIndex = 1, int PageSize = 10);
