using System.Text.RegularExpressions;

namespace domain;

public class Patient : Entity
{
  private string Name { get; }
  private string Email { get; }
  private Document Document { get; }
  private bool Active;
  public Patient(string name, string email, Document document, string? id) : base(id)
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