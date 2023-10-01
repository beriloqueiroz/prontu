namespace domain;
public class Notification
{
  private List<NotificationError> Errors = new List<NotificationError>();

  public void AddError(NotificationError error)
  {
    Errors.Add(error);
  }

  public bool HasErrors()
  {
    return Errors.Count > 0;
  }

  public string Messages(string? context)
  {
    string message = "";
    Errors.ForEach((error) =>
    {
      if (context == null || error.context.Equals(context))
      {
        message += $"{error.context}: ${error.message},";
      }
    });
    return message;
  }

  public List<NotificationError> GetErrors()
  {
    return Errors;
  }

}