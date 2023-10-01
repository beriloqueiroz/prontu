using System.Text.RegularExpressions;

namespace domain;

public class Professional : Entity
{
  public string ProfessionalDocument { get; private set; }
  public string Name { get; private set; }
  public string Email { get; private set; }
  public IDocument Document { get; private set; }
  public List<Guid> Patients { get; private set; }

  public Professional(string professionalDocument, string name, string email, IDocument document, List<Guid> patientIds, string? id) : base(id)
  {
    ProfessionalDocument = professionalDocument;
    Name = name;
    Email = email;
    Document = document;
    Patients = patientIds;
    Validate();
    if (notification.HasErrors())
    {
      throw new DomainException(notification.GetErrors());
    }
  }

  public void AddPatient(Guid patientId)
  {
    Patients.Add(patientId);
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
    if (IsValidEmail(Email))
    {
      notification.AddError(new NotificationError("Professional", "Email inválido"));
    }
    if (!Document.IsValid())
    {
      notification.AddError(new NotificationError("Professional", "Cpf inválido"));
    }
  }

  private static bool IsValidEmail(string email)
  {
    string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

    return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
  }
}

