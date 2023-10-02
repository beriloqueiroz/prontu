namespace domain.test;

using domain;

[TestClass]
public class NotificationTest
{
  [TestMethod]
  public void ShouldCreateErrors()
  {
    Notification notification = new();

    notification.AddError(new NotificationError("Professional", "Erro teste 1"));

    Assert.AreEqual(notification.Messages("Professional"), "Professional: Erro teste 1");
  }

  [TestMethod]
  public void ShouldCheckIfNotificationHasAtLeastOneError()
  {
    Notification notification = new();

    notification.AddError(new NotificationError("Professional", "Erro teste 1"));

    Assert.IsTrue(notification.HasErrors());
  }

  [TestMethod]
  public void ShouldBeGetAllErrorProps()
  {
    Notification notification = new();

    List<NotificationError> list = new(4)
    {
        new("Professional", "Erro teste 1"),
        new("Professional", "Erro teste 2"),
        new("Patient", "Erro teste 1"),
        new("Patient", "Erro teste 2")
    };

    notification.AddError(list[0]);
    notification.AddError(list[1]);
    notification.AddError(list[2]);
    notification.AddError(list[3]);

    Assert.AreEqual(notification.GetErrors().Count, list.Count);
    Assert.AreEqual(notification.GetErrors()[0], list[0]);
    Assert.AreEqual(notification.GetErrors()[1], list[1]);
    Assert.AreEqual(notification.GetErrors()[2], list[2]);
    Assert.AreEqual(notification.GetErrors()[3], list[3]);
  }
}