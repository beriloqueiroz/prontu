using application.professional;
using domain;
using Moq;

namespace application.test;

[TestClass]
public class UpdatePatientFinancialInfoUsecaseTest
{

  private readonly Mock<IProfessionalGateway> mock = new();
  private UpdatePatientFinancialInfoUseCase? Usecase;

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

    var input = new UpdatePatientFinancialInfoInputDto(
      professionalId,
      patient.Id.ToString(),
      decimal.Parse("105.25"),
      4,
      50,
      SessionType.IN_PERSON,
      PaymentType.PER_SESSION,
      null,
      null
    );

    var output = Usecase?.Execute(input);

    Assert.AreEqual(output?.FinancialInfo?.DefaultSessionPrice, input.DefaultSessionPrice);
    Assert.AreEqual(output?.FinancialInfo?.SessionQuantityPerPayment, input.SessionQuantityPerPayment);
    Assert.AreEqual(output?.FinancialInfo?.EstimatedTimeSessionInMinutes, input.EstimatedTimeSessionInMinutes);
    Assert.AreEqual(output?.FinancialInfo?.PaymentPeriodInDays, input.PaymentPeriodInDays);
    Assert.AreEqual(output?.FinancialInfo?.EstimatedSessionsByWeek, input.EstimatedSessionsByWeek);
    Assert.AreEqual(output?.FinancialInfo?.PaymentType, input.PaymentType);
    Assert.AreEqual(output?.FinancialInfo?.SessionType, input.SessionType);
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

    var input = new UpdatePatientFinancialInfoInputDto(
      professionalId,
      patientId,
      decimal.Parse("105.25"),
      4,
      50,
      SessionType.IN_PERSON,
      PaymentType.PER_SESSION,
      null,
      null
    );

    try
    {
      Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.UpdatePatient(It.IsAny<Patient>(), It.IsAny<string>()), Times.Exactly(0));
      Assert.IsTrue(e.Message.Contains("UpdatePatientFinancialInfoUseCase: Paciente não encontrado"));
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

    var input = new UpdatePatientFinancialInfoInputDto(
       professionalId,
       patient.Id.ToString(),
       decimal.Parse("105.25"),
       4,
       50,
       SessionType.IN_PERSON,
       PaymentType.PER_SESSION,
       null,
       null
     );

    try
    {
      Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.UpdatePatient(It.IsAny<Patient>(), professionalId), Times.Exactly(0));
      Assert.IsTrue(e.Message.Contains("UpdatePatientFinancialInfoUseCase: Erro ao buscar paciente"));
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

    var input = new UpdatePatientFinancialInfoInputDto(
      professionalId,
      patient.Id.ToString(),
      decimal.Parse("105.25"),
      4,
      50,
      SessionType.IN_PERSON,
      PaymentType.PER_SESSION,
      null,
      null
    );

    try
    {
      Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.UpdatePatient(It.IsAny<Patient>(), professionalId), Times.Exactly(1));
      Assert.IsTrue(e.Message.Contains("UpdatePatientFinancialInfoUseCase: Erro ao atualizar"));
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