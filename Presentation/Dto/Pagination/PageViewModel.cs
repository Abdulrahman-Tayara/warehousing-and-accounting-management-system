namespace wms.Dto.Pagination;

public class PageViewModel<T>
{
    public IEnumerable<T> Data { get; set; }
    
    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    public int PagesCount { get; set; }

    public int RowsCount { get; set; }
}