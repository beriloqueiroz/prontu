namespace application;

public class PageAble
{
  public PageAble(int pageSize, int pageIndex)
  {
    PageSize = pageSize;
    if (PageSize == 0) PageSize = 1;
    PageIndex = pageIndex;
    if (PageIndex == 0) PageIndex = 1;
  }
  public int PageIndex;
  public int PageSize;
}