using domain;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repository;

[Index(nameof(Email), nameof(Document))]
public class Professional : Model
{
  public Professional()
  {

  }
  public required string ProfessionalDocument { get; set; }
  public required string ProfessionalDocumentInstitution { get; set; }
  public required string Name { get; set; }
  public required string Email { get; set; }
  public required string Document { get; set; }
  public IList<Patient> Patients { get; } = new List<Patient>();

  public List<ProfessionalPatient> ProfessionalPatients { get; } = new();

  public void AddPatient(Patient patient)
  {
    Patients.Add(patient);
  }

  public void FromEntity(domain.Professional entity)
  {
    Id = entity.Id;
    Document = entity.Document.Value;
    Email = entity.Email;
    Name = entity.Name;
    ProfessionalDocument = entity.ProfessionalDocument.Value;
    ProfessionalDocumentInstitution = entity.ProfessionalDocument.Institution;
  }

  public static Professional From(domain.Professional entity)
  {
    return new Professional()
    {
      Id = entity.Id,
      Document = entity.Document.Value,
      Email = entity.Email,
      Name = entity.Name,
      ProfessionalDocument = entity.ProfessionalDocument.Value,
      ProfessionalDocumentInstitution = entity.ProfessionalDocument.Institution
    };
  }

  public domain.Professional ToEntity()
  {
    return new domain.Professional(
      new(ProfessionalDocument, ProfessionalDocumentInstitution),
      Name,
      Email,
      new Cpf(Document),
      Patients.Where(p => p != null).Select(p => p.ToEntity())
      .Select(p =>
      {
        var professionalPatient = ProfessionalPatients.Find(pp => pp.PatientId == p.Id);
        if (professionalPatient != null && professionalPatient.Active)
          p.Activate();
        else p.Deactivate();
        return p;
      }).ToList(),
      Id.ToString());
  }

}