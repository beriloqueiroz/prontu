namespace domain;

public class PersonalForm : IValueObject
{
  public string? Street { get; set; }
  public string? Neighborhood { get; set; }
  public string? City { get; set; }
  public string? Number { get; set; }
  public string? Country { get; set; }
  public string? ZipCode { get; set; }
  public string? Region { get; set; }
  public string? Contact { get; set; }
  public string? Phones { get; set; }
  public string? OthersInfos { get; set; }
  public string? Observations { get; set; }

  public string GetErrorMessages()
  {
    return "";
  }

  public bool IsValid()
  {
    return true;
  }
}