namespace infrastructure.repository;

public class ProfessionalPatient : Model
{
  public required Guid ProfessionalId { get; set; }
  public required Guid PatientId { get; set; }
  public decimal? DefaultPrice { get; set; }
  public int? EstimatedSessionsByWeek { get; set; }
  public int? EstimatedTimeSessionInMinutes { get; set; }
  public string? SessionType { get; set; }

  public bool IsFinancialInfoComplete()
  {
    return
      DefaultPrice != null &&
      EstimatedSessionsByWeek != null &&
      EstimatedTimeSessionInMinutes != null &&
      SessionType != null;
  }
}