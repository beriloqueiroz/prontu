namespace domain;

public abstract class AggregateRoot : Entity
{
  protected AggregateRoot(string? id) : base(id)
  {
  }
}