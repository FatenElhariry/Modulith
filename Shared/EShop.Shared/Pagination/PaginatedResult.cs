

namespace EShop.Shared.Pagination;

public class PaginatedResult<TEntity> where TEntity : class
{
    public PaginatedResult(IEnumerable<TEntity> items, int totalCount, int pageNumber, int pageSize)
    {
        Items = items ?? throw new ArgumentNullException(nameof(items));
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
    public IEnumerable<TEntity> Items { get; }
    public int TotalCount { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
}  
