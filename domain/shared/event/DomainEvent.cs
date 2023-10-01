using Microsoft.OpenApi.Any;

namespace domain;
public class DomainEvent
{
  public DateTime DataTimeOccurred;
  public AnyType EventData;
}
