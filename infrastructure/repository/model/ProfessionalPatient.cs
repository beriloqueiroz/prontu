using domain;

namespace infrastructure.repository;

public class ProfessionalPatient : Model
{
  public required Guid ProfessionalId { get; set; }
  public required Guid PatientId { get; set; }
  public decimal? DefaultSessionPrice { get; set; }
  public int? EstimatedSessionsByWeek { get; set; }
  public int? EstimatedTimeSessionInMinutes { get; set; }
  public SessionType? SessionType { get; set; }
  public PaymentType? PaymentType { get; internal set; }
  public int? PaymentPeriodInDays { get; internal set; }
  public int? SessionQuantityPerPayment { get; internal set; }
  public bool Active { get; set; }


}