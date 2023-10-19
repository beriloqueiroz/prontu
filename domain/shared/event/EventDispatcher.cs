namespace domain;
public class EventDispatcher : IEventDispatcher
{
  private Dictionary<string, List<IEventHandler>> EventHandlers = new();

  public Dictionary<string, List<IEventHandler>> GetEventHandlers()
  {
    return EventHandlers;
  }
  public void Notify(DomainEvent domainEvent)
  {
    string eventName = domainEvent.GetType().Name;
    try
    {
      EventHandlers[eventName].ForEach((eventHandler) =>
     {
       eventHandler.Handle(domainEvent);
     });
    }
    catch (KeyNotFoundException)
    {
    }
  }

  public void Register(string eventName, IEventHandler eventHandler)
  {
    if (!EventHandlers.TryAdd(eventName, new List<IEventHandler>
    {
        eventHandler
    }))
    {
      EventHandlers[eventName].Add(eventHandler);
    }
  }

  public void Unregister(string eventName, IEventHandler eventHandler)
  {
    try
    {
      EventHandlers[eventName].Remove(eventHandler);
    }
    catch (KeyNotFoundException)
    {

    }
  }

  public void UnregisterAll()
  {
    EventHandlers = new Dictionary<string, List<IEventHandler>>();
  }
}
