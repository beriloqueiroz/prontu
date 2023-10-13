namespace application.professional;
public record ChangePatientFinancialInfoInputDto(
  string ProfessionalId,
  string PatientId,
  string DefaultPrice,
  string EstimatedSessionsByWeek,
  string EstimatedTimeSessionInMinutes,
  string SessionType
);

public record ChangePatientFinancialInfoOutputDto(
);

