namespace domain;
public interface IProfessionalGateway : IRepository<Professional>
{
  bool IsExists(string document, string email);
}