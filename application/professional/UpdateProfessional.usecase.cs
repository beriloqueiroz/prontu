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

    input.Patients.ToList().ForEach(inputPatient =>
    {
      professional.Patients.ForEach(patient =>
      {
        if (inputPatient.Id.Equals(patient.Id.ToString()))
        {
          patient.ChangeEmail(inputPatient.Email);
          patient.ChangeName(inputPatient.Name);
        }
      });
    });

    UpdateProfessionalPatientOutputDto[] patientsOutput = professional.Patients.Select(patient =>
      new UpdateProfessionalPatientOutputDto(patient.Id.ToString(), patient.Name, patient.Email, patient.Document.Value, patient.IsActive())).ToArray();
    ProfessionalGateway.Update(professional);
    return new UpdateProfessionalOutputDto(professional.Id.ToString(), input.Name, input.Email, professional.Document.Value, input.ProfessionalDocument, patientsOutput);
  }
}