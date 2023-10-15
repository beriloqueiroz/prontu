using domain;

namespace application.professional;

public class FindProfessionalUseCase : IFindProfessionalUseCase
{
  private readonly IProfessionalGateway ProfessionalGateway;
  public FindProfessionalUseCase(IProfessionalGateway professionalGateway)
  {
    ProfessionalGateway = professionalGateway;
  }

  public ProfessionalDefaultDto Execute(FindProfessionalInputDto input)
  {
    Professional? professional = null;
    try
    {
      professional = ProfessionalGateway.Find(input.Id);
    }
    catch (Exception e)
    {
      throw new ApplicationException("FindProfessionalUseCase: Erro ao buscar profissional", e);
    }

    if (professional == null)
    {
      throw new ApplicationException("FindProfessionalUseCase: Profissional não encontrado");
    }

    return new ProfessionalDefaultDto(
      professional.Id.ToString(),
      professional.Name,
      professional.Email,
      professional.Document.Value,
      professional.ProfessionalDocument,
      professional.Patients?.Select(pat =>
        new PatientDefaultDto(pat.Id.ToString(), pat.Name, pat.Email, pat.Document.Value, pat.Active, null, null))
      .ToArray() ?? Array.Empty<PatientDefaultDto>());
  }
}

public record FindProfessionalInputDto(
  string Id
);