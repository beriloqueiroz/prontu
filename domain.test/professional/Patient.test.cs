namespace domain.test;

[TestClass]
public class PatientTest
{
  [TestMethod]
  public void ShouldBeCreateAValidPatient()
  {
    Patient patient = new("Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), null);

    Assert.IsNotNull(patient);
    Assert.IsTrue(patient.Active);
    Assert.AreEqual(patient.Name, "Fulano de tal");
  }

  [TestMethod]
  public void ShouldBeNotCreatePatientWhenNameIsEmpty()
  {
    try
    {
      Patient patient = new("", "fulano.tal@gmail.com", new Cpf("74838333005"), null);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual(e.Message, "Patient: Nome inválido");
    }
  }

  [TestMethod]
  public void ShouldBeNotCreatePatientWithInvalidEmail()
  {
    try
    {
      Patient patient = new("Fulano de tal", "fulano.talgmail.com", new Cpf("74838333005"), null);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual(e.Message, "Patient: Email inválido");
    }
  }

  [TestMethod]
  public void ShouldBeNotCreatePatientWithInvalidDocument()
  {
    try
    {
      Patient patient = new("Fulano de tal", "fulano.tal@gmail.com", new Cpf("12365478910"), null);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual(e.Message, "Patient: Cpf inválido");
    }
  }

  [TestMethod]
  public void ShouldBeNotCreatePatientWithMultiInvalidParams()
  {
    try
    {
      Patient patient = new("", "fulano.talgmail.com", new Cpf("12365478"), null);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.IsTrue(e.Message.Contains("Patient: Email inválido"));
      Assert.IsTrue(e.Message.Contains("Patient: Cpf inválido"));
      Assert.IsTrue(e.Message.Contains("Patient: Nome inválido"));
    }
  }

  [TestMethod]
  public void ShouldBeActivateAndDeactivatePatient()
  {
    Patient patient = new("Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), null);

    Assert.IsTrue(patient.IsActive());

    patient.Deactivate();

    Assert.IsFalse(patient.IsActive());

    patient.Activate();

    Assert.IsTrue(patient.IsActive());
  }

  [TestMethod]
  public void ShouldBeChangePatientEmail()
  {
    Patient patient = CreateValidPatient("1", "74838333005");
    patient.ChangeEmail("teste.silva@hotmail.com");

    Assert.AreEqual(patient.Email, "teste.silva@hotmail.com");
  }

  [TestMethod]
  public void ShouldBeNotChangePatientEmailToInvalidEmail()
  {
    try
    {
      Patient patient = CreateValidPatient("1", "74838333005");
      patient.ChangeEmail("teste.silvahotmail.com");
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual(e.Message, "Patient: Email inválido");
    }
  }

  [TestMethod]
  public void ShouldBeChangePatientName()
  {
    Patient patient = CreateValidPatient("1", "74838333005");
    patient.ChangeName("teste silva");

    Assert.AreEqual(patient.Name, "teste silva");
  }

  [TestMethod]
  public void ShouldBeNotChangePatientNameToInvalidName()
  {
    try
    {
      Patient patient = CreateValidPatient("1", "74838333005");
      patient.ChangeName("");
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual(e.Message, "Patient: Nome inválido");
    }
  }

  public static Patient CreateValidPatient(string tag, string cpf)
  {
    return new($"Fulano de tal {tag}", $"fulano.tal{tag}@gmail.com", new Cpf(cpf), null);
  }

  [TestMethod]
  public void ShouldBePopulateFinancialInfos()
  {
    Patient patient = new("Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), null);
    patient.AddFinancialInfo(new()
    {
      DefaultPrice = 12.5M,
      EstimatedSessionsByWeek = 4,
      EstimatedTimeSessionInMinutes = 50,
      SessionType = "Remoto"
    });

    Assert.IsNotNull(patient);
    Assert.IsTrue(patient.Active);
    Assert.AreEqual(patient.Name, "Fulano de tal");
    Assert.AreEqual(patient.FinancialInfo?.SessionType, "Remoto");
    Assert.AreEqual(patient.FinancialInfo?.DefaultPrice, 12.5M);
  }

  [TestMethod]
  public void ShouldBePopulatePersonalForm()
  {
    Patient patient = new("Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), null);
    patient.AddPersonalForm(new()
    {
      City = "Fortaleza",
      Contact = "Sicrano",
      Country = "Brasil",
      Neighborhood = "Bairro",
      Number = "123",
      Observations = "",
      OthersInfos = "",
      Phones = "85989898989",
      Region = "Ceará",
      Street = "Rua dos bobos"
    });

    Assert.IsNotNull(patient);
    Assert.IsTrue(patient.Active);
    Assert.AreEqual(patient.Name, "Fulano de tal");
    Assert.AreEqual(patient.PersonalForm?.City, "Fortaleza");
    Assert.AreEqual(patient.PersonalForm?.Contact, "Sicrano");
    Assert.AreEqual(patient.PersonalForm?.Phones, "85989898989");
  }

  [TestMethod]
  public void ShouldBeErrorWhenPopulateFinancialInfosWrong()
  {
    try
    {
      Patient patient = new("Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), null);

      patient.AddFinancialInfo(new()
      {
        DefaultPrice = -12.5M,
        EstimatedSessionsByWeek = 0,
        EstimatedTimeSessionInMinutes = 10,
        SessionType = "Remoto"
      });
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.IsTrue(e.Message.Contains("Preço inválido"));
      Assert.IsTrue(e.Message.Contains("Quantidade de sessões por semana inválida"));
      Assert.IsTrue(e.Message.Contains("Tempo estimado para sessão inválido"));
    }
  }

  [TestMethod]
  public void ShouldBeErrorWhenPopulatePersonalFormWrong()
  {
    try
    {
      Patient patient = new("Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), null);

      patient.AddPersonalForm(new()
      {
        City = "",
        Contact = "",
        Country = "",
        Neighborhood = "",
        Number = "123",
        Observations = "",
        OthersInfos = "",
        Phones = "",
        Region = "Ceará",
        Street = "Rua dos bobos"
      });
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.IsTrue(e.Message.Contains("Bairro inválido"));
      Assert.IsTrue(e.Message.Contains("País inválido"));
      Assert.IsTrue(e.Message.Contains("Cidade inválida"));
      Assert.IsTrue(e.Message.Contains("Telefone inválido"));
      Assert.IsTrue(e.Message.Contains("Contato inválido"));
    }
  }
}