using domain;

namespace application.professional;

public class FindProfessionalUseCase : IUsecase<FindProfessionalInputDto, FindProfessionalOutputDto>
{
  private readonly IProfessionalGateway ProfessionalGateway;
  public FindProfessionalUseCase(IProfessionalGateway professionalGateway)
  {
    ProfessionalGateway = professionalGateway;
  }

  public FindProfessionalOutputDto Execute(FindProfessionalInputDto input)
  {
    Professional professional = ProfessionalGateway.Find(input.Id);
    return new FindProfessionalOutputDto(
      professional.Id.ToString(),
      professional.Name,
      professional.Email,
      professional.Document.Value,
      professional.ProfessionalDocument,
      professional.Patients.Select(pat =>
        new FindProfessionalPatientOutputDto(pat.Id.ToString(), pat.Name, pat.Email, pat.Document.Value, pat.Active))
      .ToArray());
  }
}