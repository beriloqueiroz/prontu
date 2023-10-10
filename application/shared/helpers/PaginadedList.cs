using X.PagedList;

namespace application;

public class PaginatedList<T> : PagedList<T>
{
  public PaginatedList(IPagedList pagedList, IEnumerable<T> superset) : base(pagedList, superset)
  {
  }

  public PaginatedList(IQueryable<T> superset, int pageNumber, int pageSize) : base(superset, pageNumber, pageSize)
  {
  }

  public PaginatedList(IEnumerable<T> superset, int pageNumber, int pageSize) : base(superset, pageNumber, pageSize)
  {
  }

  public PaginatedList(IEnumerable<T> superset, PageAble pageAble) : base(superset, pageAble.PageIndex, pageAble.PageSize)
  {
  }

  public PaginatedList(IQueryable<T> superset, PageAble pageAble) : base(superset, pageAble.PageIndex, pageAble.PageSize)
  {
  }

  public static PaginatedList<T> Empty()
  {
    return new PaginatedList<T>(new List<T>(), 1, 1);
  }
}