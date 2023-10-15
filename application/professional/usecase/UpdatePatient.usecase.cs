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
      throw new ApplicationException("UpdatePatientUseCase: Erro ao buscar profissional", e);
    }

    if (professional == null)
    {
      throw new ApplicationException("UpdatePatientUseCase: Profissional não encontrado");
    }

    Patient? patient;
    try
    {
      patient = ProfessionalGateway.FindPatient(input.PatientId, input.ProfessionalId);
    }
    catch (Exception e)
    {
      throw new ApplicationException("FindPatientUseCase: Erro ao buscar profissional", e);
    }

    if (patient == null)
    {
      throw new ApplicationException("FindPatientUseCase: Paciente não encontrado");
    }

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