namespace domain;

public class Professional : AggregateRoot
{
  public string ProfessionalDocument { get; private set; }
  public string Name { get; private set; }
  public string Email { get; private set; }
  public IDocument Document { get; private set; }
  public List<Patient> Patients { get; private set; } = new List<Patient>();

  public Professional(string professionalDocument, string name, string email, IDocument document, List<Patient> patients, string? id) : base(id)
  {
    ProfessionalDocument = professionalDocument;
    Name = name;
    Email = email;
    Document = document;
    Patients = patients;
    if (patients == null)
    {
      Patients = new List<Patient>();
    }
    Validate();
  }

  public void AddPatient(Patient patient)
  {
    if (Patients == null)
    {
      notification.AddError(new NotificationError("Professional", "Não é possível adicionar paciente"));
      throw new DomainException(notification.GetErrors());
    }
    if (Patients.Any(pat => pat.Document.Value.Equals(patient.Document.Value)))
    {
      notification.AddError(new NotificationError("Professional", "Já existe um paciente cadastrado com documento informado"));
    }
    if (Patients.Any(pat => pat.Email.Equals(patient.Email)))
    {
      notification.AddError(new NotificationError("Professional", "Já existe um paciente cadastrado com email informado"));
    }
    if (notification.HasErrors())
    {
      throw new DomainException(notification.GetErrors());
    }
    Patients.Add(patient);
  }

  public void ChangePatient(Patient patient)
  {
    if (Patients == null)
    {
      notification.AddError(new NotificationError("Professional", "Não é possível adicionar paciente"));
      throw new DomainException(notification.GetErrors());
    }
    Patient? patientFound = Patients.Find(p => p.Id.ToString().Equals(patient.Id.ToString()));
    if (patientFound == null)
    {
      throw new DomainException("Professional: Paciente não é atendido pelo profissional");
    }
    if (Patients.Any(pat => pat.Document.Value.Equals(patient.Document.Value) && !pat.Id.ToString().Equals(patient.Id.ToString())))
    {
      notification.AddError(new NotificationError("Professional", "Já existe um paciente cadastrado com documento informado"));
    }
    if (Patients.Any(pat => pat.Email.Equals(patient.Email) && !pat.Id.ToString().Equals(patient.Id.ToString())))
    {
      notification.AddError(new NotificationError("Professional", "Já existe um paciente cadastrado com email informado"));
    }
    if (notification.HasErrors())
    {
      throw new DomainException(notification.GetErrors());
    }
    Patients.Remove(patientFound);
    AddPatient(patient);
  }

  public void ChangeEmail(string email)
  {
    Email = email;
    Validate();
  }

  public void ChangeName(string name)
  {
    Name = name;
    Validate();
  }

  public void ChangeProfessionalDocument(string professionalDocument)
  {
    ProfessionalDocument = professionalDocument;
    Validate();
  }

  public override void Validate()
  {
    if (ProfessionalDocument.Length < 3)
    {
      notification.AddError(new NotificationError("Professional", "Documento profissional inválido"));
    }
    if (Name.Length < 4 && !Name.Contains(" "))
    {
      notification.AddError(new NotificationError("Professional", "Nome inválido"));
    }
    if (!Helpers.IsValidEmail(Email))
    {
      notification.AddError(new NotificationError("Professional", "Email inválido"));
    }
    if (!Document.IsValid())
    {
      notification.AddError(new NotificationError("Professional", Document.GetErrorMessages()));
    }
    if (notification.HasErrors())
    {
      throw new DomainException(notification.GetErrors());
    }
  }
}

