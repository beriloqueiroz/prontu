using System.ComponentModel.DataAnnotations.Schema;
using domain;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repository;

[Index(nameof(Email), nameof(Document))]
public class Patient : Model
{
  public Patient()
  {

  }
  public required string Name { get; set; }
  public required string Email { get; set; }
  public required string Document { get; set; }
  public bool Active { get; set; }

  public IList<ProfessionalPatient>? ProfessionalPatients { get; set; }

  [NotMapped]
  public IList<Professional>? Professionals
  {
    get
    {
      return ProfessionalPatients?.Select(pp => pp.Professional).ToList();
    }
    set { }
  }

  public void FromEntity(domain.Patient entity)
  {
    Id = entity.Id;
    Document = entity.Document.Value;
    Email = entity.Email;
    Name = entity.Name;
    ProfessionalPatients = null;
  }

  public static Patient From(domain.Patient entity)
  {
    return new Patient()
    {
      Id = entity.Id,
      Document = entity.Document.Value,
      Email = entity.Email,
      Name = entity.Name,
      ProfessionalPatients = null
    };
  }

  public domain.Patient ToEntity()
  {
    return new domain.Patient(Name, Email, new Cpf(Document), Id.ToString());
  }

}