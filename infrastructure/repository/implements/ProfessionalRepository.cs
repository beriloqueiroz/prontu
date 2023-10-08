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
    return new Professional("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), id);
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