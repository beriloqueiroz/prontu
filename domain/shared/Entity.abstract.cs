namespace domain;
public abstract class Entity
{
  public Guid Id { get; private set; }
  public Notification notification = new();
  public Entity(string? id)
  {
    if (id == null)
    {
      Id = new Guid();
      return;
    }
    Guid guid;
    bool isValid = Guid.TryParse(id, out guid);
    if (!isValid)
      throw new DomainException("Identificador inválido");
    Id = guid;
  }

  public abstract void Validate();
}