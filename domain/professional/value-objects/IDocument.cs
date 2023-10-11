namespace domain;

public interface IDocument : IValueObject
{
  public abstract string Value { get; }
}