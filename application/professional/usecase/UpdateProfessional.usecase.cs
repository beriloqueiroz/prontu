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
    Professional? professional;
    try
    {
      professional = ProfessionalGateway.Find(input.Id);
    }
    catch (Exception e)
    {
      throw new ApplicationException("UpdateProfessionalUseCase: Erro ao buscar", e);
    }
    if (professional == null)
    {
      throw new ApplicationException("UpdateProfessionalUseCase: Profissional não encontrado");
    }
    professional.ChangeEmail(input.Email);
    professional.ChangeName(input.Name);
    professional.ChangeProfessionalDocument(input.ProfessionalDocument);

    try
    {
      ProfessionalGateway.Update(professional);
    }
    catch (Exception e)
    {
      throw new ApplicationException("UpdateProfessionalUseCase: Erro ao atualizar", e);
    }
    return new ProfessionalDefaultDto(professional.Id.ToString(), input.Name, input.Email, professional.Document.Value, input.ProfessionalDocument, null);
  }
}

public record UpdateProfessionalInputDto(
  string Id,
  string Name,
  string Email,
  string ProfessionalDocument
);