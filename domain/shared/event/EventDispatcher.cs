namespace domain;
public class EventDispatcher : IEventDispatcher
{
  private Dictionary<string, List<IEventHandler>> EventHandlers = new Dictionary<string, List<IEventHandler>>();

  public Dictionary<string, List<IEventHandler>> GetEventHandlers()
  {
    return EventHandlers;
  }
  public void Notify(DomainEvent domainEvent)
  {
    string eventName = domainEvent.GetType().ToString();
    if (EventHandlers[eventName] != null)
    {
      EventHandlers[eventName].ForEach((eventHandler) =>
      {
        eventHandler.Handle(domainEvent);
      });
    }
  }

  public void Register(string eventName, IEventHandler eventHandler)
  {
    if (EventHandlers[eventName] == null)
    {
      EventHandlers[eventName] = new List<IEventHandler>();
    }
    EventHandlers[eventName].Add(eventHandler);
  }

  public void Unregister(string eventName, IEventHandler eventHandler)
  {
    if (EventHandlers[eventName] == null)
    {
      return;
    }
    EventHandlers[eventName].Remove(eventHandler);
  }

  public void UnregisterAll()
  {
    EventHandlers = new Dictionary<string, List<IEventHandler>>();
  }
}
