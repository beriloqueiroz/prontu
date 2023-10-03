namespace domain.test;

[TestClass]
public class ProfessionalTest
{
  [TestMethod]
  public void ShouldBeCreateAValidProfessional()
  {
    Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);

    Assert.IsNotNull(professional);
    Assert.AreEqual(professional.Name, "Fulano de tal");
  }

  [TestMethod]
  public void ShouldBeNotCreateProfessionalWhenNameIsEmpty()
  {
    try
    {
      Professional professional = new("123654789", "", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual(e.Message, "Professional: Nome inválido");
    }
  }

  [TestMethod]
  public void ShouldBeNotCreateProfessionalWithInvalidEmail()
  {
    try
    {
      Professional professional = new("123654789", "Fulano de tal", "fulano.talgmail.com", new Cpf("74838333005"), new(), null);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual(e.Message, "Professional: Email inválido");
    }
  }

  [TestMethod]
  public void ShouldBeNotCreateProfessionalWithInvalidProfessionalDocument()
  {
    try
    {
      Professional professional = new("", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual(e.Message, "Professional: Documento profissional inválido");
    }
  }

  [TestMethod]
  public void ShouldBeNotCreateProfessionalWithInvalidDocument()
  {
    try
    {
      Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("12365478"), new(), null);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual(e.Message, "Professional: Cpf inválido");
    }
  }

  [TestMethod]
  public void ShouldBeNotCreateProfessionalWithMultiInvalidParams()
  {
    try
    {
      Professional professional = new("123654789", "", "fulano.talgmail.com", new Cpf("123654"), new(), null);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.IsTrue(e.Message.Contains("Professional: Email inválido"));
      Assert.IsTrue(e.Message.Contains("Professional: Cpf inválido"));
      Assert.IsTrue(e.Message.Contains("Professional: Nome inválido"));
    }
  }

  [TestMethod]
  public void ShouldBeAddPatient()
  {
    Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);

    Patient patient = PatientTest.CreateValidPatient("1");
    professional.AddPatient(patient);

    Assert.IsNotNull(professional.Patients);
    Assert.AreEqual(professional.Patients.Count, 1);
    Assert.AreEqual(professional.Name, "Fulano de tal");
    Assert.AreSame(professional.Patients[0], patient);

    Patient patient2 = PatientTest.CreateValidPatient("2");
    Patient patient3 = PatientTest.CreateValidPatient("3");
    professional.AddPatient(patient2);
    professional.AddPatient(patient3);

    Assert.AreEqual(professional.Patients.Count, 3);
    Assert.AreSame(professional.Patients[0], patient);
    Assert.AreSame(professional.Patients[1], patient2);
    Assert.AreSame(professional.Patients[2], patient3);
  }

  [TestMethod]
  public void ShouldBeChangeProfessionalEmail()
  {
    Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
    professional.ChangeEmail("teste.silva@hotmail.com");

    Assert.AreEqual(professional.Email, "teste.silva@hotmail.com");
  }

  [TestMethod]
  public void ShouldBeNotChangeProfessionalEmailToInvalidEmail()
  {
    try
    {
      Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
      professional.ChangeEmail("teste.silvahotmail.com");
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual(e.Message, "Professional: Email inválido");
    }
  }

  [TestMethod]
  public void ShouldBeChangeProfessionalName()
  {
    Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
    professional.ChangeName("teste silva");

    Assert.AreEqual(professional.Name, "teste silva");
  }

  [TestMethod]
  public void ShouldBeNotChangeProfessionalNameToInvalidName()
  {
    try
    {
      Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
      professional.ChangeName("");
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual(e.Message, "Professional: Nome inválido");
    }
  }

  [TestMethod]
  public void ShouldBeChangeProfessionalDocument()
  {
    Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
    professional.ChangeProfessionalDocument("00112233");

    Assert.AreEqual(professional.ProfessionalDocument, "00112233");
  }

  [TestMethod]
  public void ShouldBeNotChangeProfessionalDocumentToInvalidProfessionalDocument()
  {
    try
    {
      Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
      professional.ChangeProfessionalDocument("");
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual(e.Message, "Professional: Documento profissional inválido");
    }
  }
}