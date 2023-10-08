using application;
using application.professional;
using domain;

namespace repository;

public class ProfessionalRepository : IProfessionalGateway
{
  public void Create(Professional entity)
  {
    throw new NotImplementedException();
  }

  public Professional? Find(string id)
  {
    throw new NotImplementedException();
  }

  public bool IsExists(string document, string email)
  {
    throw new NotImplementedException();
  }

  public PaginatedList<Professional> List(PageAble pageAble)
  {
    throw new NotImplementedException();
  }

  public void Update(Professional entity)
  {
    throw new NotImplementedException();
  }
}