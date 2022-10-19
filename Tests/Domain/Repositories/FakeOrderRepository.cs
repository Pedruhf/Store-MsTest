using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Domain.Repositories {
  public class FakeOrderRepository: IOrderRepository {
    public void Save(Order order) {}
  }
}
