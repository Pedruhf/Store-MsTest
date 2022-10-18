namespace Store.Domain.Entities {
  public class Customer : BaseEntity {
    public Customer(String name, String email) {
      this.Name = name;
      this.Email = email;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
  }
}
