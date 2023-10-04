namespace domain;

public class PageAble
{
  public PageAble(int pageSize, int pageIndex)
  {
    PageSize = pageSize;
    if (PageSize == 0) PageSize = 1;
    PageIndex = pageIndex;
  }
  public int PageIndex;
  public int PageSize;
}