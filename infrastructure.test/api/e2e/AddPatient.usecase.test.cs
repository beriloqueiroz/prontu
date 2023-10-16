namespace infrastructure.test;


[TestClass]
public class ProfessionalControllerE2ETest
{

  private readonly Mock<IProfessionalGateway> mock = new();
  private AddPatientUseCase? Usecase;

  [TestMethod]
  public void ShouldBeExecute()
  {

  }
}