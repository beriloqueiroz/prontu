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
  public required string Name { get; set; }
  public required string Email { get; set; }
  public required string Document { get; set; }
  public IList<Patient> Patients { get; } = new List<Patient>();

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
    ProfessionalDocument = entity.ProfessionalDocument;
  }

  public static Professional From(domain.Professional entity)
  {
    var patients = entity.Patients.Select(Patient.From).ToList();
    return new Professional()
    {
      Id = entity.Id,
      Document = entity.Document.Value,
      Email = entity.Email,
      Name = entity.Name,
      ProfessionalDocument = entity.ProfessionalDocument
    };
  }

  public domain.Professional ToEntity()
  {
    return new domain.Professional(
      ProfessionalDocument,
      Name,
      Email,
      new Cpf(Document),
      Patients.Where(p => p != null).Select(p => p.ToEntity()).ToList(),
      Id.ToString());
  }

}