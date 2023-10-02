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
    Cpf cpf = new(input.Document);
    Professional professional = new(input.ProfessionalDocument, input.Name, input.Email, cpf, new List<Patient>(), null);
    ProfessionalGateway.Create(professional);
    return new CreateProfessionalOutputDto("", input.Name, input.Email, cpf.Value, input.ProfessionalDocument);
  }
}