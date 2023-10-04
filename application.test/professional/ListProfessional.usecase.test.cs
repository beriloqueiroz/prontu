using application.professional;
using domain;
using Moq;

namespace application.test;


[TestClass]
public class ListProfessionalUsecaseTest
{

  private Mock<IProfessionalGateway> mock = new();
  private ListProfessionalUseCase? Usecase;

  [TestInitialize]
  public void AssemblyInit()
  {
    Usecase = new(mock.Object);
  }

  [TestMethod]
  public void ShouldBeExecuteListProfessionalUseCaseWhenEmpty()
  {
    mock.Setup(p => p.List(It.IsAny<PageAble>())).Returns(PaginatedList<Professional>.Empty());

    var input = new ListProfessionalInputDto(0, 100);

    var output = Usecase?.Execute(input);

    Assert.AreEqual(output?.Count, 0);
    mock.Verify(mk => mk.List(It.IsAny<PageAble>()), Times.Once());
  }

  [TestMethod]
  public void ShouldBeErrorExecuteListProfessionalUseCaseWhenListError()
  {
    mock.Setup(p => p.List(It.IsAny<PageAble>())).Throws(new Exception("erro do list"));

    var input = new ListProfessionalInputDto(0, 100);

    try
    {
      var output = Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      Assert.IsTrue(e.Message.Contains("ListProfessionalUseCase: Erro ao listar profissionais"));
      mock.Verify(mk => mk.List(It.IsAny<PageAble>()), Times.Once());
    }

  }

  [TestMethod]
  public void ShouldBeExecuteListProfessionalUseCaseWhenNotEmpty()
  {

    List<Professional> professionals = new(4)
    {
        CreateValidProfessional(Guid.NewGuid().ToString()),
        CreateValidProfessional(Guid.NewGuid().ToString()),
        CreateValidProfessional(Guid.NewGuid().ToString()),
        CreateValidProfessional(Guid.NewGuid().ToString())
    };

    mock.Setup(p => p.List(It.IsAny<PageAble>())).Returns(new PaginatedList<Professional>(professionals, professionals.Count, new PageAble(4, 1)));

    var input = new ListProfessionalInputDto(0, 4);

    var output = Usecase?.Execute(input);

    Assert.AreEqual(output?.Count, 4);
    for (int i = 0; i < professionals.Count; i++)
    {
      Assert.AreEqual(professionals[i].Name, output?[i].Name);
    }
    mock.Verify(mk => mk.List(It.IsAny<PageAble>()), Times.Once());
  }

  private Professional CreateValidProfessional(string uuid)
  {
    return new Professional(uuid, "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
  }
}