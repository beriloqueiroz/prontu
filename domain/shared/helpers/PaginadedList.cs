namespace domain;
public class PaginatedList<T> : List<T>
{
  public int PageIndex { get; private set; }
  public int TotalPages { get; private set; }

  public PaginatedList(List<T> items, int count, PageAble pageAble)
  {
    PageIndex = pageAble.PageIndex;
    TotalPages = (int)Math.Ceiling(count / (double)pageAble.PageSize);

    AddRange(items);
  }

  public bool HasPreviousPage => PageIndex > 1;

  public bool HasNextPage => PageIndex < TotalPages;

  public static PaginatedList<T> Empty()
  {
    return new PaginatedList<T>(new List<T>(), 0, new PageAble(1, 1));
  }
}