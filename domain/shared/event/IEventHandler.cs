namespace domain;
public interface IEventHandler<T> where T : DomainEvent
{
    void Handle(T domainEvent);
}

public interface IEventHandler : IEventHandler<DomainEvent>
{

}