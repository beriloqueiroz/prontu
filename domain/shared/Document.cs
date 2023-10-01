namespace domain;

public abstract class Document
{
  public abstract string Value { get; }


  public abstract bool IsValid();
}