using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repository;

[Index(nameof(Email), nameof(Document))]
public class Professional : Model
{
  public required string ProfessionalDocument { get; set; }
  public required string Name { get; set; }
  public required string Email { get; set; }
  public required string Document { get; set; }
  public required IList<ProfessionalPatient> ProfessionalPatients { get; set; }

  [NotMapped]
  public IList<Patient> Patients
  {
    get
    {
      return ProfessionalPatients.Select(pp => pp.Patient).ToList();
    }
  }
}