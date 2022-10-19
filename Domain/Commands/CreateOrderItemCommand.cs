using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands {
  public class CreateOrderItemCommand : Notifiable, ICommand {
    public CreateOrderItemCommand(Guid productId, int quantity) {
      this.ProductId = productId;
      this.Quantity = quantity;
    }

    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public void Validate() {
      AddNotifications(
        new Contract()
          .Requires()
          .HasLen(ProductId.ToString(), 32, "ProductId", "Produto invalido")
          .IsGreaterThan(Quantity, 0, "Quantity", "Quantidade invalida")
      );
    }
  }
}
