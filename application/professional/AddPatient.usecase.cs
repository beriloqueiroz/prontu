using domain;

namespace application.professional;

public class AddPatientUseCase : IUsecase<AddPatientInputDto, AddPatientOutputDto>
{
  private readonly IProfessionalGateway ProfessionalGateway;
  public AddPatientUseCase(IProfessionalGateway professionalGateway)
  {
    ProfessionalGateway = professionalGateway;
  }

  public AddPatientOutputDto Execute(AddPatientInputDto input)
  {
    Professional professional = ProfessionalGateway.Find(input.ProfessionalId);
    Patient patient = new(input.Name, input.Email, new Cpf(input.Document), null);
    professional.AddPatient(patient);
    ProfessionalGateway.Update(professional);
    AddPatientsOutputDto[] addPatientsOutputDto = professional.Patients.Select(pat =>
      new AddPatientsOutputDto(pat.Id.ToString(), pat.Name, pat.Email, pat.Document.Value, pat.IsActive())).ToArray();
    return new AddPatientOutputDto(professional.Id.ToString(), input.Name, input.Email, professional.Document.Value, professional.ProfessionalDocument, addPatientsOutputDto);
  }
}