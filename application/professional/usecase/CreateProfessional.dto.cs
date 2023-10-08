namespace application.professional;
public record CreateProfessionalInputDto(
  string Name,
  string Email,
  string Document,
  string ProfessionalDocument
);

public record CreateProfessionalOutputDto(
  string Id,
  string Name,
  string Email,
  string Document,
  string ProfessionalDocument
);