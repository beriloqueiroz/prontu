using application.professional;
using domain;
using Moq;

namespace application.test;

[TestClass]
public class ChangeProfessionalEmailUsecaseTest
{

  private readonly Mock<IProfessionalGateway> mock = new();
  private ChangeProfessionalEmailUseCase? Usecase;

  [TestInitialize]
  public void AssemblyInit()
  {
    Usecase = new(mock.Object);
  }

  [TestMethod]
  public void ShouldBeExecuteChangeProfessionalEmailUseCase()
  {
    Professional professional = CreateValidProfessional();
    mock.Setup(p => p.Find(professional.Id.ToString())).Returns(professional);

    var input = new ChangeProfessionalEmailInputDto(professional.Id.ToString(), "teste.silva@gmail.com");

    var output = Usecase?.Execute(input);

    Assert.AreEqual(output?.Email, input.Email);
    Assert.AreEqual(output?.Id, input.Id);
    mock.Verify(mk => mk.Update(It.IsAny<Professional>()), Times.Once());
  }

  [TestMethod]
  public void ShouldNotBeExecuteChangeProfessionalEmailUseCaseWhenNotExists()
  {
    Professional? professional = null;
    mock.Setup(p => p.Find(It.IsAny<string>())).Returns(professional);

    var input = new ChangeProfessionalEmailInputDto(Guid.NewGuid().ToString(), "teste.silva@gmail.com");

    try
    {
      Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.Update(It.IsAny<Professional>()), Times.Exactly(0));
      Assert.IsTrue(e.Message.Contains("ChangeProfessionalEmailUseCase: Profissional não encontrado"));
    }
  }

  [TestMethod]
  public void ShouldNotBeExecuteChangeProfessionalEmailUseCaseWhenFindError()
  {
    Professional professional = CreateValidProfessional();
    mock.Setup(p => p.Find(professional.Id.ToString())).Throws(new Exception("teste error"));

    var input = new ChangeProfessionalEmailInputDto(professional.Id.ToString(), "teste.silva@gmail.com");

    try
    {
      Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.Update(It.IsAny<Professional>()), Times.Exactly(0));
      Assert.IsTrue(e.Message.Contains("ChangeProfessionalEmailUseCase: Erro ao buscar"));
    }
  }

  [TestMethod]
  public void ShouldNotBeExecuteChangeProfessionalEmailUseCaseWhenUpdateError()
  {
    var professionalId = Guid.NewGuid().ToString();
    mock.Setup(p => p.Find(professionalId)).Returns(CreateValidProfessional());
    mock.Setup(p => p.Update(It.IsAny<Professional>())).Throws(new Exception("teste error"));

    var input = new ChangeProfessionalEmailInputDto(professionalId, "teste.silva@gmail.com");

    try
    {
      Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.Update(It.IsAny<Professional>()), Times.Exactly(1));
      Assert.IsTrue(e.Message.Contains("ChangeProfessionalEmailUseCase: Erro ao atualizar"));
    }
  }

  private static Professional CreateValidProfessional()
  {
    return new Professional(new("123654789", "CRP"), "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
  }
  public static Patient CreateValidPatient(string tag, string cpf)
  {
    return new($"Fulano de tal {tag}", $"fulano.tal{tag}@gmail.com", new Cpf(cpf), null);
  }
}