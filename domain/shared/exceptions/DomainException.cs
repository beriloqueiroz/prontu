public class DomainException : Exception
{
  public DomainException()
  {
  }

  public DomainException(string message)
      : base(message)
  {
  }

  public DomainException(string message, Exception inner)
      : base(message, inner)
  {
  }

  public DomainException(List<NotificationError> errors)
      : base(string.Join(",", errors.Select(error => $"{error.context}: {error.message}")))
  {
  }
}