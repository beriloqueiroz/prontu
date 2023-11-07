using application.professional;
using domain;
using Moq;

namespace application.test;

[TestClass]
public class UpdatePatientUsecaseTest
{

  private readonly Mock<IProfessionalGateway> mock = new();
  private UpdatePatientUseCase? Usecase;

  [TestInitialize]
  public void AssemblyInit()
  {
    Usecase = new(mock.Object);
  }

  [TestMethod]
  public void ShouldBeExecuteUpdatePatientWithoutFinancialInfoAndPersonalFormUseCase()
  {
    Patient patient = CreateValidPatient();
    Professional professional = CreateValidProfessional(patient);
    var professionalId = professional.Id.ToString();
    mock.Setup(p => p.Find(professionalId)).Returns(professional);
    mock.Setup(p => p.FindPatient(patient.Id.ToString(), professionalId)).Returns(patient);

    var input = new UpdatePatientInputDto(
      professionalId,
      patient.Id.ToString(),
      "teste da silva",
      "teste.silva@gmail.com",
      "86153877028",
      true,
      null, null, new List<PhoneDto> { new("85989898989", null) }, null);

    var output = Usecase?.Execute(input);

    Assert.AreEqual(output?.Email, input.Email);
    Assert.AreEqual(output?.Id, input.PatientId);
    Assert.AreEqual("85989898989", output?.Phones?[0]?.Value);
    mock.Verify(mk => mk.UpdatePatient(It.IsAny<Patient>(), professionalId), Times.Once());
  }

  [TestMethod]
  public void ShouldBeExecuteUpdatePatientWithFinancialInfoAndPersonalFormUseCase()
  {
    Patient patient = CreateValidPatient();
    Professional professional = CreateValidProfessional(patient);
    var professionalId = professional.Id.ToString();
    mock.Setup(p => p.Find(professionalId)).Returns(professional);
    mock.Setup(p => p.FindPatient(patient.Id.ToString(), professionalId)).Returns(patient);

    var input = new UpdatePatientInputDto(
      professionalId,
      patient.Id.ToString(),
      "teste da silva",
      "teste.silva@gmail.com",
      "86153877028",
      true,
      new(12.5M, 1, 50, SessionType.ONLINE, PaymentType.PER_SESSION, null, null),
      new("Rua dos bobos", "Aracapé", "Fortaleza", "123", "Brasil", "60511111", "Ceará", "Pai", "858585858585", "", null),
      new List<PhoneDto> { new("85989898989", null) }, null
      );

    var output = Usecase?.Execute(input);

    Assert.AreEqual(output?.Email, input.Email);
    Assert.AreEqual(output?.Id, input.PatientId);
    Assert.AreEqual(12.5M, output?.FinancialInfo?.DefaultSessionPrice);
    Assert.AreEqual("Rua dos bobos", output?.PersonalForm?.Street);
    mock.Verify(mk => mk.UpdatePatient(It.IsAny<Patient>(), professionalId), Times.Once());
  }

  [TestMethod]
  public void ShouldNotBeExecuteUpdatePatientUseCaseWhenNotExists()
  {
    var professionalId = Guid.NewGuid().ToString();
    var patientId = Guid.NewGuid().ToString();
    Patient? patient = null;
    mock.Setup(p => p.FindPatient(It.IsAny<string>(), It.IsAny<string>())).Returns(patient);

    var input = new UpdatePatientInputDto(
      professionalId,
      patientId,
      "teste da silva",
      "teste.silva@gmail.com",
      "86153877028",
      true,
      null, null, new List<PhoneDto> { new("85989898989", null) }, null);

    try
    {
      Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.UpdatePatient(It.IsAny<Patient>(), It.IsAny<string>()), Times.Exactly(0));
      Assert.IsTrue(e.Message.Contains("UpdatePatientUseCase: Paciente não encontrado"));
    }
  }

  [TestMethod]
  public void ShouldNotBeExecuteUpdatePatientUseCaseWhenFindError()
  {
    Patient patient = CreateValidPatient();
    Professional professional = CreateValidProfessional(patient);
    var professionalId = professional.Id.ToString();
    mock.Setup(p => p.Find(professionalId)).Returns(professional);
    mock.Setup(p => p.FindPatient(patient.Id.ToString(), professionalId)).Throws(new Exception("teste error"));

    var input = new UpdatePatientInputDto(
      professionalId,
      patient.Id.ToString(),
      "teste da silva",
      "teste.silva@gmail.com",
      "86153877028",
      true,
      null, null, new List<PhoneDto> { new("85989898989", null) }, null);

    try
    {
      Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.UpdatePatient(It.IsAny<Patient>(), professionalId), Times.Exactly(0));
      Assert.IsTrue(e.Message.Contains("UpdatePatientUseCase: Erro ao buscar paciente"));
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
    mock.Setup(p => p.UpdatePatient(It.IsAny<Patient>(), It.IsAny<string>())).Throws(new Exception("teste error"));

    var input = new UpdatePatientInputDto(
      professionalId,
      patient.Id.ToString(),
      "teste da silva",
      "teste.silva@gmail.com",
      "86153877028",
      true,
      null, null, new List<PhoneDto> { new("85989898989", null) }, null);
    try
    {
      Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.UpdatePatient(It.IsAny<Patient>(), professionalId), Times.Exactly(1));
      Assert.IsTrue(e.Message.Contains("UpdatePatientUseCase: Erro ao atualizar"));
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