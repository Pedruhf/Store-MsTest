using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Queries;

namespace Store.Tests.Domain.Queries {
  [TestClass]
  public class ProductQueriesTests {
    private IList<Product> _products;

    public ProductQueriesTests() {
      this._products = new List<Product>();
      this._products.Add(new Product("Produto 01", 10, true));
      this._products.Add(new Product("Produto 02", 20, true));
      this._products.Add(new Product("Produto 03", 30, true));
      this._products.Add(new Product("Produto 04", 40, false));
      this._products.Add(new Product("Produto 05", 50, false));
    }

    [TestMethod]
    [TestCategory("Queries")]
    public void Should_return_3_active_products() {
      var result = this._products.AsQueryable().Where(ProductQueries.GetActiveProducts());
      Assert.AreEqual(result.Count(), 3);
    }
  }
}
