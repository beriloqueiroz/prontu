using domain;

namespace application.professional;
public record PatientDefaultDto(
  string Id,
  string Name,
  string Email,
  string Document,
  bool IsActive,
  PatientFinancialInfoDefaultDto? FinancialInfo,
  PatientPersonalFormDefaultDto? PersonalForm,
  List<PhoneDto>? Phones,
  string? Avatar

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

public record PhoneDto(string Value, bool? IsChat)
{
  public Phone ToEntity()
  {
    return new(Value, IsChat);
  }

  public static List<Phone> ToEntityList(List<PhoneDto>? phoneDtoList)
  {
    if (phoneDtoList == null || phoneDtoList.Count == 0) return new List<Phone>();
    return phoneDtoList.Select(ph => new Phone(ph.Value, ph.IsChat)).ToList();
  }

  public static List<PhoneDto> ByEntityList(List<Phone>? phoneList)
  {
    if (phoneList == null || phoneList.Count == 0) return new List<PhoneDto>();
    return phoneList.Select(ph => new PhoneDto(ph.Value, ph.IsChat)).ToList();
  }
}
