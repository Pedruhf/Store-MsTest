using Flunt.Validations;
using Store.Domain.Enums;

namespace Store.Domain.Entities {
  public class Order : BaseEntity {
    public Order(Customer customer, decimal deliveryFee, Discount discount) {
      AddNotifications(
        new Contract().Requires().IsNotNull(customer, "Customer", "Cliente invalido")
      );

      this.Customer = customer;
      this.DeliveryFee = deliveryFee;
      this.Discount = discount;
      this.Customer = customer;
      this.Date = DateTime.Now;
      this.Number = Guid.NewGuid().ToString().Substring(0, 8);
      this.Status = EOrderStatus.WaitingPayment;
      this.Items = new List<OrderItem>();
    }

    public Customer Customer { get; private set; }
    public decimal DeliveryFee { get; private set; }
    public Discount Discount { get; private set; }
    public DateTime Date { get; private set; }
    public string Number { get; private set; }
    public EOrderStatus Status { get; private set; }
    public List<OrderItem> Items { get; private set; }

    public void AddItem(Product product, int quantity) {
      var item = new OrderItem(product, quantity);
      if(item.Valid) {
        Items.Add(item);
      }
    }

    public decimal Total() {
      decimal total = 0;
      foreach (OrderItem item in Items) {
        total =+ item.Total();
      }

      total += this.DeliveryFee;
      total -= this.Discount != null ? Discount.Value() : 0;
      return total;
    }

    public void Pay(decimal amount) {
      if (amount == Total()) {
        this.Status = EOrderStatus.WaitingDelivery;
      }
    }

    public void Cancel() {
      this.Status = EOrderStatus.Canceled;
    }
  }
}
