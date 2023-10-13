namespace domain;

public class PersonalForm : IValueObject
{
  private readonly List<string> Errors = new();
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
    return string.Join(",", Errors);
  }

  public bool IsValid()
  {
    if (Street?.Length == 0) Errors.Add("Rua inválida");
    if (Neighborhood?.Length == 0) Errors.Add("Bairro inválido");
    if (City?.Length == 0) Errors.Add("Cidade inválida");
    if (Number?.Length == 0) Errors.Add("Número inválido");
    if (Country?.Length == 0) Errors.Add("País inválido");
    if (ZipCode?.Length == 0) Errors.Add("Cep inválido");
    if (Region?.Length == 0) Errors.Add("Estado inválido");
    if (Contact?.Length == 0) Errors.Add("Contato inválido");
    if (Phones?.Length == 0) Errors.Add("Telefone inválido");
    if (Errors.Count > 0) return false;
    return true;
  }
}