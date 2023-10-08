using application.professional;
using domain;
using Moq;

namespace application.test;


[TestClass]
public class AddPatientUsecaseTest
{

  private Mock<IProfessionalGateway> mock = new();
  private AddPatientUseCase? Usecase;

  [TestInitialize]
  public void AssemblyInit()
  {
    Usecase = new(mock.Object);
  }

  [TestMethod]
  public void ShouldBeExecuteAddPatientUseCase()
  {
    var professionalId = Guid.NewGuid().ToString();
    var patientId = Guid.NewGuid().ToString();
    mock.Setup(p => p.Find(professionalId)).Returns(CreateValidProfessional());

    var input = new AddPatientInputDto(professionalId, "teste da silva", "teste.silva@gmail.com", "86153877028");

    var output = Usecase?.Execute(input);

    Assert.AreEqual(output?.Patients[0].Document, input.Document);
    mock.Verify(mk => mk.Update(It.IsAny<Professional>()), Times.Once());
  }

  [TestMethod]
  public void ShouldNotBeExecuteAddPatientUseCaseWhenProfessionalNotExists()
  {
    var professionalId = Guid.NewGuid().ToString();
    var patientId = Guid.NewGuid().ToString();
    Professional? professional = null;
    mock.Setup(p => p.Find(professionalId)).Returns(professional);

    var input = new AddPatientInputDto(professionalId, "teste da silva", "teste.silva@gmail.com", "86153877028");

    try
    {
      var output = Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.Update(It.IsAny<Professional>()), Times.Exactly(0));
      Assert.IsTrue(e.Message.Contains("AddPatientUseCase: Profissional não encontrado"));
    }
  }

  [TestMethod]
  public void ShouldNotBeExecuteAddPatientUseCaseWhenFindError()
  {
    var professionalId = Guid.NewGuid().ToString();
    var patientId = Guid.NewGuid().ToString();
    mock.Setup(p => p.Find(professionalId)).Throws(new Exception("teste error"));

    var input = new AddPatientInputDto(professionalId, "teste da silva", "teste.silva@gmail.com", "86153877028");

    try
    {
      var output = Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.Update(It.IsAny<Professional>()), Times.Exactly(0));
      Assert.IsTrue(e.Message.Contains("AddPatientUseCase: Erro ao buscar"));
    }
  }

  [TestMethod]
  public void ShouldNotBeExecuteAddPatientUseCaseWhenUpdateError()
  {
    var professionalId = Guid.NewGuid().ToString();
    var patientId = Guid.NewGuid().ToString();
    mock.Setup(p => p.Find(professionalId)).Returns(CreateValidProfessional());
    mock.Setup(p => p.Update(It.IsAny<Professional>())).Throws(new Exception("teste error"));

    var input = new AddPatientInputDto(professionalId, "teste da silva", "teste.silva@gmail.com", "86153877028");

    try
    {
      var output = Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.Update(It.IsAny<Professional>()), Times.Exactly(1));
      Assert.IsTrue(e.Message.Contains("AddPatientUseCase: Erro ao atualizar"));
    }
  }

  private Professional CreateValidProfessional()
  {
    return new Professional("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
  }
}