namespace domain;

public class Phone : IValueObject
{
  public string Value { get; set; }
  public bool IsChat { get; set; }
  public Phone(string value, bool? isChat)
  {
    Value = value;
    IsChat = isChat ?? false;
  }

  //necessário para desserialização
  public Phone()
  {
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