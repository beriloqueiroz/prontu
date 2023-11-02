using application.professional;
using domain;
using Moq;

namespace application.test;


[TestClass]
public class CreateProfessionalUsecaseTest
{

  private readonly Mock<IProfessionalGateway> mock = new();
  private CreateProfessionalUseCase? Usecase;

  [TestInitialize]
  public void AssemblyInit()
  {
    Usecase = new(mock.Object);
  }

  [TestMethod]
  public void ShouldBeExecuteCreateProfessionalUseCase()
  {
    var input = new CreateProfessionalInputDto("teste da silva", "teste.silva@gmail.com", "86153877028", "123654");

    var output = Usecase?.Execute(input);

    Assert.AreEqual(output?.Document, input.Document);
    Assert.AreEqual(output?.Email, input.Email);
    mock.Verify(mk => mk.Create(It.IsAny<Professional>()), Times.Once());
  }

  [TestMethod]
  public void ShouldNotBeExecuteCreateProfessionalUseCaseWhenProfessionalAlreadyExists()
  {
    mock.Setup(p => p.IsExists(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

    var input = new CreateProfessionalInputDto("teste da silva", "teste@gmail.com", "86153877028", "123654");

    try
    {
      _ = Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.Create(It.IsAny<Professional>()), Times.Exactly(0));
      Assert.IsTrue(e.Message.Contains("CreateProfessionalUseCase: Já existe um profissional cadastrado"));
    }
  }

  [TestMethod]
  public void ShouldNotBeExecuteCreateProfessionalUseCaseWhenVerifyIfExistsError()
  {
    mock.Setup(p => p.IsExists(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception("teste error"));

    var input = new CreateProfessionalInputDto("teste da silva", "teste.silva@gmail.com", "86153877028", "123654");

    try
    {
      _ = Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.Create(It.IsAny<Professional>()), Times.Exactly(0));
      Assert.IsTrue(e.Message.Contains("CreateProfessionalUseCase: Erro ao verificar se existe"));
    }
  }

  [TestMethod]
  public void ShouldNotBeExecuteCreateProfessionalUseCaseWhenCreateError()
  {
    var professionalId = Guid.NewGuid().ToString();
    mock.Setup(p => p.Find(professionalId)).Returns(CreateValidProfessional());
    mock.Setup(p => p.Create(It.IsAny<Professional>())).Throws(new Exception("teste error"));

    var input = new CreateProfessionalInputDto("teste da silva", "teste.silva@gmail.com", "86153877028", "123654");

    try
    {
      _ = Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.Create(It.IsAny<Professional>()), Times.Exactly(1));
      Assert.IsTrue(e.Message.Contains("CreateProfessionalUseCase: Erro ao criar"));
    }
  }

  private Professional CreateValidProfessional()
  {
    return new Professional("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
  }
}