using application.professional;
using domain;
using Moq;

namespace application.test;


[TestClass]
public class FindPatientUsecaseTest
{

  private readonly Mock<IProfessionalGateway> mock = new();
  private FindPatientUseCase? Usecase;

  [TestInitialize]
  public void AssemblyInit()
  {
    Usecase = new(mock.Object);
  }

  [TestMethod]
  public void ShouldBeExecuteFindPatientUseCase()
  {
    var patientId = Guid.NewGuid().ToString();
    var professionalId = Guid.NewGuid().ToString();
    Patient? patient = CreateValidPatient(patientId.ToString());

    mock.Setup(p => p.FindPatient(patientId, professionalId)).Returns(patient);

    var input = new FindPatientInputDto(patientId, professionalId);

    var output = Usecase?.Execute(input);

    Assert.AreEqual(output?.Document, patient.Document.Value);
    Assert.AreEqual(output?.Email, patient.Email);
    mock.Verify(mk => mk.FindPatient(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
  }

  [TestMethod]
  public void ShouldNotBeExecuteFindPatientUseCaseWhenPatientNotFound()
  {
    var patientId = Guid.NewGuid().ToString();
    var professionalId = Guid.NewGuid().ToString();

    CreateValidPatient(patientId.ToString());

    Patient? patientFound = null;

    mock.Setup(p => p.FindPatient(patientId, professionalId)).Returns(patientFound);

    var input = new FindPatientInputDto(patientId, professionalId);

    try
    {
      Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      Assert.IsTrue(e.Message.Contains("FindPatientUseCase: Paciente não encontrado"));
      mock.Verify(mk => mk.FindPatient(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
    }
  }

  [TestMethod]
  public void ShouldNotBeExecuteFindPatientUseCaseWhenFindError()
  {
    var patientId = Guid.NewGuid().ToString();
    var professionalId = Guid.NewGuid().ToString();

    CreateValidPatient(patientId.ToString());

    mock.Setup(p => p.FindPatient(patientId, professionalId)).Throws(new Exception("erro no find"));

    var input = new FindPatientInputDto(patientId, professionalId);

    try
    {
      Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      Assert.IsTrue(e.Message.Contains("FindPatientUseCase: Erro ao buscar paciente"));
      mock.Verify(mk => mk.FindPatient(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
    }
  }

  private static Patient CreateValidPatient(string uuid)
  {
    return new Patient("Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), uuid);
  }
}