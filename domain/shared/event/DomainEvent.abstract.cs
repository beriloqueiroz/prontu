using Microsoft.OpenApi.Any;

namespace domain;
public abstract class DomainEvent
{
  public DateTime DataTimeOccurred;
  public AnyType EventData;
}
