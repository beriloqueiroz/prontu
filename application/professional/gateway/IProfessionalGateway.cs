using domain;

namespace application.professional;

public interface IProfessionalGateway : IRepository<Professional>
{
  bool IsExists(string id);
  bool IsExists(string document, string email);
  void AddPatient(Patient patient, string professionalId);
  Patient FindPatient(string id, string professionalId);
  void UpdatePatient(Patient patient, string professionalId);
}