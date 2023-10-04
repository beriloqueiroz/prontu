using application.professional;
using domain;
using Moq;

namespace application.test;

[TestClass]
public class UpdateProfessionalUsecaseTest
{

  private Mock<IProfessionalGateway> mock = new();
  private UpdateProfessionalUseCase? Usecase;

  [TestInitialize]
  public void AssemblyInit()
  {
    Usecase = new(mock.Object);
  }

  [TestMethod]
  public void ShouldBeExecuteUpdateProfessionalUseCaseWithoutPatients()
  {
    Professional professional = CreateValidProfessional();
    mock.Setup(p => p.Find(professional.Id.ToString())).Returns(professional);

    var input = new UpdateProfessionalInputDto(professional.Id.ToString(), "teste da silva", "teste.silva@gmail.com", "86153877028", null);

    var output = Usecase?.Execute(input);

    Assert.AreEqual(output?.Email, input.Email);
    Assert.AreEqual(output?.Id, input.Id);
    Assert.AreEqual(output?.Patients.Length, 0);
    mock.Verify(mk => mk.Update(It.IsAny<Professional>()), Times.Once());
  }

  [TestMethod]
  public void ShouldNotBeExecuteUpdateProfessionalUseCaseWhenNotExists()
  {
    Professional? professional = null;
    mock.Setup(p => p.Find(It.IsAny<string>())).Returns(professional);

    var input = new UpdateProfessionalInputDto(Guid.NewGuid().ToString(), "teste da silva", "teste.silva@gmail.com", "86153877028", null);

    try
    {
      var output = Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.Update(It.IsAny<Professional>()), Times.Exactly(0));
      Assert.IsTrue(e.Message.Contains("UpdateProfessionalUseCase: Profissional não encontrado"));
    }
  }

  [TestMethod]
  public void ShouldNotBeExecuteUpdateProfessionalUseCaseWhenFindError()
  {
    Professional professional = CreateValidProfessional();
    mock.Setup(p => p.Find(professional.Id.ToString())).Throws(new Exception("teste error"));

    var input = new UpdateProfessionalInputDto(professional.Id.ToString(), "teste da silva", "teste.silva@gmail.com", "86153877028", null);

    try
    {
      var output = Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.Update(It.IsAny<Professional>()), Times.Exactly(0));
      Assert.IsTrue(e.Message.Contains("UpdateProfessionalUseCase: Erro ao buscar"));
    }
  }

  [TestMethod]
  public void ShouldNotBeExecuteUpdateProfessionalUseCaseWhenUpdateError()
  {
    var professionalId = Guid.NewGuid().ToString();
    var patientId = Guid.NewGuid().ToString();
    mock.Setup(p => p.Find(professionalId)).Returns(CreateValidProfessional());
    mock.Setup(p => p.Update(It.IsAny<Professional>())).Throws(new Exception("teste error"));

    var input = new UpdateProfessionalInputDto(professionalId, "teste da silva", "teste.silva@gmail.com", "86153877028", null);

    try
    {
      var output = Usecase?.Execute(input);
      Assert.Fail();
    }
    catch (ApplicationException e)
    {
      mock.Verify(mk => mk.Update(It.IsAny<Professional>()), Times.Exactly(1));
      Assert.IsTrue(e.Message.Contains("UpdateProfessionalUseCase: Erro ao atualizar"));
    }
  }

  [TestMethod]
  public void ShouldBeExecuteUpdateProfessionalUseCaseWhenChangeAllPatients()
  {
    Professional professional = CreateValidProfessional();
    Patient patient2 = CreateValidPatient("2", "37115176094");
    Patient patient3 = CreateValidPatient("3", "74838333005");
    professional.AddPatient(patient2);
    professional.AddPatient(patient3);

    mock.Setup(p => p.Find(professional.Id.ToString())).Returns(professional);

    var change = "#11aaaaa";

    var inputPatients = professional.Patients.Select(p => new UpdateProfessionalPatientInputDto(p.Id.ToString(), p.Name + change, p.Email, p.Document.Value, p.IsActive())).ToArray();

    var input = new UpdateProfessionalInputDto(professional.Id.ToString(), "teste da silva", "teste.silva@gmail.com", "86153877028", inputPatients);

    var output = Usecase?.Execute(input);

    Assert.AreEqual(output?.Email, input.Email);
    Assert.AreEqual(output?.Id, input.Id);
    Assert.AreEqual(output?.Patients.Length, 2);
    Assert.AreEqual(output?.Patients[0].Document, patient2.Document.Value);
    Assert.AreEqual(output?.Patients[1].Document, patient3.Document.Value);
    Assert.AreEqual(output?.Patients[0].Name, inputPatients[0].Name);
    Assert.AreEqual(output?.Patients[1].Name, inputPatients[1].Name);
    mock.Verify(mk => mk.Update(It.IsAny<Professional>()), Times.Once());
  }

  [TestMethod]
  public void ShouldBeExecuteUpdateProfessionalUseCaseWhenChangeOneOfTwoPatients()
  {
    Professional professional = CreateValidProfessional();
    Patient patient2 = CreateValidPatient("2", "37115176094");
    Patient patient3 = CreateValidPatient("3", "74838333005");
    professional.AddPatient(patient2);
    professional.AddPatient(patient3);

    mock.Setup(p => p.Find(professional.Id.ToString())).Returns(professional);

    var change = "#11aaaaa";

    UpdateProfessionalPatientInputDto[] inputPatients = { new(patient2.Id.ToString(), patient2.Name + change, patient2.Email, patient2.Document.Value, patient2.IsActive()) };

    var input = new UpdateProfessionalInputDto(professional.Id.ToString(), "teste da silva", "teste.silva@gmail.com", "86153877028", inputPatients);

    var output = Usecase?.Execute(input);

    Assert.AreEqual(output?.Email, input.Email);
    Assert.AreEqual(output?.Id, input.Id);
    Assert.AreEqual(output?.Patients.Length, 2);
    Assert.AreEqual(output?.Patients[0].Document, patient2.Document.Value);
    Assert.AreEqual(output?.Patients[1].Document, patient3.Document.Value);
    Assert.AreEqual(output?.Patients[0].Name, inputPatients[0].Name);
    Assert.AreEqual(output?.Patients[1].Name, professional.Patients[1].Name);
    mock.Verify(mk => mk.Update(It.IsAny<Professional>()), Times.Once());
  }

  [TestMethod]
  public void ShouldBeUpdatePatientsFromProfessionalOnlyPatientIsExists()
  {
    Professional professional = CreateValidProfessional();
    Patient patient2 = CreateValidPatient("2", "37115176094");
    Patient patient3 = CreateValidPatient("3", "74838333005");
    professional.AddPatient(patient2);
    professional.AddPatient(patient3);

    mock.Setup(p => p.Find(professional.Id.ToString())).Returns(professional);

    var change = "#11aaaaa";

    UpdateProfessionalPatientInputDto[] inputPatients = { new("53f31bda-b6e1-47d4-8a93-013e8a7a1ffc", patient2.Name + change, patient2.Email, patient2.Document.Value, patient2.IsActive()) };

    var input = new UpdateProfessionalInputDto(professional.Id.ToString(), "teste da silva", "teste.silva@gmail.com", "86153877028", inputPatients);

    var output = Usecase?.Execute(input);

    Assert.AreEqual(output?.Email, input.Email);
    Assert.AreEqual(output?.Id, input.Id);
    Assert.AreEqual(output?.Patients.Length, 2);
    Assert.AreEqual(output?.Patients[0].Document, patient2.Document.Value);
    Assert.AreEqual(output?.Patients[1].Document, patient3.Document.Value);
    Assert.AreEqual(output?.Patients[0].Name, professional.Patients[0].Name);
    Assert.AreEqual(output?.Patients[1].Name, professional.Patients[1].Name);
    mock.Verify(mk => mk.Update(It.IsAny<Professional>()), Times.Once());
  }

  [TestMethod]
  public void ShouldBeNotUpdatePatientsFromProfessionalWhenPatientIsNotExists()
  {
    Professional professional = CreateValidProfessional();
    Patient patient2 = CreateValidPatient("2", "37115176094");
    Patient patient3 = CreateValidPatient("3", "74838333005");
    professional.AddPatient(patient2);
    professional.AddPatient(patient3);

    mock.Setup(p => p.Find(professional.Id.ToString())).Returns(professional);

    var change = "#11aaaaa";

    var inputPatients = professional.Patients.Select(p => new UpdateProfessionalPatientInputDto(p.Id.ToString(), p.Name + change, p.Email, p.Document.Value, p.IsActive())).ToList();

    inputPatients.Add(new("53f31bda-b6e1-47d4-8a93-013e8a7a1ffc", patient2.Name + change, patient2.Email, patient2.Document.Value, patient2.IsActive()));

    var input = new UpdateProfessionalInputDto(professional.Id.ToString(), "teste da silva", "teste.silva@gmail.com", "86153877028", inputPatients.ToArray());

    var output = Usecase?.Execute(input);

    Assert.AreEqual(output?.Email, input.Email);
    Assert.AreEqual(output?.Id, input.Id);
    Assert.AreEqual(output?.Patients.Length, 2);
    Assert.AreEqual(output?.Patients[0].Document, patient2.Document.Value);
    Assert.AreEqual(output?.Patients[1].Document, patient3.Document.Value);
    Assert.AreEqual(output?.Patients[0].Name, inputPatients[0].Name);
    Assert.AreEqual(output?.Patients[1].Name, inputPatients[1].Name);
    mock.Verify(mk => mk.Update(It.IsAny<Professional>()), Times.Once());
  }

  private Professional CreateValidProfessional()
  {
    return new Professional("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
  }
  public static Patient CreateValidPatient(string tag, string cpf)
  {
    return new($"Fulano de tal {tag}", $"fulano.tal{tag}@gmail.com", new Cpf(cpf), null);
  }
}