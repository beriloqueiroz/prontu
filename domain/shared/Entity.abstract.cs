namespace domain;
public abstract class Entity
{
  public bool IsNew { get; } = false;
  public Guid Id { get; private set; }
  public Notification notification { get; set; } = new();
  protected Entity(string? id)
  {
    if (id == null)
    {
      Id = Guid.NewGuid();
      IsNew = true;
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