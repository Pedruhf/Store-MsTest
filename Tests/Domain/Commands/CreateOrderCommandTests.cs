using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Commands;

namespace Store.Tests.Domain.Commands {
  [TestClass]
  public class CreateOrderCommandTests {
    [TestMethod]
    [TestCategory("Handlers")]
    public void Should_not_create_a_product_if_command_is_invalid() {
      var command = new CreateOrderCommand();
      command.ZipCode = "59076260";
      command.PromoCode = "ABC12345";
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
      command.Validate();

      Assert.AreEqual(command.Valid, false);
      Assert.AreEqual(command.Notifications.First().Message, "Cliente invalido");
    }
  }
}
