using domain;

namespace application.professional;

public class CreateProfessionalUseCase : IUsecase<CreateProfessionalInputDto, CreateProfessionalOutputDto>
{
  private readonly IProfessionalGateway ProfessionalGateway;
  public CreateProfessionalUseCase(IProfessionalGateway professionalGateway)
  {
    ProfessionalGateway = professionalGateway;
  }

  public CreateProfessionalOutputDto Execute(CreateProfessionalInputDto input)
  {
    bool exists;
    try
    {
      exists = ProfessionalGateway.IsExists(input.Document, input.Email);
    }
    catch (Exception e)
    {
      throw new ApplicationException("CreateProfessionalUseCase: Erro ao verificar se existe", e);
    }
    if (exists)
    {
      throw new ApplicationException("CreateProfessionalUseCase: Já existe um profissional cadastrado");
    }
    Cpf cpf = new(input.Document);
    Professional professional = new(input.ProfessionalDocument, input.Name, input.Email, cpf, new List<Patient>(), null);
    try
    {
      ProfessionalGateway.Create(professional);
    }
    catch (Exception e)
    {
      throw new ApplicationException("CreateProfessionalUseCase: Erro ao criar", e);
    }
    return new CreateProfessionalOutputDto(professional.Id.ToString(), input.Name, input.Email, cpf.Value, input.ProfessionalDocument);
  }
}