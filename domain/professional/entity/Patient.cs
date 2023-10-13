namespace domain;
public class Patient : Entity
{
  public string Name { get; private set; }
  public string Email { get; private set; }
  public IDocument Document { get; private set; }
  public bool Active { get; private set; }
  public FinancialInfo? FinancialInfo { get; private set; }
  public PersonalForm? PersonalForm { get; private set; }
  public Patient(string name, string email, IDocument document, string? id) : base(id)
  {
    Name = name;
    Email = email;
    Document = document;
    Active = true;
    Validate();
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

  public void ChangeFinancialInfo(FinancialInfo financialInfo)
  {
    FinancialInfo = financialInfo;
    Validate();
  }

  public void AddPersonalForm(PersonalForm personalForm)
  {
    PersonalForm = personalForm;
    Validate();
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
      notification.AddError(new NotificationError("Patient", Document.GetErrorMessages()));
    }
    if (FinancialInfo != null && !FinancialInfo.IsValid())
    {
      notification.AddError(new NotificationError("Patient", FinancialInfo.GetErrorMessages()));
    }
    if (PersonalForm != null && !PersonalForm.IsValid())
    {
      notification.AddError(new NotificationError("Patient", PersonalForm.GetErrorMessages()));
    }
    if (notification.HasErrors())
    {
      throw new DomainException(notification.GetErrors());
    }
  }
}