namespace domain;

public class Avatar : IValueObject
{
  public string Value { get; private set; }
  public Avatar(string value)
  {
    Value = value;
  }
  public string GetErrorMessages()
  {
    return "Avatar inválido";
  }

  public bool IsValid()
  {
    return true;
  }
}