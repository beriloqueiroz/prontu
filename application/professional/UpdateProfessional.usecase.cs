using domain;

namespace application.professional;

public class UpdateProfessionalUseCase : IUsecase<UpdateProfessionalInputDto, UpdateProfessionalOutputDto>
{
  private readonly IProfessionalGateway ProfessionalGateway;
  public UpdateProfessionalUseCase(IProfessionalGateway professionalGateway)
  {
    ProfessionalGateway = professionalGateway;
  }

  public UpdateProfessionalOutputDto Execute(UpdateProfessionalInputDto input)
  {
    Professional professional = ProfessionalGateway.Find(input.Id);
    professional.ChangeEmail(input.Email);
    professional.ChangeName(input.Name);
    professional.ChangeProfessionalDocument(input.ProfessionalDocument);
    ProfessionalGateway.Update(professional);
    return new UpdateProfessionalOutputDto("", input.Name, input.Email, professional.Document.Value, input.ProfessionalDocument);
  }
}