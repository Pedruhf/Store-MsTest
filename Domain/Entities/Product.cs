namespace Store.Domain.Entities {
  public class Product : BaseEntity {
    public Product(String title, decimal price, bool active) {
      this.Title = title;
      this.Price = price;
      this.Active = active;
    }

    public string Title { get; private set; }
    public decimal Price { get; private set; }
    public bool Active { get; private set; }
  }
}
