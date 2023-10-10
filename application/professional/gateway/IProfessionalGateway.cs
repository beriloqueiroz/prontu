using domain;

namespace application.professional;

public interface IProfessionalGateway : IRepository<Professional>
{
  bool IsExists(string id);
  bool IsExists(string document, string email);
}