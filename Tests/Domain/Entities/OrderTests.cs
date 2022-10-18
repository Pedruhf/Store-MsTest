using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Tests.Domain.Entities {
  [TestClass]
  public class OrderTests {
    private readonly Customer _customer = new Customer("Peitin", "peitin@gmail.com");
    private readonly Product _product = new Product("Produto 1", 10, true);
    private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(5));

    [TestMethod]
    [TestCategory("Domain")]
    public void Should_generate_a_order_number_with_8_characters() {
      var order = new Order(this._customer, 0, null);
      Assert.AreEqual(order.Number.Length, 8);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Should_have_waiting_payment_status() {
      var order = new Order(this._customer, 0, null);
      Assert.AreEqual(order.Status, EOrderStatus.WaitingPayment);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Should_have_waiting_delivery_status() {
      var order = new Order(this._customer, 0, null);
      order.AddItem(this._product, 2);
      order.Pay(20);
      Assert.AreEqual(order.Status, EOrderStatus.WaitingDelivery);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Should_have_canceled_status() {
      var order = new Order(this._customer, 0, null);
      order.Cancel();
      Assert.AreEqual(order.Status, EOrderStatus.Canceled);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Should_not_add_a_invalid_item() {
      var order = new Order(this._customer, 0, null);
      order.AddItem(null, 1);
      Assert.AreEqual(order.Items.Count(), 0);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Should_not_add_a_invalid_item_2() {
      var order = new Order(this._customer, 0, null);
      order.AddItem(this._product, 0);
      order.AddItem(this._product, -1);
      Assert.AreEqual(order.Items.Count(), 0);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Should_have_total_equal_to_50() {
      var order = new Order(this._customer, 0, null);
      order.AddItem(this._product, 5);
      Assert.AreEqual(order.Total(), 50);
      Assert.AreEqual(order.Items.Count(), 1);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Should_have_total_equal_to_42_with_discount_and_delivery_fee() {
      var order = new Order(this._customer, 2, this._discount);
      order.AddItem(this._product, 5);
      Assert.AreEqual(order.Total(), 42);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Should_have_total_equdal_to_55_with_expired_discount_and_delivery_fee() {
      var expiredDiscount = new Discount(5, DateTime.Now.AddMilliseconds(-1));
      var order = new Order(this._customer, 5, expiredDiscount);
      order.AddItem(this._product, 5);
      Assert.AreEqual(order.Total(), 55);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Should_be_a_invalid_product_when_customer_is_invalid() {
      var order = new Order(null, 0, this._discount);
      Assert.AreEqual(order.Valid, false);
    }
  }
}
