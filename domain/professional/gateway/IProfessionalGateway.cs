namespace domain;

public interface IProfessionalGateway
{
  void Create(Professional professional);
  void Update(Professional professional);
  Professional Find(string professionalId);
  PaginatedList<Professional> List(PageAble pageAble);


  void AddPatient(Patient patient, string professionalId);
  void UpdatePatient(Patient patient, string professionalId);
  Patient FindPatient(string patientId, string professionalId);
  PaginatedList<Patient> ListPatients(string professionalId, PageAble pageAble);
}