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

  public static Patient CreateValidPatient(string tag)
  {
    return new($"Fulano de tal {tag}", $"fulano.tal{tag}@gmail.com", new Cpf("74838333005"), null);
  }
}