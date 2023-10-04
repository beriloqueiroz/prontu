using application.professional;
using domain;
using Moq;

namespace application.test;


[TestClass]
public class FindProfessionalUsecaseTest
{

  private Mock<IProfessionalGateway> mock = new();
  private FindProfessionalUseCase? Usecase;

  [TestInitialize]
  public void AssemblyInit()
  {
    Usecase = new(mock.Object);
  }

  [TestMethod]
  public void ShouldBeExecuteFindProfessionalUseCase()
  {
    var professionalId = Guid.NewGuid().ToString();
    Professional? professional = CreateValidProfessional(professionalId.ToString());

    mock.Setup(p => p.Find(professionalId)).Returns(professional);

    var input = new FindProfessionalInputDto(professionalId);

    var output = Usecase?.Execute(input);

    Assert.AreEqual(output?.Document, professional.Document.Value);
    Assert.AreEqual(output?.Email, professional.Email);
    mock.Verify(mk => mk.Find(It.IsAny<string>()), Times.Once());
  }

  [TestMethod]
  public void ShouldNotBeExecuteFindProfessionalUseCaseWhenProfessionalNotFound()
  {
    var professionalId = Guid.NewGuid().ToString();
    Professional? professional = CreateValidProfessional(professionalId.ToString());

    Professional? professionalFound = null;

    mock.Setup(p => p.Find(professionalId)).Returns(professionalFound);

    var input = new FindProfessionalInputDto(professionalId);

    try
    {
      var output = Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      Assert.IsTrue(e.Message.Contains("FindProfessionalUseCase: Profissional não encontrado"));
      mock.Verify(mk => mk.Find(It.IsAny<string>()), Times.Once());
    }
  }

  [TestMethod]
  public void ShouldNotBeExecuteFindProfessionalUseCaseWhenFindError()
  {
    var professionalId = Guid.NewGuid().ToString();
    Professional? professional = CreateValidProfessional(professionalId.ToString());

    mock.Setup(p => p.Find(professionalId)).Throws(new Exception("erro no find"));

    var input = new FindProfessionalInputDto(professionalId);

    try
    {
      var output = Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      Assert.IsTrue(e.Message.Contains("FindProfessionalUseCase: Erro ao buscar profissional"));
      mock.Verify(mk => mk.Find(It.IsAny<string>()), Times.Once());
    }
  }

  private Professional CreateValidProfessional(string uuid)
  {
    return new Professional(uuid, "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
  }
}