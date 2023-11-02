using System.Net.Mail;

namespace domain;

public static class Helpers
{
  public static bool IsValidEmail(string email)
  {
    try
    {
      MailAddress.TryCreate(email, out _);
      return true;
    }
    catch (FormatException)
    {
      return false;
    }
  }
}