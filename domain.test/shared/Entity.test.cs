namespace domain.test;

[TestClass]
public class EntityTest
{
  [TestMethod]
  public void ShouldBeCreateAEntityWithoutId()
  {
    Entity entity = new TestEntity(null);

    Assert.IsNotNull(entity.Id);
  }

  [TestMethod]
  public void ShouldBeCreateAEntityWithValidId()
  {
    Guid guid = new();
    Entity entity = new TestEntity(guid.ToString());

    Assert.AreEqual(entity.Id, guid);
  }

  [TestMethod]
  public void ShouldBeCreateAEntityWithInvalidId()
  {
    try
    {
      Entity entity = new TestEntity("123");

      Assert.Fail();
    }
    catch (DomainException)
    {
    }

  }
}

class TestEntity : Entity
{
  public TestEntity(string? id) : base(id)
  {
  }

  public override void Validate()
  {
    throw new NotImplementedException();
  }
}