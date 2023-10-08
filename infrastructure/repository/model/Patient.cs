using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repository;

[Index(nameof(Email), nameof(Document))]
public class Patient : Model
{
  public required string Name { get; set; }
  public required string Email { get; set; }
  public required string Document { get; set; }
  public bool Active { get; set; }

  public required IList<ProfessionalPatient> ProfessionalPatients { get; set; }

  [NotMapped]
  public IList<Professional> Professionals
  {
    get
    {
      return ProfessionalPatients.Select(pp => pp.Professional).ToList();
    }
  }
}