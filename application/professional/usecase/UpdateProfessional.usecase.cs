using domain;

namespace application.professional;

public class UpdateProfessionalUseCase : IUpdateProfessionalUseCase
{
  private readonly IProfessionalGateway ProfessionalGateway;
  public UpdateProfessionalUseCase(IProfessionalGateway professionalGateway)
  {
    ProfessionalGateway = professionalGateway;
  }

  public ProfessionalDefaultDto Execute(UpdateProfessionalInputDto input)
  {
    Professional professional = FindProfessional(input.Id);

    professional.ChangeEmail(input.Email);
    professional.ChangeName(input.Name);
    professional.ChangeProfessionalDocument(input.ProfessionalDocument);

    UpdateProfessional(professional);

    return new ProfessionalDefaultDto(professional.Id.ToString(), input.Name, input.Email, professional.Document.Value, input.ProfessionalDocument, null);
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
      throw new ApplicationException("UpdateProfessionalUseCase: Erro ao buscar", e);
    }
    if (professional == null)
    {
      throw new ApplicationException("UpdateProfessionalUseCase: Profissional não encontrado");
    }
    return professional;
  }

  private void UpdateProfessional(Professional professional)
  {
    try
    {
      ProfessionalGateway.Update(professional);
    }
    catch (Exception e)
    {
      throw new ApplicationException("UpdateProfessionalUseCase: Erro ao atualizar", e);
    }
  }
}

public record UpdateProfessionalInputDto(
  string Id,
  string Name,
  string Email,
  string ProfessionalDocument
);