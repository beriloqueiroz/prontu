namespace application;

public class PageAble
{
  public PageAble(int pageSize = 20, int pageIndex = 1)
  {
    PageSize = pageSize;
    if (PageSize <= 0) PageSize = 20;
    PageIndex = pageIndex;
    if (PageIndex <= 0) PageIndex = 1;
  }
  public int PageIndex { get; set; }
  public int PageSize { get; set; }
}