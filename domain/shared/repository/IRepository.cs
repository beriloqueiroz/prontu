namespace domain;

public interface IRepository<T> where T : Entity
{
  void Update(T entity);
  void Create(T entity);
  T? Find(string id);
  PaginatedList<T> List(PageAble pageAble);

}