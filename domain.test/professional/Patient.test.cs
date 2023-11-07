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
    Assert.AreEqual("Fulano de tal", patient.Name);
  }

  [TestMethod]
  public void ShouldBeNotCreatePatientWhenNameIsEmpty()
  {
    try
    {
      _ = new Patient("", "fulano.tal@gmail.com", new Cpf("74838333005"), null);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual("Patient: Nome inválido", e.Message);
    }
  }

  [TestMethod]
  public void ShouldBeNotCreatePatientWithInvalidEmail()
  {
    try
    {
      _ = new Patient("Fulano de tal", "fulano.talgmail.com", new Cpf("74838333005"), null);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual("Patient: Email inválido", e.Message);
    }
  }

  [TestMethod]
  public void ShouldBeNotCreatePatientWithInvalidDocument()
  {
    try
    {
      _ = new Patient("Fulano de tal", "fulano.tal@gmail.com", new Cpf("12365478910"), null);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual("Patient: Cpf inválido", e.Message);
    }
  }

  [TestMethod]
  public void ShouldBeNotCreatePatientWithMultiInvalidParams()
  {
    try
    {
      _ = new Patient("", "fulano.talgmail.com", new Cpf("12365478"), null);
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

    Assert.AreEqual("teste.silva@hotmail.com", patient.Email);
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
      Assert.AreEqual("Patient: Email inválido", e.Message);
    }
  }

  [TestMethod]
  public void ShouldBeChangePatientName()
  {
    Patient patient = CreateValidPatient("1", "74838333005");
    patient.ChangeName("teste silva");

    Assert.AreEqual("teste silva", patient.Name);
  }

  [TestMethod]
  public void ShouldBeChangePatientAvatar()
  {
    Patient patient = CreateValidPatient("1", "74838333005");
    patient.ChangeAvatar(new("11dalsasld31as2d2sd"));

    Assert.AreEqual("11dalsasld31as2d2sd", patient.Avatar?.Value);
  }

  [TestMethod]
  public void ShouldBeAddAndRemovePhone()
  {
    Patient patient = CreateValidPatient("1", "74838333005");
    patient.AddPhone(new("859988555522", null));
    patient.AddPhone(new("859988555111", null));
    Assert.AreEqual(2, patient.Phones?.Count);
    patient.RemovePhone("859988555111");
    Assert.AreEqual(1, patient.Phones?.Count);
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
      Assert.AreEqual("Patient: Nome inválido", e.Message);
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
    patient.ChangeFinancialInfo(new()
    {
      DefaultSessionPrice = 12.5M,
      EstimatedSessionsByWeek = 4,
      EstimatedTimeSessionInMinutes = 50,
      SessionType = SessionType.ONLINE,
      PaymentType = PaymentType.GROUPED,
      PaymentPeriodInDays = 30,
      SessionQuantityPerPayment = 4
    });

    Assert.IsNotNull(patient);
    Assert.IsTrue(patient.Active);
    Assert.AreEqual("Fulano de tal", patient.Name);
    Assert.AreEqual(SessionType.ONLINE, patient.FinancialInfo?.SessionType);
    Assert.AreEqual(12.5M, patient.FinancialInfo?.DefaultSessionPrice);
  }

  [TestMethod]
  public void ShouldBePopulatePersonalForm()
  {
    Patient patient = new("Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), null);
    patient.ChangePersonalForm(new()
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
    Assert.AreEqual("Fulano de tal", patient.Name);
    Assert.AreEqual("Fortaleza", patient.PersonalForm?.City);
    Assert.AreEqual("Sicrano", patient.PersonalForm?.Contact);
    Assert.AreEqual("85989898989", patient.PersonalForm?.Phones);
  }

  [TestMethod]
  public void ShouldBeErrorWhenPopulateFinancialInfosWrong()
  {
    try
    {
      Patient patient = new("Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), null);

      patient.ChangeFinancialInfo(new()
      {
        DefaultSessionPrice = -12.5M,
        EstimatedSessionsByWeek = 0,
        EstimatedTimeSessionInMinutes = 10,
        SessionType = SessionType.ONLINE,
        PaymentType = PaymentType.GROUPED,
        PaymentPeriodInDays = 30,
        SessionQuantityPerPayment = 1
      });
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.IsTrue(e.Message.Contains("Preço inválido"));
      Assert.IsTrue(e.Message.Contains("Quantidade de sessões por semana inválida"));
      Assert.IsTrue(e.Message.Contains("Tempo estimado para sessão inválido"));
      Assert.IsTrue(e.Message.Contains("Para o tipo de pagamento selecionado, deve-se haver uma quantidade de sessões por pagamento maior do que 1 (dois)"));
    }

    try
    {
      Patient patient = new("Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), null);

      patient.ChangeFinancialInfo(new()
      {
        DefaultSessionPrice = -12.5M,
        EstimatedSessionsByWeek = 0,
        EstimatedTimeSessionInMinutes = 10,
        SessionType = SessionType.ONLINE,
        PaymentType = PaymentType.PER_SESSION,
        PaymentPeriodInDays = 30,
        SessionQuantityPerPayment = 3
      });
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.IsTrue(e.Message.Contains("Preço inválido"));
      Assert.IsTrue(e.Message.Contains("Quantidade de sessões por semana inválida"));
      Assert.IsTrue(e.Message.Contains("Tempo estimado para sessão inválido"));
      Assert.IsTrue(e.Message.Contains("Para o tipo de pagamento selecionado, deve-se haver uma quantidade de sessões igual a 1 (hum), ou deixar nulo"));
    }
  }

  [TestMethod]
  public void ShouldBeErrorWhenPopulatePersonalFormWrong()
  {
    try
    {
      Patient patient = new("Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), null);

      patient.ChangePersonalForm(new()
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