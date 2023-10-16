using System.ComponentModel.DataAnnotations.Schema;

namespace infrastructure.repository;

public class PersonalForm : Model
{
  public string? Street { get; set; }
  public string? Neighborhood { get; set; }
  public string? City { get; set; }
  public string? Number { get; set; }
  public string? Country { get; set; }
  public string? ZipCode { get; set; }
  public string? Region { get; set; }
  public string? Contact { get; set; }
  public string? Phones { get; set; }
  public string? OthersInfos { get; set; }
  public string? Observations { get; set; }

  public Patient? Patient { get; set; }

  [ForeignKey("Patient")]
  public Guid? PatientId { get; set; }
}