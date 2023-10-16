namespace infrastructure.repository;

public class FinancialInfo : Model
{
  public required Guid PatientId { get; set; }
  public required Guid ProfessionalId { get; set; }

  public decimal? DefaultPrice { get; set; }
  public int? EstimatedSessionsByWeek { get; set; }
  public int? EstimatedTimeSessionInMinutes { get; set; }
  public string? SessionType { get; set; }
}