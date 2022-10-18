using Flunt.Notifications;

namespace Store.Domain.Entities {
  public abstract class BaseEntity : Notifiable {
    public BaseEntity() {
      this.Id = Guid.NewGuid();
    }

    public Guid Id { get; private set; }
  }
}
