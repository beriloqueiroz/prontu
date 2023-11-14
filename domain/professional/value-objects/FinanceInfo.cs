namespace domain;

public class FinancialInfo : IValueObject
{
  private readonly List<string> Errors = new();
  public required decimal DefaultSessionPrice { get; set; }
  public required int EstimatedSessionsByWeek { get; set; }
  public required int EstimatedTimeSessionInMinutes { get; set; }
  public required SessionType SessionType { get; set; }
  public required PaymentType PaymentType { get; set; }
  public int? PaymentPeriodInDays { get; set; }
  public int? SessionQuantityPerPayment { get; set; } = 1;

  public string GetErrorMessages()
  {
    return string.Join(",", Errors);
  }

  public bool IsValid()
  {
    if (DefaultSessionPrice.CompareTo(decimal.Zero) < 0) Errors.Add("Preço inválido");
    if (EstimatedSessionsByWeek < 0) Errors.Add("Quantidade de sessões por semana inválida");
    if (EstimatedTimeSessionInMinutes < 0) Errors.Add("Tempo estimado para sessão inválido");
    if (SessionQuantityPerPayment < 0) Errors.Add("Quantidade de sessões por pagamento inválida");
    if (PaymentPeriodInDays < 0) Errors.Add("Período de pagamento inválido");
    if (PaymentType.Equals(PaymentType.GROUPED) && SessionQuantityPerPayment < 2) Errors.Add("Para o tipo de pagamento selecionado, deve-se haver uma quantidade de sessões por pagamento maior do que 1 (dois)");
    if (PaymentType.Equals(PaymentType.PER_SESSION) && SessionQuantityPerPayment != 1 && SessionQuantityPerPayment != null)
      Errors.Add("Para o tipo de pagamento selecionado, deve-se haver uma quantidade de sessões igual a 1 (hum), ou deixar nulo");
    if (Errors.Count > 0) return false;
    return true;
  }
}