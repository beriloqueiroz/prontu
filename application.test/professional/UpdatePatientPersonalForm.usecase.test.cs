using application.professional;
using domain;
using Moq;

namespace application.test;

[TestClass]
public class UpdatePatientPersonalFormUsecaseTest
{

  private readonly Mock<IProfessionalGateway> mock = new();
  private UpdatePatientPersonalFormUseCase? Usecase;

  [TestInitialize]
  public void AssemblyInit()
  {
    Usecase = new(mock.Object);
  }

  [TestMethod]
  public void ShouldBeExecuteUpdatePatientWithoutPersonalFormAndPersonalFormUseCase()
  {
    Patient patient = CreateValidPatient();
    Professional professional = CreateValidProfessional(patient);
    var professionalId = professional.Id.ToString();
    mock.Setup(p => p.Find(professionalId)).Returns(professional);
    mock.Setup(p => p.FindPatient(patient.Id.ToString(), professionalId)).Returns(patient);

    var input = new UpdatePatientPersonalFormInputDto(
      professionalId,
      patient.Id.ToString(),
      "Rua dos bobos",
      "Dos doidos",
      "Fraqueza",
      "0",
      "Nenhum",
      "60000000",
      "Nada",
      "Todos",
      "8585858585858",
      "Vixe maria",
      "Nada a declarar"
    );

    var output = Usecase?.Execute(input);

    Assert.AreEqual(output?.PersonalForm?.Street, input.Street);
    Assert.AreEqual(output?.PersonalForm?.Neighborhood, input.Neighborhood);
    Assert.AreEqual(output?.PersonalForm?.Number, input.Number);
    Assert.AreEqual(output?.PersonalForm?.Country, input.Country);
    Assert.AreEqual(output?.PersonalForm?.Contact, input.Contact);
    Assert.AreEqual(output?.PersonalForm?.Phones, input.Phones);
    Assert.AreEqual(output?.PersonalForm?.ZipCode, input.ZipCode);
    Assert.AreEqual(output?.Id, input.PatientId);
    mock.Verify(mk => mk.UpdatePatient(It.IsAny<Patient>(), professionalId), Times.Once());
  }

  [TestMethod]
  public void ShouldNotBeExecuteUpdatePatientUseCaseWhenNotExists()
  {
    var professionalId = Guid.NewGuid().ToString();
    var patientId = Guid.NewGuid().ToString();
    Patient? patient = null;
    mock.Setup(p => p.FindPatient(It.IsAny<string>(), It.IsAny<string>())).Returns(patient);

    var input = new UpdatePatientPersonalFormInputDto(
      professionalId,
      patientId,
     "Rua dos bobos",
      "Dos doidos",
      "Fraqueza",
      "0",
      "Nenhum",
      "60000000",
      "Nada",
      "Todos",
      "8585858585858",
      "Vixe maria",
      "Nada a declarar"
    );

    try
    {
      Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.UpdatePatient(It.IsAny<Patient>(), It.IsAny<string>()), Times.Exactly(0));
      Assert.IsTrue(e.Message.Contains("UpdatePatientPersonalFormUseCase: Paciente não encontrado"));
    }
  }

  [TestMethod]
  public void ShouldNotBeExecuteUpdatePatientUseCaseWhenFindError()
  {
    Patient patient = CreateValidPatient();
    Professional professional = CreateValidProfessional(patient);
    var professionalId = professional.Id.ToString();
    mock.Setup(p => p.Find(professionalId)).Returns(professional);
    mock.Setup(p => p.FindPatient(patient.Id.ToString(), professionalId)).Throws(new GatewayException("teste error"));

    var input = new UpdatePatientPersonalFormInputDto(
       professionalId,
       patient.Id.ToString(),
       "Rua dos bobos",
      "Dos doidos",
      "Fraqueza",
      "0",
      "Nenhum",
      "60000000",
      "Nada",
      "Todos",
      "8585858585858",
      "Vixe maria",
      "Nada a declarar"
     );

    try
    {
      Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.UpdatePatient(It.IsAny<Patient>(), professionalId), Times.Exactly(0));
      Assert.IsTrue(e.Message.Contains("UpdatePatientPersonalFormUseCase: Erro ao buscar paciente"));
    }
  }

  [TestMethod]
  public void ShouldNotBeExecuteUpdatePatientUseCaseWhenUpdateError()
  {
    Patient patient = CreateValidPatient();
    Professional professional = CreateValidProfessional(patient);
    var professionalId = professional.Id.ToString();
    mock.Setup(p => p.Find(professionalId)).Returns(professional);
    mock.Setup(p => p.FindPatient(patient.Id.ToString(), professionalId)).Returns(patient);
    mock.Setup(p => p.UpdatePatient(It.IsAny<Patient>(), It.IsAny<string>())).Throws(new GatewayException("teste error"));

    var input = new UpdatePatientPersonalFormInputDto(
      professionalId,
      patient.Id.ToString(),
      "Rua dos bobos",
      "Dos doidos",
      "Fraqueza",
      "0",
      "Nenhum",
      "60000000",
      "Nada",
      "Todos",
      "8585858585858",
      "Vixe maria",
      "Nada a declarar"
    );

    try
    {
      Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.UpdatePatient(It.IsAny<Patient>(), professionalId), Times.Exactly(1));
      Assert.IsTrue(e.Message.Contains("UpdatePatientPersonalFormUseCase: Erro ao atualizar"));
    }
  }

  private static Patient CreateValidPatient()
  {
    return new Patient("Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), Guid.NewGuid().ToString());
  }

  private static Professional CreateValidProfessional(Patient patient)
  {
    Professional prof = new Professional(new("123654789", "CRP"), "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
    prof.AddPatient(patient);
    return prof;
  }
}