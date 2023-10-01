using System.Text.RegularExpressions;

namespace domain;

public class Professional : Entity
{
  private string ProfessionalDocument { get; }
  private string Name { get; }
  private string Email { get; }
  private Document Document { get; }
  private List<Guid> Patients { get; }

  public Professional(string professionalDocument, string name, string email, Document document, string? id) : base(id)
  {
    ProfessionalDocument = professionalDocument;
    Name = name;
    Email = email;
    Document = document;
    Patients = new List<Guid>();
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

