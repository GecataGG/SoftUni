using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NetTraderSystem.Tests
{
    public class Tests
    {
        [TestFixture]
        public class TradingPlatformTests
        {
            private TradingPlatform tradingPlatform;
            private Product product1;
            private Product product2;

            [SetUp]
            public void SetUp()
            {
                tradingPlatform = new TradingPlatform(3);
                product1 = new Product("Laptop", "Electronics", 1500.00);
                product2 = new Product("Phone", "Electronics", 800.00);
            }

            [Test]
            public void Constructor_ShouldInitializeFieldsCorrectly()
            {
                Assert.AreEqual(0, tradingPlatform.Products.Count);
            }

            [Test]
            public void AddProduct_ShouldAddProduct_WhenInventoryHasSpace()
            {
                string result = tradingPlatform.AddProduct(product1);

                Assert.AreEqual($"Product {product1.Name} added successfully", result);
                Assert.Contains(product1, tradingPlatform.Products.ToList());
            }

            [Test]
            public void AddProduct_ShouldNotAddProduct_WhenInventoryIsFull()
            {
                tradingPlatform.AddProduct(new Product("Product1", "Category", 100.00));
                tradingPlatform.AddProduct(new Product("Product2", "Category", 200.00));
                tradingPlatform.AddProduct(new Product("Product3", "Category", 300.00));

                string result = tradingPlatform.AddProduct(product1);

                Assert.AreEqual("Inventory is full", result);
                Assert.IsFalse(tradingPlatform.Products.Contains(product1));
            }

            [Test]
            public void RemoveProduct_ShouldRemoveExistingProduct()
            {
                tradingPlatform.AddProduct(product1);

                bool result = tradingPlatform.RemoveProduct(product1);

                Assert.IsTrue(result);
                Assert.IsFalse(tradingPlatform.Products.Contains(product1));
            }

            [Test]
            public void RemoveProduct_ShouldReturnFalse_WhenProductDoesNotExist()
            {
                bool result = tradingPlatform.RemoveProduct(product1);

                Assert.IsFalse(result);
            }

            [Test]
            public void SellProduct_ShouldReturnProductAndRemoveIt_WhenProductExists()
            {
                tradingPlatform.AddProduct(product1);

                Product soldProduct = tradingPlatform.SellProduct(product1);

                Assert.AreEqual(product1, soldProduct);
                Assert.IsFalse(tradingPlatform.Products.Contains(product1));
            }

            [Test]
            public void SellProduct_ShouldReturnNull_WhenProductDoesNotExist()
            {
                Product soldProduct = tradingPlatform.SellProduct(product1);

                Assert.IsNull(soldProduct);
            }

            [Test]
            public void InventoryReport_ShouldReturnCorrectReport()
            {
                tradingPlatform.AddProduct(product1);
                tradingPlatform.AddProduct(product2);

                string report = tradingPlatform.InventoryReport();

                var expectedReportBuilder = new StringBuilder();
                expectedReportBuilder.AppendLine("Inventory Report:");
                expectedReportBuilder.AppendLine("Available Products: 2");
                expectedReportBuilder.AppendLine($"Name: {product1.Name}, Category: {product1.Category} - ${product1.Price:F2}");
                expectedReportBuilder.AppendLine($"Name: {product2.Name}, Category: {product2.Category} - ${product2.Price:F2}");
                string expectedReport = expectedReportBuilder.ToString().TrimEnd();

                Assert.AreEqual(expectedReport, report);
            }
        }
    }
}