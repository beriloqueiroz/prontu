namespace domain;
public class NotificationError
{
  public string context { get; set; }
  public string message { get; set; }

  public NotificationError(string context, string message)
  {
    this.context = context;
    this.message = message;
  }
}