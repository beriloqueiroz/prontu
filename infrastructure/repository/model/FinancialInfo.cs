namespace infrastructure.repository;

public class FinancialInfo : Model
{
  public required Patient Patient { get; set; }
  public required Professional Professional { get; set; }

  public decimal? DefaultPrice { get; set; }
  public int? EstimatedSessionsByWeek { get; set; }
  public int? EstimatedTimeSessionInMinutes { get; set; }
  public string? SessionType { get; set; }
}