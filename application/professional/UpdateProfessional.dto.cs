namespace application.professional;
public record UpdateProfessionalInputDto(
  string Id,
  string Name,
  string Email,
  string ProfessionalDocument,
  UpdateProfessionalPatientInputDto[] Patients
);

public record UpdateProfessionalPatientInputDto(
  string Id,
  string Name,
  string Email,
  string Document,
  bool IsActive
);

public record UpdateProfessionalOutputDto(
  string Id,
  string Name,
  string Email,
  string Document,
  string ProfessionalDocument,
  UpdateProfessionalPatientOutputDto[] Patients
);

public record UpdateProfessionalPatientOutputDto(
  string Id,
  string Name,
  string Email,
  string Document,
  bool IsActive
);