using System.ComponentModel.DataAnnotations.Schema;

namespace infrastructure.repository;

public class ProfessionalPatient : Model
{

  public ProfessionalPatient() { }

  [ForeignKey("Professional")]
  public Guid ProfessionalId { get; set; }
  public required Professional Professional { get; set; }

  [ForeignKey("Patient")]
  public Guid PatientId { get; set; }
  public required Patient Patient { get; set; }
}