using System;
using Xunit;
using Bangazon.Orders;
using System.Collections.Generic;

namespace Banganzon.Tests
{
    public class OrderTests
    {
        [Fact]
        public void TestTheTestingFramework()
        {
            Assert.True(true);
        }
        // [Fact]
        // public void FailingTest()
        // {
        //     Assert.Equal(3,5);
        // }
        [Fact]
        public void OrdersCanExist()
        {
            Order ord = new Order();
            Assert.NotNull(ord);
        }

        [Fact]
        public void NewOrdersHaveAGuid()
        {
            Order ord = new Order();
            Assert.NotNull(ord.orderNumber);
            Assert.IsType<Guid>(ord.orderNumber);
            // You can see that the same method can have several Tests on it
        }

        [Fact]

        public void NewOrdersShouldHaveAnEmptyProductList()
        {
            Order ord = new Order ();
            Assert.NotNull(ord.products);
            Assert.IsType<List<string>>(ord.products);
            Assert.Empty(ord.products);
        }

        [Theory]
        [InlineDataAttribute("Banana")]
        [InlineDataAttribute("1872364817237648723164")]
        [InlineDataAttribute("A product with space")]
        [InlineDataAttribute("Product that has a, comma?")]
        public void OrdersCanHaveProductsAddedToThem(string product)
        {
            Order ord = new Order();
            ord.addProduct(product);
            Assert.Equal(1, ord.products.Count);
            Assert.Contains<string>(product, ord.products);
        }

        [Theory]
        [InlineDataAttribute("Product")]
        [InlineDataAttribute("Product, another prodcut")]
        [InlineDataAttribute("a first product, someother, yet another")]
        [InlineDataAttribute("prod 1, prod 2, prod 3, prod 4")]
        public void OrdersCanListProductsForTerminalDisplay(string productsStr)
        {
            string[]products = productsStr.Split(new char[] {','});
            Order ord = new Order();
            foreach (string product in products)
            {
                ord.addProduct(product);
            }
            foreach (string product in products)
            {
                Assert.Contains($"\nYou ordered {product}", ord.listProducts());
            }
        }
        [Fact]
        public void OrdersCanHaveProductsRemovedFromThem()
        {
            Order ord = new Order();
            ord.addProduct("Product");
            ord.addProduct("Banana");
            ord.addProduct("Honeydew Melon");
            
            ord.removeProduct("Banana");

            Assert.Equal(2, ord.products.Count);
            Assert.DoesNotContain<string>("Banana", ord.products);
        }
        public void OrdersCanNotRemoveAProductThatDoesntExists()
        {
            Order ord = new Order();
            ord.addProduct("Product");
            ord.addProduct("Banana");
            ord.addProduct("Honeydew Melon");
            
            ord.removeProduct("Pineapple");

            Assert.Equal(2, ord.products.Count);
        }
        [Theory]
        [InlineDataAttribute("Banana")]
        [InlineDataAttribute("Pineapple")]
        public void RemoveMethodReturnsBooleanIndicatingIfProductWasRemoved(string product)
        {
            Order ord = new Order();
            ord.addProduct("Banana Bread");
            ord.addProduct("Product");
            ord.addProduct("Banana");
            ord.addProduct("Honeydew Melon");

            bool removed = ord.removeProduct(product);

            if (product == "Banana")
            {
                Assert.True(removed);
            }
            if (product == "Pineapple")
            {
                Assert.False(removed);
            }
        }
        [Fact]
        public void AllProdcutsFromAnOrderCanBeDeleted()
        {
            Order ord = new Order();
            ord.addProduct("Banana Bread");
            ord.addProduct("Product");
            ord.addProduct("Banana");
            ord.addProduct("Honeydew Melon");

            ord.removeProduct();

            Assert.Empty(ord.products);
        }
    }
}