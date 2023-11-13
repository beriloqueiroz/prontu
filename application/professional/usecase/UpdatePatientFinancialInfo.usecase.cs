using domain;

namespace application.professional;

public class UpdatePatientFinancialInfoUseCase : IUpdatePatientFinancialInfoUseCase
{
  private readonly IProfessionalGateway ProfessionalGateway;
  public UpdatePatientFinancialInfoUseCase(IProfessionalGateway professionalGateway)
  {
    ProfessionalGateway = professionalGateway;
  }

  public PatientDefaultDto Execute(UpdatePatientFinancialInfoInputDto input)
  {
    Professional professional = FindProfessional(input.ProfessionalId);

    Patient patient = FindPatient(input.PatientId, input.ProfessionalId);

    patient.ChangeFinancialInfo(new()
    {
      DefaultSessionPrice = input.DefaultSessionPrice,
      EstimatedSessionsByWeek = input.EstimatedSessionsByWeek,
      EstimatedTimeSessionInMinutes = input.EstimatedTimeSessionInMinutes,
      SessionType = input.SessionType,
      PaymentType = input.PaymentType,
      PaymentPeriodInDays = input.PaymentPeriodInDays,
      SessionQuantityPerPayment = input.SessionQuantityPerPayment
    });

    professional.ChangePatient(patient);

    UpdatePatient(patient, input.ProfessionalId);

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
      ) : null, PhoneDto.ByEntityList(patient.Phones), patient.Avatar?.Value);
  }

  private Patient FindPatient(string patientId, string professionalId)
  {
    Patient? patient;
    try
    {
      patient = ProfessionalGateway.FindPatient(patientId, professionalId);
    }
    catch (Exception e)
    {
      throw new ApplicationException("UpdatePatientFinancialInfoUseCase: Erro ao buscar paciente", e);
    }

    if (patient == null)
    {
      throw new ApplicationException("UpdatePatientFinancialInfoUseCase: Paciente não encontrado");
    }
    return patient;
  }

  private void UpdatePatient(Patient patient, string professionalId)
  {
    try
    {
      ProfessionalGateway.UpdatePatient(patient, professionalId);
    }
    catch (Exception e)
    {
      throw new ApplicationException("UpdatePatientFinancialInfoUseCase: Erro ao atualizar", e);
    }
  }

  private Professional FindProfessional(string professionalId)
  {
    Professional? professional;
    try
    {
      professional = ProfessionalGateway.Find(professionalId);
    }
    catch (Exception e)
    {
      throw new ApplicationException("UpdatePatientFinancialInfoUseCase: Erro ao buscar paciente", e);
    }

    if (professional == null)
    {
      throw new ApplicationException("UpdatePatientFinancialInfoUseCase: Paciente não encontrado");
    }
    return professional;
  }
}


public record UpdatePatientFinancialInfoInputDto(
  string ProfessionalId,
  string PatientId,
  decimal DefaultSessionPrice,
  int EstimatedSessionsByWeek,
  int EstimatedTimeSessionInMinutes,
  SessionType SessionType,
  PaymentType PaymentType,
  int? PaymentPeriodInDays,
  int? SessionQuantityPerPayment
);