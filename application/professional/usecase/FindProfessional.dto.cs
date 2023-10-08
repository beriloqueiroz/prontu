namespace application.professional;
public record FindProfessionalInputDto(
  string Id
);

public record FindProfessionalOutputDto(
  string Id,
  string Name,
  string Email,
  string Document,
  string ProfessionalDocument,
  FindProfessionalPatientOutputDto[] Patients
);

public record FindProfessionalPatientOutputDto(
  string Id,
  string Name,
  string Email,
  string Document,
  bool IsActive
);