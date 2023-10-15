using domain;

namespace application.professional;

public class AddPatientUseCase : IAddPatientUseCase
{
  private readonly IProfessionalGateway ProfessionalGateway;
  public AddPatientUseCase(IProfessionalGateway professionalGateway)
  {
    ProfessionalGateway = professionalGateway;
  }

  public ProfessionalDefaultDto Execute(AddPatientInputDto input)
  {
    Professional? professional;
    try
    {
      professional = ProfessionalGateway.Find(input.ProfessionalId);
    }
    catch (Exception e)
    {
      throw new ApplicationException("AddPatientUseCase: Erro ao buscar", e);
    }
    if (professional == null)
    {
      throw new ApplicationException("AddPatientUseCase: Profissional não encontrado");
    }
    Patient patient = new(input.Name, input.Email, new Cpf(input.Document), null);
    professional.AddPatient(patient);
    try
    {
      ProfessionalGateway.AddPatient(patient);
    }
    catch (Exception e)
    {
      throw new ApplicationException("AddPatientUseCase: Erro ao adicionar", e);
    }
    PatientDefaultDto[]? addPatientsOutputDto = professional.Patients?.Select(pat =>
      new PatientDefaultDto(pat.Id.ToString(), pat.Name, pat.Email, pat.Document.Value, pat.IsActive(), null, null)).ToArray();
    return new ProfessionalDefaultDto(professional.Id.ToString(), input.Name, input.Email, professional.Document.Value, professional.ProfessionalDocument, addPatientsOutputDto ?? Array.Empty<PatientDefaultDto>());
  }
}

public record AddPatientInputDto(
  string ProfessionalId,
  string Name,
  string Email,
  string Document
);

