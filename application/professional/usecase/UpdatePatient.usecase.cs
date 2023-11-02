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

    Professional professional = FindProfessional(input.ProfessionalId);

    Patient patient = FindPatient(input.PatientId, input.ProfessionalId);

    patient.ChangeEmail(input.Email);

    if (input.FinancialInfo != null) patient.ChangeFinancialInfo(new()
    {
      DefaultSessionPrice = input.FinancialInfo.DefaultSessionPrice,
      EstimatedSessionsByWeek = input.FinancialInfo.EstimatedSessionsByWeek,
      EstimatedTimeSessionInMinutes = input.FinancialInfo.EstimatedTimeSessionInMinutes,
      SessionType = input.FinancialInfo.SessionType,
      PaymentType = input.FinancialInfo.PaymentType,
      PaymentPeriodInDays = input.FinancialInfo.PaymentPeriodInDays,
      SessionQuantityPerPayment = input.FinancialInfo.SessionQuantityPerPayment
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
      ) : null);
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
      throw new ApplicationException("UpdatePatientUseCase: Erro ao buscar paciente", e);
    }

    if (professional == null)
    {
      throw new ApplicationException("UpdatePatientUseCase: Paciente não encontrado");
    }
    return professional;
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
      throw new ApplicationException("UpdatePatientUseCase: Erro ao buscar paciente", e);
    }

    if (patient == null)
    {
      throw new ApplicationException("UpdatePatientUseCase: Paciente não encontrado");
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
      throw new ApplicationException("UpdatePatientUseCase: Erro ao atualizar", e);
    }
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