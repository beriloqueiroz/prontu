namespace domain;

public interface IEventDispatcher
{
  void Notify(DomainEvent domainEvent);
  void Register(string eventName, IEventHandler eventHandler);
  void Unregister(string eventName, IEventHandler eventHandler);
  void UnregisterAll();
}
