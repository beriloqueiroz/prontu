using domain;

namespace application.professional;

public class UpdatePatientUseCase : IUpdatePatientUseCase
{
  private readonly IProfessionalGateway ProfessionalGateway;
  public UpdatePatientUseCase(IProfessionalGateway professionalGateway)
  {
    ProfessionalGateway = professionalGateway;
  }

  public PatientDefaultDto Execute(UpdatePatientInputDto input)
  {
    Professional? professional;
    try
    {
      professional = ProfessionalGateway.Find(input.ProfessionalId);
    }
    catch (Exception e)
    {
      throw new ApplicationException("UpdatePatientUseCase: Erro ao buscar paciente", e);
    }

    if (professional == null)
    {
      throw new ApplicationException("UpdatePatientUseCase: Paciente não encontrado");
    }

    Patient? patient;
    try
    {
      patient = ProfessionalGateway.FindPatient(input.PatientId, input.ProfessionalId);
    }
    catch (Exception e)
    {
      throw new ApplicationException("UpdatePatientUseCase: Erro ao buscar paciente", e);
    }

    if (patient == null)
    {
      throw new ApplicationException("UpdatePatientUseCase: Paciente não encontrado");
    }


    patient.ChangeEmail(input.Email);
    if (input.FinancialInfo != null) patient.ChangeFinancialInfo(new()
    {
      DefaultPrice = input.FinancialInfo.DefaultPrice,
      EstimatedSessionsByWeek = input.FinancialInfo.EstimatedSessionsByWeek,
      EstimatedTimeSessionInMinutes = input.FinancialInfo.EstimatedTimeSessionInMinutes,
      SessionType = input.FinancialInfo.SessionType
    });
    patient.ChangeName(patient.Name);
    if (input.PersonalForm != null) patient.ChangePersonalForm(
      new()
      {
        Street = input.PersonalForm.Street,
        Neighborhood = input.PersonalForm.Neighborhood,
        City = input.PersonalForm.City,
        Number = input.PersonalForm.Number,
        Country = input.PersonalForm.Country,
        ZipCode = input.PersonalForm.ZipCode,
        Region = input.PersonalForm.Region,
        Contact = input.PersonalForm.Contact,
        Phones = input.PersonalForm.Phones,
        OthersInfos = input.PersonalForm.OthersInfos,
        Observations = input.PersonalForm.Observations,
      }
    );

    professional.ChangePatient(patient);

    try
    {
      ProfessionalGateway.UpdatePatient(patient, input.ProfessionalId);
    }
    catch (Exception e)
    {
      throw new ApplicationException("UpdatePatientUseCase: Erro ao atualizar", e);
    }

    return new PatientDefaultDto(
      patient.Id.ToString(),
      patient.Name,
      patient.Email,
      patient.Document.Value,
      patient.IsActive(),
      patient.FinancialInfo != null ? new(
        patient.FinancialInfo.DefaultPrice,
        patient.FinancialInfo.EstimatedSessionsByWeek,
        patient.FinancialInfo.EstimatedTimeSessionInMinutes,
        patient.FinancialInfo.SessionType) : null,
      patient.PersonalForm != null ? new(
        patient.PersonalForm?.Street,
        patient.PersonalForm?.Neighborhood,
        patient.PersonalForm?.City,
        patient.PersonalForm?.Number,
        patient.PersonalForm?.Country,
        patient.PersonalForm?.ZipCode,
        patient.PersonalForm?.Region,
        patient.PersonalForm?.Contact,
        patient.PersonalForm?.Phones,
        patient.PersonalForm?.OthersInfos,
        patient.PersonalForm?.Observations
      ) : null);
  }
}

public record UpdatePatientInputDto(
  string ProfessionalId,
  string PatientId,
  string Name,
  string Email,
  string Document,
   bool IsActive,
  PatientFinancialInfoDefaultDto? FinancialInfo,
  PatientPersonalFormDefaultDto? PersonalForm
);