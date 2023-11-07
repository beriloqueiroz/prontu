using application.professional;

namespace infrastructure.controller;

public record AddPatientControllerInputDto(
  string Name,
  string Email,
  string Document,
  List<PhoneDto>? Phones
);

public record AddPatientControllerOutputDto(
  string Id,
  string Name,
  string Email,
  string Document,
  string ProfessionalDocument,
  PatientDefaultDto[] Patients
);

public record UpdateProfessionalControllerInputDto(
  string Name,
  string Email,
  string Document,
  string ProfessionalDocument,
  string ProfessionalDocumentInstitution
);

public record ChangeProfessionalEmailControllerInputDto(
  string Email
);

public record UpdateProfessionalControllerOutputDto(
  string Id,
  string Name,
  string Email,
  string Document,
  string ProfessionalDocument,
  string ProfessionalDocumentInstitution
);

public record UpdatePatientControllerInputDto(
  string Name,
  string Email,
  string Document,
  bool IsActive,
  PatientFinancialInfoDefaultDto? FinancialInfo,
  PatientPersonalFormDefaultDto? PersonalForm,
  List<PhoneDto>? Phones,
  string? Avatar
);