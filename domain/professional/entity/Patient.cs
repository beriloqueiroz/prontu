namespace domain;
public class Patient : Entity
{
  public string Name { get; private set; }
  public string Email { get; private set; }
  public IDocument Document { get; private set; }
  public bool Active { get; private set; }
  public FinancialInfo? FinancialInfo { get; private set; }
  public PersonalForm? PersonalForm { get; private set; }
  public List<Phone>? Phones { get; private set; } = new();
  public Avatar? Avatar { get; private set; }
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
  
  public void ChangeDocument(IDocument document)
  {
    Document = document;
    Validate();
  }

  public void ChangeName(string name)
  {
    Name = name;
    Validate();
  }

  public void ChangeAvatar(Avatar avatar)
  {
    Avatar = avatar;
    Validate();
  }

  public void ChangeFinancialInfo(FinancialInfo financialInfo)
  {
    FinancialInfo = financialInfo;
    Validate();
  }

  public void ChangePersonalForm(PersonalForm personalForm)
  {
    PersonalForm = personalForm;
    Validate();
  }

  public void AddPhone(Phone phone)
  {
    if (Phones == null) Phones = new();
    Phones.Add(phone);
  }

  public void RemovePhone(string phone)
  {
    if (Phones != null)
    {
      Phone? phoneFound = Phones.Find(p => p.Value == phone);
      if (phoneFound != null)
        Phones.Remove(phoneFound);
    }
  }

  public void ChangePhones(List<Phone> phones)
  {
    Phones = phones;
  }

  public void AddAllPhones()
  {
    Phones = new();
  }

  public override void Validate()
  {
    if (Name.Length < 4 && !Name.Contains(" "))
    {
      notification.AddError(new NotificationError("Patient", "Nome inválido"));
    }
    if (Email!=null && Email != "" && !Helpers.IsValidEmail(Email))
    {
      notification.AddError(new NotificationError("Patient", "Email inválido"));
    }
    if (Document != null && Document.Value != "" && Document.Value!="" && !Document.IsValid())
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