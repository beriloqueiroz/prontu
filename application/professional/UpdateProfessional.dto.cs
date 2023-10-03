namespace application.professional;
public record UpdateProfessionalInputDto(
  string Id,
  string Name,
  string Email,
  string ProfessionalDocument
);

public record UpdateProfessionalOutputDto(
  string Id,
  string Name,
  string Email,
  string Document,
  string ProfessionalDocument
);