using System.Net.Mail;
using System.Text.RegularExpressions;

namespace domain;
public class Patient : Entity
{
  public string Name { get; private set; }
  public string Email { get; private set; }
  public IDocument Document { get; private set; }
  public bool Active { get; private set; }
  public Patient(string name, string email, IDocument document, string? id) : base(id)
  {
    Name = name;
    Email = email;
    Document = document;
    Active = true;
    Validate();
    if (notification.HasErrors())
    {
      throw new DomainException(notification.GetErrors());
    }
  }

  public void Activate()
  {
    Active = true;
  }

  public void Deactivate()
  {
    Active = false;
  }

  public bool IsActive()
  {
    return Active;
  }

  public override void Validate()
  {
    if (Name.Length < 4 && !Name.Contains(" "))
    {
      notification.AddError(new NotificationError("Patient", "Nome inválido"));
    }
    if (!Helpers.IsValidEmail(Email))
    {
      notification.AddError(new NotificationError("Patient", "Email inválido"));
    }
    if (!Document.IsValid())
    {
      notification.AddError(new NotificationError("Patient", "Cpf inválido"));
    }
  }
}