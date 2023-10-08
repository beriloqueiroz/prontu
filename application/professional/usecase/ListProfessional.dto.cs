namespace application.professional;
public record ListProfessionalInputDto(
  int PageSize,
  int PageIndex
);

public record ListProfessionalOutputDto(
  string Id,
  string Name,
  string Email,
  string Document,
  string ProfessionalDocument,
  ListProfessionalPatientOutputDto[] Patients
);

public record ListProfessionalPatientOutputDto(
  string Id,
  string Name,
  string Email,
  string Document,
  bool IsActive
);