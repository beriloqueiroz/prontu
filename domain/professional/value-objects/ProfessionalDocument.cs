namespace domain;
public class ProfessionalDocument : IValueObject
{
  private readonly List<string> Errors = new();
  public string Value { get; private set; }
  public string Institution { get; private set; }

  public ProfessionalDocument(string value, string institution)
  {
    Value = value;
    Institution = institution;
  }
  public string GetErrorMessages()
  {
    return string.Join(",", Errors);
  }

  public bool IsValid()
  {
    if (Value.Length == 0)
      Errors.Add("Documento profissional deve conter um valor válido!");
    if (Institution.Length == 0)
      Errors.Add("Instituição do Documento profissional deve conter um valor válido!");
    if (Errors.Count > 0) return false;
    return true;
  }
}