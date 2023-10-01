public class NotificationError
{
  public string context;
  public string message;

  public NotificationError(string context, string message)
  {
    this.context = context;
    this.message = message;
  }
}