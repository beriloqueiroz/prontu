namespace application.professional;

public record ProfessionalDefaultDto(
  string Id,
  string Name,
  string Email,
  string Document,
  string ProfessionalDocument,
  PatientDefaultDto[]? Patients
);