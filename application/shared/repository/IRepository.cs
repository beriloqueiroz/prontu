using domain;

namespace application;

public interface IRepository<T> where T : AggregateRoot
{
  void Update(T entity);
  void Create(T entity);
  T? Find(string id);
  PaginatedList<T> List(PageAble pageAble);

}