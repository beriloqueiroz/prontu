using domain;

namespace application.professional;

public class FindPatientUseCase : IFindPatientUseCase
{
  private readonly IProfessionalGateway ProfessionalGateway;
  public FindPatientUseCase(IProfessionalGateway patientGateway)
  {
    ProfessionalGateway = patientGateway;
  }

  public PatientDefaultDto Execute(FindPatientInputDto input)
  {
    Patient? patient;
    try
    {
      patient = ProfessionalGateway.FindPatient(input.PatientId, input.ProfessionalId);
    }
    catch (Exception e)
    {
      throw new ApplicationException("FindPatientUseCase: Erro ao buscar paciente", e);
    }

    if (patient == null)
    {
      throw new ApplicationException("FindPatientUseCase: Paciente não encontrado");
    }

    return new PatientDefaultDto(
      patient.Id.ToString(),
      patient.Name,
      patient.Email,
      patient.Document.Value,
      patient.IsActive(),
      patient.FinancialInfo != null ? new(
        patient.FinancialInfo.DefaultSessionPrice,
        patient.FinancialInfo.EstimatedSessionsByWeek,
        patient.FinancialInfo.EstimatedTimeSessionInMinutes,
        patient.FinancialInfo.SessionType,
        patient.FinancialInfo.PaymentType,
        patient.FinancialInfo.PaymentPeriodInDays,
        patient.FinancialInfo.SessionQuantityPerPayment) : null,
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

public record FindPatientInputDto(
  string PatientId,
  string ProfessionalId
);