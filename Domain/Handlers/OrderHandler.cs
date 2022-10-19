using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Repositories;
using Store.Domain.Utils;

namespace Store.Domain.Handlers {
  public class OrderHandler : Notifiable, IHandler<CreateOrderCommand> {
    public OrderHandler(
      ICustomerRepository customerRepository,
      IDeliveryFeeRepository deliveryFeeRepository,
      IDiscountRepository discountRepository,
      IProductRepository productRepository,
      IOrderRepository orderRepository
    ) {
      this._customerRepository = customerRepository;
      this._deliveryFeeRepository = deliveryFeeRepository;
      this._discountRepository = discountRepository;
      this._productRepository = productRepository;
      this._orderRepository = orderRepository;
    }

    private readonly ICustomerRepository _customerRepository;
    private readonly IDeliveryFeeRepository _deliveryFeeRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public ICommandResult Handle(CreateOrderCommand command) {
      command.Validate();
      if (command.Invalid) {
        return new GenericCommandResult(false, "Pedido invalido", command.Notifications);
      }

      var customer = this._customerRepository.Get(command.Customer);
      var deliveryFee = this._deliveryFeeRepository.Get(command.ZipCode);
      var discount = this._discountRepository.Get(command.PromoCode);
      var products = this._productRepository.Get(ExtractGuids.Extract(command.Items)).ToList();
      var order = new Order(customer!, deliveryFee, discount!);
      foreach(var item in command.Items) {
        var product = products.Where(x => x.Id == item.ProductId).FirstOrDefault();
        order.AddItem(product, item.Quantity);
      }

      this.AddNotifications(order.Notifications);
      if (this.Invalid) {
        return new GenericCommandResult(false, "Erro ao gerar o pedido", this.Notifications);
      }

      this._orderRepository.Save(order);
      return new GenericCommandResult(true, $"Pedido {order.Number} gerado com sucesso!", order);
    }
  }
}
