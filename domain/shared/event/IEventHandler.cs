namespace domain;
public interface IEventHandler
{
    void Handle(DomainEvent domainEvent);
}