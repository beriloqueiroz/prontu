/*
should register an event handler
should unregister an event handler
should unregister all event handlers
should notify all event handlers
*/
namespace domain.test;

using Moq;

[TestClass]
public class EventDispatcherTest
{

  readonly Mock<IEventHandler> MockEventHandler = new();

  [TestMethod]
  public void ShouldRegisterAnEventHandler()
  {
    IEventDispatcher eventDispatcher = new EventDispatcher();
    IEventHandler eventHandler = MockEventHandler.Object;

    eventDispatcher.Register("SessionCreatedEvent", eventHandler);

    Assert.IsNotNull(eventDispatcher.GetEventHandlers()["SessionCreatedEvent"]);
    Assert.AreEqual(eventDispatcher.GetEventHandlers()["SessionCreatedEvent"].Count, 1);
    Assert.AreSame(eventDispatcher.GetEventHandlers()["SessionCreatedEvent"][0], eventHandler);
  }

  [TestMethod]
  public void ShouldUnRegisterAnEventHandler()
  {
    IEventDispatcher eventDispatcher = new EventDispatcher();
    IEventHandler eventHandler = MockEventHandler.Object;

    eventDispatcher.Register("SessionCreatedEvent", eventHandler);

    Assert.IsNotNull(eventDispatcher.GetEventHandlers()["SessionCreatedEvent"]);
    Assert.AreEqual(eventDispatcher.GetEventHandlers()["SessionCreatedEvent"].Count, 1);
    Assert.AreSame(eventDispatcher.GetEventHandlers()["SessionCreatedEvent"][0], eventHandler);

    eventDispatcher.Unregister("SessionCreatedEvent", eventHandler);

    Assert.AreEqual(eventDispatcher.GetEventHandlers()["SessionCreatedEvent"].Count, 0);
  }

  [TestMethod]
  public void ShouldUnRegisterAnEventHandlerWhenALotHandlers()
  {
    IEventDispatcher eventDispatcher = new EventDispatcher();
    IEventHandler eventHandler1 = MockEventHandler.Object;
    IEventHandler eventHandler2 = MockEventHandler.Object;

    eventDispatcher.Register("SessionCreatedEvent", eventHandler1);
    eventDispatcher.Register("SessionCreatedEvent", eventHandler2);

    Assert.AreEqual(eventDispatcher.GetEventHandlers()["SessionCreatedEvent"].Count, 2);

    eventDispatcher.Unregister("SessionCreatedEvent", eventHandler1);

    Assert.AreEqual(eventDispatcher.GetEventHandlers()["SessionCreatedEvent"].Count, 1);
    Assert.AreSame(eventDispatcher.GetEventHandlers()["SessionCreatedEvent"][0], eventHandler2);
  }

  [TestMethod]
  public void ShouldUnRegisterAllEventHandlers()
  {
    IEventDispatcher eventDispatcher = new EventDispatcher();
    IEventHandler eventHandler1 = MockEventHandler.Object;
    IEventHandler eventHandler2 = MockEventHandler.Object;

    eventDispatcher.Register("SessionCreatedEvent", eventHandler1);
    eventDispatcher.Register("SessionCreatedEvent", eventHandler2);

    Assert.AreEqual(eventDispatcher.GetEventHandlers()["SessionCreatedEvent"].Count, 2);

    eventDispatcher.UnregisterAll();

    Assert.AreEqual(eventDispatcher.GetEventHandlers().Count, 0);
  }


  [TestMethod]
  public void ShouldNotifyAllEventHandlers()
  {
    IEventDispatcher eventDispatcher = new EventDispatcher();
    IEventHandler eventHandler = MockEventHandler.Object;

    eventDispatcher.Register("DomainEventTest", eventHandler);
    eventDispatcher.Register("DomainEventTest", eventHandler);

    Assert.AreEqual(eventDispatcher.GetEventHandlers()["DomainEventTest"].Count, 2);

    var cpf = new Cpf("43548178022");
    DomainEvent domainEvent = new DomainEventTest()
    {
      DataTimeOccurred = DateTime.Now,
      EventData = cpf
    };

    eventDispatcher.Notify(domainEvent);

    MockEventHandler.Verify(mock => mock.Handle(It.IsAny<DomainEvent>()), Times.Exactly(2));
  }
}

class DomainEventTest : DomainEvent
{
  public new DateTime DataTimeOccurred;
  public new required Cpf EventData;
}