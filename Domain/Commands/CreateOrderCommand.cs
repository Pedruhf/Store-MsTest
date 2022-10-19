using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;

namespace Store.Domain.Commands {
  public class CreateOrderCommand : Notifiable, ICommand {
    public CreateOrderCommand() {
      this.Customer = "";
      this.ZipCode = "";
      this.PromoCode = "";
      this.Items = new List<CreateOrderItemCommand>();
    }

    public CreateOrderCommand(string customer, string zipCode, string promoCode, IList<CreateOrderItemCommand> items) {
      this.Customer = customer;
      this.ZipCode = zipCode;
      this.PromoCode = promoCode;
      this.Items = items;
    }

    public string Customer { get; set; }
    public string ZipCode { get; set; }
    public string PromoCode { get; set; }
    public IList<CreateOrderItemCommand> Items { get; set; }

    public void Validate() {
      AddNotifications(
        new Contract()
          .Requires()
          .HasLen(Customer, 11, "Customer", "Cliente invalido")
          .HasLen(ZipCode, 8, "ZipCode", "CEP invalido")
          .IsGreaterOrEqualsThan(Items.Count(), 1, "Items", "O pedido deve ter pelo menos 1 item")
      );
    }
  }
}
