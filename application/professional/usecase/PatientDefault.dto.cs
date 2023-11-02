using domain;

namespace application.professional;
public record PatientDefaultDto(
  string Id,
  string Name,
  string Email,
  string Document,
  bool IsActive,
  PatientFinancialInfoDefaultDto? FinancialInfo,
  PatientPersonalFormDefaultDto? PersonalForm
);

public record PatientFinancialInfoDefaultDto(
  decimal DefaultSessionPrice,
  int EstimatedSessionsByWeek,
  int EstimatedTimeSessionInMinutes,
  SessionType SessionType,
  PaymentType PaymentType,
  int? PaymentPeriodInDays,
  int? SessionQuantityPerPayment
);

public record PatientPersonalFormDefaultDto(
  string? Street,
  string? Neighborhood,
  string? City,
  string? Number,
  string? Country,
  string? ZipCode,
  string? Region,
  string? Contact,
  string? Phones,
  string? OthersInfos,
  string? Observations
);