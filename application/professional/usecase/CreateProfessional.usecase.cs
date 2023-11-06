using domain;

namespace application.professional;

public class CreateProfessionalUseCase : ICreateProfessionalUseCase
{
  private readonly IProfessionalGateway ProfessionalGateway;
  public CreateProfessionalUseCase(IProfessionalGateway professionalGateway)
  {
    ProfessionalGateway = professionalGateway;
  }

  public CreateProfessionalOutputDto Execute(CreateProfessionalInputDto input)
  {
    VerifyIfProfessionalExists(input.Document, input.Email);

    Cpf cpf = new(input.Document);
    Professional professional = new(new(input.ProfessionalDocument, input.ProfessionalDocumentInstitution), input.Name, input.Email, cpf, new List<Patient>(), null);

    CreateProfessional(professional);

    return new CreateProfessionalOutputDto(professional.Id.ToString(), input.Name, input.Email, cpf.Value, input.ProfessionalDocument, input.ProfessionalDocumentInstitution);
  }

  private void VerifyIfProfessionalExists(string document, string email)
  {
    bool exists;
    try
    {
      exists = ProfessionalGateway.IsExists(document, email);
    }
    catch (Exception e)
    {
      throw new ApplicationException("CreateProfessionalUseCase: Erro ao verificar se existe", e);
    }
    if (exists)
    {
      throw new ApplicationException("CreateProfessionalUseCase: Já existe um profissional cadastrado");
    }
  }

  private void CreateProfessional(Professional professional)
  {
    try
    {
      ProfessionalGateway.Create(professional);
    }
    catch (Exception e)
    {
      throw new ApplicationException("CreateProfessionalUseCase: Erro ao criar", e);
    }
  }
}

public record CreateProfessionalInputDto(
  string Name,
  string Email,
  string Document,
  string ProfessionalDocument,
  string ProfessionalDocumentInstitution
);

public record CreateProfessionalOutputDto(
  string Id,
  string Name,
  string Email,
  string Document,
  string ProfessionalDocument,
  string ProfessionalDocumentInstitution
);