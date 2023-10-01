namespace domain;

public class Cpf : IDocument
{
  private readonly string[] InvalidCPFs = { "11111111111", "22222222222", "33333333333", "44444444444", "55555555555", "66666666666", "77777777777", "88888888888", "99999999999", "00000000000" };

  public string Value => Value;

  public bool IsValid()
  {
    string cpf = Value.Replace("\\D", "");
    if (cpf.Length == 11 && !InvalidCPFs.Contains(cpf))
    {
      string digits = cpf[9..];
      int first = FirstDigit(cpf);
      int second = SecondDigit(cpf);
      string er = first.ToString() + second;
      return digits.Equals(er);
    }
    else
    {
      return false;
    }
  }

  private int SecondDigit(string cpf)
  {
    int mult = 11;
    int sum = 0;

    int rest;
    for (rest = 0; rest < 10; ++rest)
    {
      sum += mult * int.Parse(cpf[rest].ToString());
      --mult;
    }

    rest = 11 - sum % 11;
    if (rest >= 10)
    {
      rest = 0;
    }

    return rest;
  }

  private int FirstDigit(string cpf)
  {
    int mult = 10;
    int sum = 0;

    int rest;
    for (rest = 0; rest < 9; ++rest)
    {
      sum += mult * int.Parse(cpf[rest].ToString());
      --mult;
    }

    rest = 11 - sum % 11;
    if (rest >= 10)
    {
      rest = 0;
    }

    return rest;
  }
}