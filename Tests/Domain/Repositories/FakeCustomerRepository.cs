using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Domain.Repositories {
  public class FakeCustomerRepository: ICustomerRepository {
    public Customer? Get(string document) {
      if (document == "00100200300") {
        return new Customer("Pedro Freitas", "pedroh.ufrn@gmail.com");
      }

      return null;
    }

    public static string GetValidCustomer() {
      return "00100200300";
    }
  }
}
