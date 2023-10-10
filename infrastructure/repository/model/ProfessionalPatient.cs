using System.ComponentModel.DataAnnotations.Schema;

namespace infrastructure.repository;

public class ProfessionalPatient : Model
{
  [ForeignKey("Professional")]
  public required Guid ProfessionalId { get; set; }

  public Professional? Professional { get; set; }

  [ForeignKey("Patient")]
  public required Guid PatientId { get; set; }

  public Patient? Patient { get; set; }
}