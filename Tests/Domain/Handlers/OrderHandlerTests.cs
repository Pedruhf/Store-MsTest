
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Repositories;
using Store.Tests.Domain.Repositories;

namespace Store.Tests.Domain.Handlers {
  [TestClass]
  public class OrderHandlerTests {
    public OrderHandlerTests() {
      this._customerRepository = new FakeCustomerRepository();
      this._deliveryFeeRepository = new FakeDeliveryFeeRepository();
      this._discountRepository = new FakeDiscountRepository();
      this._productRepository = new FakeProductRepository();
      this._orderRepository = new FakeOrderRepository();
      this._handler = new OrderHandler(this._customerRepository, this._deliveryFeeRepository, this._discountRepository, this._productRepository, this._orderRepository);
    }

    private readonly ICustomerRepository _customerRepository;
    private readonly IDeliveryFeeRepository _deliveryFeeRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IHandler<CreateOrderCommand> _handler;

    [TestMethod]
    [TestCategory("Handlers")]
    public void Should_not_generate_an_product_if_client_is_invalid() {
      var command = new CreateOrderCommand();
      command.Customer = "invalid_customer";
      command.ZipCode = "59076260";
      command.PromoCode = "ABC12345";
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

      this._handler.Handle(command);
      Assert.AreEqual(command.Notifications.First().Message, "Cliente invalido");
      Assert.AreEqual(command.Valid, false);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Should_not_generate_an_order_if_zipCode_is_invalid() {
      var command = new CreateOrderCommand();
      command.Customer = FakeCustomerRepository.GetValidCustomer();
      command.ZipCode = "invalid_zipCode";
      command.PromoCode = "ABC12345";
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

      this._handler.Handle(command);
      Assert.AreEqual(command.Notifications.First().Message, "CEP invalido");
      Assert.AreEqual(command.Valid, false);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Should_not_generate_an_order_if_items_is_not_provided() {
      var command = new CreateOrderCommand();
      command.Customer = FakeCustomerRepository.GetValidCustomer();
      command.ZipCode = "59076260";
      command.PromoCode = "ABC12345";

      this._handler.Handle(command);
      Assert.AreEqual(command.Notifications.First().Message, "O pedido deve ter pelo menos 1 item");
      Assert.AreEqual(command.Valid, false);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Should_generate_an_order_if_promoCode_is_not_provided() {
      var command = new CreateOrderCommand();
      command.Customer = FakeCustomerRepository.GetValidCustomer();
      command.ZipCode = "59076260";
      command.PromoCode = "";

      this._handler.Handle(command);
      Assert.AreEqual(command.Notifications.First().Message, "O pedido deve ter pelo menos 1 item");
      Assert.AreEqual(command.Valid, false);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Should_generate_an_order_on_success() {
      var command = new CreateOrderCommand();
      command.Customer = FakeCustomerRepository.GetValidCustomer();
      command.ZipCode = "59076260";
      command.PromoCode = "ABC12345";
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
      command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

      var handler = new OrderHandler(this._customerRepository, this._deliveryFeeRepository, this._discountRepository, this._productRepository, this._orderRepository);

      handler.Handle(command);
      Assert.AreEqual(handler.Valid, true);
    }
  }
}
