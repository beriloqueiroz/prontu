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
    Professional? professional = Find(input.ProfessionalId);

    Patient patient = new(input.Name, input.Email, new Cpf(input.Document), null);

    if (input.Phones != null && input.Phones.Count > 0)
    {
      foreach (var phone in input.Phones)
      {
        patient.AddPhone(phone.ToEntity());
      }
    }

    patient.Activate();

    professional.AddPatient(patient);

    AddPatient(patient, input.ProfessionalId);

    PatientDefaultDto[]? addPatientsOutputDto = professional.Patients?.Select(pat =>
      new PatientDefaultDto(pat.Id.ToString(), pat.Name, pat.Email, pat.Document.Value, pat.IsActive(), null, null, PhoneDto.ByEntityList(pat.Phones), pat.Avatar?.Value)).ToArray();

    return new ProfessionalDefaultDto(
      professional.Id.ToString(),
      input.Name,
      input.Email,
      professional.Document.Value,
      professional.ProfessionalDocument.Value,
      professional.ProfessionalDocument.Institution,
      addPatientsOutputDto ?? Array.Empty<PatientDefaultDto>());
  }

  private Professional Find(string id)
  {
    Professional? professional;
    try
    {
      professional = ProfessionalGateway.Find(id);
    }
    catch (Exception e)
    {
      throw new ApplicationException("AddPatientUseCase: Erro ao buscar", e);
    }
    if (professional == null)
    {
      throw new ApplicationException("AddPatientUseCase: Profissional não encontrado");
    }
    return professional;
  }
  private void AddPatient(Patient patient, string professionalId)
  {
    try
    {
      ProfessionalGateway.AddPatient(patient, professionalId);
    }
    catch (Exception e)
    {
      throw new ApplicationException("AddPatientUseCase: Erro ao adicionar", e);
    }
  }
}

public record AddPatientInputDto(
  string ProfessionalId,
  string Name,
  string Email,
  string Document,
  List<PhoneDto>? Phones
);
