namespace domain;
public interface IValueObject
{
  public bool IsValid();
  public string GetErrorMessages();
}