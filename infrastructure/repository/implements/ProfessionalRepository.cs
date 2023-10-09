using application;
using application.professional;
using infrastructure.repository;

namespace repository;

public class ProfessionalRepository : IProfessionalGateway
{
  private readonly ProntuDbContext Context;

  public ProfessionalRepository(ProntuDbContext context)
  {
    Context = context;
  }
  public void Create(domain.Professional entity)
  {
    throw new NotImplementedException();
  }

  public domain.Professional? Find(string id)
  {
    return Context.Professionals?.Find(id)?.ToEntity();
  }

  public bool IsExists(string document, string email)
  {
    var result = Context.Professionals?.Any(p => p.Document.Equals(document) || p.Email.Equals(email));
    return result != null && (bool)result;
  }

  public PaginatedList<domain.Professional> List(PageAble pageAble)
  {
    if (Context.Professionals == null)
    {
      return PaginatedList<domain.Professional>.Empty();
    }
    return new(Context.Professionals
      .Skip(pageAble.PageIndex)
      .Take(pageAble.PageSize).ToList().Select(p => p.ToEntity()), pageAble.PageIndex, pageAble.PageSize);
  }

  public void Update(domain.Professional entity)
  {
    throw new NotImplementedException();
  }
}