namespace domain;

public class Phone : IValueObject
{
  public string Value { get; private set; }
  public bool IsChat { get; private set; }
  public Phone(string value, bool? isChat)
  {
    Value = value;
    IsChat = isChat ?? false;
  }

  public bool IsValid()
  {
    return true;
  }

  public string GetErrorMessages()
  {
    return "Telefone inválido";
  }
}