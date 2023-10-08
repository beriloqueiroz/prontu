using System.ComponentModel.DataAnnotations.Schema;
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
  public IList<ProfessionalPatient>? ProfessionalPatients { get; set; }

  [NotMapped]
  public IList<Patient>? Patients
  {
    get
    {
      return ProfessionalPatients?.Select(pp => pp.Patient).ToList();
    }
    set { }
  }

  public void FromEntity(domain.Professional entity)
  {
    Document = entity.Document.Value;
    Email = entity.Email;
    Name = entity.Name;
    ProfessionalDocument = entity.ProfessionalDocument;
    Patients = entity.Patients?.Select(Patient.From).ToList();
  }

  public static Professional From(domain.Professional entity)
  {
    return new Professional()
    {
      Document = entity.Document.Value,
      Email = entity.Email,
      Name = entity.Name,
      ProfessionalDocument = entity.ProfessionalDocument,
      Patients = entity.Patients?.Select(Patient.From).ToList(),
    };
  }

  public domain.Professional ToEntity()
  {
    return new domain.Professional(
      ProfessionalDocument,
      Name,
      Email,
      new Cpf(Document),
      Patients?.Select(p => p.ToEntity()).ToList(),
      Id.ToString());
  }

}