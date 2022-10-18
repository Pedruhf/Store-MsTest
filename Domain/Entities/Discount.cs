namespace Store.Domain.Entities {
  public class Discount : BaseEntity {
    public Discount(decimal amount, DateTime expireDate) {
      this.Amount = amount;
      this.ExpireDate = expireDate;
    }

    public decimal Amount { get; private set; }
    public DateTime ExpireDate { get; private set; }

    public bool IsValid() {
      return DateTime.Compare(DateTime.Now, this.ExpireDate) < 0;
    }

    public decimal Value() {
      if (this.IsValid()) {
        return this.Amount;
      }
      return 0;
    }
  }
}
