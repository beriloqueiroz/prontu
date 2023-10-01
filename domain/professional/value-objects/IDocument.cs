namespace domain;

public interface IDocument
{
  public abstract string Value { get; }
  public abstract bool IsValid();
}