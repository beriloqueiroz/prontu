namespace domain;

public class SendEmailWhenSessionIsCreatedHandler : IEventHandler
{
  public void Handle(DomainEvent domainEvent)
  {
    Console.WriteLine("Send email" + domainEvent.ToString());
  }
}