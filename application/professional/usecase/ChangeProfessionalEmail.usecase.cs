using domain;

namespace application.professional;

public class ChangeProfessionalEmailUseCase : IChangeProfessionalEmailUsecase
{
  private readonly IProfessionalGateway ProfessionalGateway;
  public ChangeProfessionalEmailUseCase(IProfessionalGateway professionalGateway)
  {
    ProfessionalGateway = professionalGateway;
  }
  public ProfessionalDefaultDto Execute(ChangeProfessionalEmailInputDto input)
  {
    Professional professional = FindProfessional(input.Id);

    professional.ChangeEmail(input.Email);

    UpdateProfessional(professional);

    return new ProfessionalDefaultDto(professional.Id.ToString(), professional.Name, input.Email, professional.Document.Value, professional.ProfessionalDocument.Value, professional.ProfessionalDocument.Institution, null);

  }

  private void UpdateProfessional(Professional professional)
  {
    try
    {
      ProfessionalGateway.Update(professional);
    }
    catch (Exception e)
    {
      throw new ApplicationException("ChangeProfessionalEmailUseCase: Erro ao atualizar", e);
    }
  }

  private Professional FindProfessional(string professionalId)
  {
    Professional? professional;
    try
    {
      professional = ProfessionalGateway.Find(professionalId);
    }
    catch (Exception e)
    {
      throw new ApplicationException("ChangeProfessionalEmailUseCase: Erro ao buscar", e);
    }
    if (professional == null)
    {
      throw new ApplicationException("ChangeProfessionalEmailUseCase: Profissional não encontrado");
    }
    return professional;
  }
}

public record ChangeProfessionalEmailInputDto(
  string Id,
  string Email
);