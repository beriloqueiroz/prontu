namespace domain.test;

[TestClass]
public class ProfessionalTest
{
  [TestMethod]
  public void ShouldBeCreateAValidProfessional()
  {
    Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);

    Assert.IsNotNull(professional);
    Assert.AreEqual("Fulano de tal", professional.Name);
  }

  [TestMethod]
  public void ShouldBeNotCreateProfessionalWhenNameIsEmpty()
  {
    try
    {
      _ = new Professional("123654789", "", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual("Professional: Nome inválido", e.Message);
    }
  }

  [TestMethod]
  public void ShouldBeNotCreateProfessionalWithInvalidEmail()
  {
    try
    {
      _ = new Professional("123654789", "Fulano de tal", "fulano.talgmail.com", new Cpf("74838333005"), new(), null);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual("Professional: Email inválido", e.Message);
    }
  }

  [TestMethod]
  public void ShouldBeNotCreateProfessionalWithInvalidProfessionalDocument()
  {
    try
    {
      _ = new Professional("", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual("Professional: Documento profissional inválido", e.Message);
    }
  }

  [TestMethod]
  public void ShouldBeNotCreateProfessionalWithInvalidDocument()
  {
    try
    {
      _ = new Professional("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("12365478"), new(), null);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual("Professional: Cpf inválido", e.Message);
    }
  }

  [TestMethod]
  public void ShouldBeNotCreateProfessionalWithMultiInvalidParams()
  {
    try
    {
      _ = new Professional("123654789", "", "fulano.talgmail.com", new Cpf("123654"), new(), null);
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

    Patient patient = PatientTest.CreateValidPatient("1", "73345940027");
    professional.AddPatient(patient);

    Assert.IsNotNull(professional.Patients);
    Assert.AreEqual(1, professional.Patients.Count);
    Assert.AreEqual("Fulano de tal", professional.Name);
    Assert.AreSame(professional.Patients[0], patient);

    Patient patient2 = PatientTest.CreateValidPatient("2", "37115176094");
    Patient patient3 = PatientTest.CreateValidPatient("3", "74838333005");
    professional.AddPatient(patient2);
    professional.AddPatient(patient3);

    Assert.AreEqual(3, professional.Patients.Count);
    Assert.AreSame(professional.Patients[0], patient);
    Assert.AreSame(professional.Patients[1], patient2);
    Assert.AreSame(professional.Patients[2], patient3);
  }

  [TestMethod]
  public void ShouldBeNotAddPatientWhenAlreadyEmailExists()
  {
    Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("73345940027"), new(), null);

    Patient patient = PatientTest.CreateValidPatient("", "74838333005");
    professional.AddPatient(patient);

    Assert.IsNotNull(professional.Patients);
    Assert.AreEqual(1, professional.Patients.Count);
    Assert.AreEqual("Fulano de tal", professional.Name);
    Assert.AreSame(professional.Patients[0], patient);

    try
    {
      Patient patient2 = PatientTest.CreateValidPatient("", "73345940027");
      professional.AddPatient(patient2);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.IsTrue(e.Message.Contains("Professional: Já existe um paciente cadastrado com email informado"));
    }
    Assert.AreEqual(1, professional.Patients.Count);
    Assert.AreSame(professional.Patients[0], patient);
  }

  [TestMethod]
  public void ShouldBeNotAddPatientWhenAlreadyDocumentExists()
  {
    Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("73345940027"), new(), null);

    Patient patient = PatientTest.CreateValidPatient("", "74838333005");
    professional.AddPatient(patient);

    Assert.IsNotNull(professional.Patients);
    Assert.AreEqual(1, professional.Patients.Count);
    Assert.AreEqual("Fulano de tal", professional.Name);
    Assert.AreSame(professional.Patients[0], patient);

    try
    {
      Patient patient2 = PatientTest.CreateValidPatient("1", "74838333005");
      professional.AddPatient(patient2);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.IsTrue(e.Message.Contains("Professional: Já existe um paciente cadastrado com documento informado"));
    }
    Assert.AreEqual(1, professional.Patients.Count);
    Assert.AreSame(professional.Patients[0], patient);
  }

  [TestMethod]
  public void ShouldBeChangeProfessionalEmail()
  {
    Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
    professional.ChangeEmail("teste.silva@hotmail.com");

    Assert.AreEqual("teste.silva@hotmail.com", professional.Email);
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
      Assert.AreEqual("Professional: Email inválido", e.Message);
    }
  }

  [TestMethod]
  public void ShouldBeChangeDocument()
  {
    Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
    professional.ChangeDocument(new Cpf("72226922075"));

    Assert.AreEqual("72226922075", professional.Document.Value);
  }

  [TestMethod]
  public void ShouldBeNotChangeInvalidDocument()
  {
    try
    {
      Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
      professional.ChangeDocument(new Cpf("111222333"));
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.AreEqual("Professional: Cpf inválido", e.Message);
    }
  }

  [TestMethod]
  public void ShouldBeChangeProfessionalName()
  {
    Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
    professional.ChangeName("teste silva");

    Assert.AreEqual("teste silva", professional.Name);
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
      Assert.AreEqual("Professional: Nome inválido", e.Message);
    }
  }

  [TestMethod]
  public void ShouldBeChangeProfessionalDocument()
  {
    Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);
    professional.ChangeProfessionalDocument("00112233");

    Assert.AreEqual("00112233", professional.ProfessionalDocument);
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
      Assert.AreEqual("Professional: Documento profissional inválido", e.Message);
    }
  }

  [TestMethod]
  public void ShouldBeChangePatient()
  {
    Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);

    Patient patient = PatientTest.CreateValidPatient("1", "73345940027");
    Patient patient2 = PatientTest.CreateValidPatient("2", "37115176094");
    Patient patient3 = PatientTest.CreateValidPatient("3", "74838333005");

    professional.AddPatient(patient);
    professional.AddPatient(patient2);
    professional.AddPatient(patient3);

    Patient patientChanged = new("Fulano de tal test", "fulano.tal@gmail.com", patient.Document, patient.Id.ToString());
    patientChanged.ChangeFinancialInfo(
      new()
      {
        DefaultSessionPrice = 12.5M,
        EstimatedSessionsByWeek = 4,
        EstimatedTimeSessionInMinutes = 50,
        SessionType = SessionType.ONLINE,
        PaymentType = PaymentType.GROUPED,
        PaymentPeriodInDays = 30,
        SessionQuantityPerPayment = 4
      });
    patientChanged.ChangePersonalForm(new()
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

    professional.ChangePatient(patientChanged);

    Patient? patientChangedFound = professional.Patients.Find(p => p.Id.ToString().Equals(patientChanged.Id.ToString()));

    Assert.AreEqual(3, professional.Patients.Count);
    Assert.AreEqual(patientChangedFound?.Id, patientChanged.Id);
    Assert.AreEqual(patientChangedFound?.Document, patientChanged.Document);
    Assert.AreEqual(patientChangedFound?.Email, patientChanged.Email);
    Assert.AreEqual(patientChangedFound?.Name, patientChanged.Name);
  }

  [TestMethod]
  public void ShouldBeChangePatientWhenDocumentAlreadyExist()
  {
    Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);

    Patient patient = PatientTest.CreateValidPatient("1", "73345940027");
    Patient patient2 = PatientTest.CreateValidPatient("2", "37115176094");
    Patient patient3 = PatientTest.CreateValidPatient("3", "74838333005");

    professional.AddPatient(patient);
    professional.AddPatient(patient2);
    professional.AddPatient(patient3);

    Patient patientChanged = new("Fulano de tal test", "fulano.tal@gmail.com", patient2.Document, patient.Id.ToString());
    try
    {
      professional.ChangePatient(patientChanged);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.IsTrue(e.Message.Contains("Já existe um paciente cadastrado com documento informado"));
    }
  }
  [TestMethod]
  public void ShouldBeChangePatientWhenEmailAlreadyExist()
  {
    Professional professional = new("123654789", "Fulano de tal", "fulano.tal@gmail.com", new Cpf("74838333005"), new(), null);

    Patient patient = PatientTest.CreateValidPatient("1", "73345940027");
    Patient patient2 = PatientTest.CreateValidPatient("2", "37115176094");
    Patient patient3 = PatientTest.CreateValidPatient("3", "74838333005");

    professional.AddPatient(patient);
    professional.AddPatient(patient2);
    professional.AddPatient(patient3);

    Patient patientChanged = new("Fulano de tal test", patient3.Email, patient.Document, patient.Id.ToString());
    try
    {
      professional.ChangePatient(patientChanged);
      Assert.Fail();
    }
    catch (DomainException e)
    {
      Assert.IsTrue(e.Message.Contains("Já existe um paciente cadastrado com email informado"));
    }
  }
}