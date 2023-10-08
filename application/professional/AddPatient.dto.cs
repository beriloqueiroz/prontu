namespace application.professional;
public record AddPatientInputDto(
  string ProfessionalId,
  string Name,
  string Email,
  string Document
);

public record AddPatientOutputDto(
  string Id,
  string Name,
  string Email,
  string Document,
  string ProfessionalDocument,
  AddPatientsOutputDto[] Patients
);

public record AddPatientsOutputDto(
  string Id,
  string Name,
  string Email,
  string Document,
  bool IsActive
);