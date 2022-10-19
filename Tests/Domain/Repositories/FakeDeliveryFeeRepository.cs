using Store.Domain.Repositories;

namespace Store.Tests.Domain.Repositories {
  public class FakeDeliveryFeeRepository: IDeliveryFeeRepository {
    public decimal Get(string zipCode) {
      return 10;
    }
  }
}
