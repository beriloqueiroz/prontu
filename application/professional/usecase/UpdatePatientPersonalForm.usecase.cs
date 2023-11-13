using domain;

namespace application.professional;

public class UpdatePatientPersonalFormUseCase : IUpdatePatientPersonalFormUseCase
{
  private readonly IProfessionalGateway ProfessionalGateway;
  public UpdatePatientPersonalFormUseCase(IProfessionalGateway professionalGateway)
  {
    ProfessionalGateway = professionalGateway;
  }

  public PatientDefaultDto Execute(UpdatePatientPersonalFormInputDto input)
  {

    Professional professional = FindProfessional(input.ProfessionalId);

    Patient patient = FindPatient(input.PatientId, input.ProfessionalId);

    patient.ChangePersonalForm(
      new()
      {
        Street = input.Street,
        Neighborhood = input.Neighborhood,
        City = input.City,
        Number = input.Number,
        Country = input.Country,
        ZipCode = input.ZipCode,
        Region = input.Region,
        Contact = input.Contact,
        Phones = input.Phones,
        OthersInfos = input.OthersInfos,
        Observations = input.Observations,
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
      ) : null, PhoneDto.ByEntityList(patient.Phones), patient.Avatar?.Value);
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
      throw new ApplicationException("UpdatePatientPersonalFormUseCase: Erro ao buscar paciente", e);
    }

    if (professional == null)
    {
      throw new ApplicationException("UpdatePatientPersonalFormUseCase: Paciente não encontrado");
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
      throw new ApplicationException("UpdatePatientPersonalFormUseCase: Erro ao buscar paciente", e);
    }

    if (patient == null)
    {
      throw new ApplicationException("UpdatePatientPersonalFormUseCase: Paciente não encontrado");
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
      throw new ApplicationException("UpdatePatientPersonalFormUseCase: Erro ao atualizar", e);
    }
  }
}

public record UpdatePatientPersonalFormInputDto(
  string ProfessionalId,
  string PatientId,
  string Street,
  string Neighborhood,
  string City,
  string Number,
  string Country,
  string ZipCode,
  string Region,
  string Contact,
  string Phones,
  string OthersInfos,
  string Observations
);