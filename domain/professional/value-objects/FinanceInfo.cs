namespace domain;

public class FinancialInfo : IValueObject
{
  private readonly List<string> Errors = new();
  public required decimal DefaultPrice { get; set; }
  public required int EstimatedSessionsByWeek { get; set; }
  public required int EstimatedTimeSessionInMinutes { get; set; }
  public required string SessionType { get; set; }

  public string GetErrorMessages()
  {
    return string.Join(",", Errors);
  }

  public bool IsValid()
  {
    if (DefaultPrice.CompareTo(decimal.Zero) < 0) Errors.Add("Preço inválido");
    if (EstimatedSessionsByWeek <= 0) Errors.Add("Quantidade de sessões por semana inválida");
    if (EstimatedTimeSessionInMinutes <= 10) Errors.Add("Tempo estimado para sessão inválido");
    if (SessionType.Length < 0) Errors.Add("Tipo da sessão inválido");
    if (Errors.Count > 0) return false;
    return true;
  }
}