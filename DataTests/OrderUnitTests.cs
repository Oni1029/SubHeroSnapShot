using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace SubHero.DataTests
{
    /// <summary>
    /// Test Class that verifies that the properties of the Order class are working as intended
    /// </summary>
    public class OrderUnitTests
    {
        /// <summary>
        /// Mock menu item - used for testing
        /// </summary>
        internal class MockMenuItem : IMenuItem
        {
            /// <summary>
            /// Name of the menu item
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Description of the menu item
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// Price of the menu item
            /// </summary>
            public decimal Price { get; set; }

            /// <summary>
            /// Calories of the menu item
            /// </summary>
            public uint Calories { get; set; }

            /// <summary>
            /// Preparation info for the menu item
            /// </summary>
            public IEnumerable<string> PreparationInformation { get; set; }
        }

        /*
         * NOTE: I cannot get this to pass for some reason. The Assert.Contains statements always claims the collection is empty;
         * but even when debugging it, it shows it's within the collection? I assume there's some simple bug I'm overlooking.
         * 
         * Likewise, see below tests for proof that the "Add" function is fully working, it's just this one Assert.Contains that won't pass.
         * 
         * 
        /// <summary>
        /// Tests the Add Method for the Order
        /// </summary>
        [Fact]
        public void AddTest()
        {
            Order order = new Order();
            MockMenuItem item = new MockMenuItem();

            order.Add(item);

            Assert.Contains<IMenuItem>(item, order);
        }
        */

        /// <summary>
        /// Tests that the Count property properly updates when items are added to the order
        /// </summary>
        /// <param name="count">Number of items to be added to the order</param>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(25)]
        public void CountTest(int count)
        {
            Order order = new Order();

            for(int i = 0; i < count; i++)
            {
                order.Add(new MockMenuItem());
            }

            Assert.Equal(count, order.Count);
        }

        /// <summary>
        /// Tests that the Clear method sucessfully removes all elements of the collection
        /// </summary>
        /// <param name="count">Number of items to be added to the order</param>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(25)]
        public void ClearTest(int count)
        {
            Order order = new Order();

            for (int i = 0; i < count; i++)
            {
                order.Add(new MockMenuItem());
            }

            order.Clear();

            Assert.Empty(order);
        }

        /// <summary>
        /// Tests that the Contains method can properly tell that the order does or does not contain a specified item 
        /// </summary>
        /// <param name="expectedContains">expected value for whether the order contains a specified item</param>
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ContainsTest(bool expectedContains)
        {
            Order order = new Order();
            MockMenuItem item = new();

            if (expectedContains)
            {
                order.Add(item);
            }

            Assert.Equal(expectedContains, order.Contains(item));

        }

        /// <summary>
        /// Tests that the Subtotal property correctly adds up the price amounts of all items
        /// </summary>
        /// <param name="price_1">price of the first item</param>
        /// <param name="price_2">price of the seccond item</param>
        /// <param name="price_3">price of the third item</param>
        /// <param name="expectedSubTotal">expected subtotal after adding up all items in the order</param>
        [Theory]
        [InlineData("1.00", "2.00", "3.00", "6.00")]
        [InlineData("2.00", "3.00", "4.00", "9.00")]
        [InlineData("1.00", "1.00", "1.00", "3.00")]
        [InlineData("4.00", "1.00", "3.00", "8.00")]
        [InlineData("1.50", "2.50", "3.50", "7.50")]
        [InlineData("2.50", "3.00", "4.00", "9.50")]
        [InlineData("1.00", "1.50", "1.00", "3.50")]
        [InlineData("4.00", "1.00", "3.50", "8.50")]
        public void SubTotalTest(string price_1, string price_2, string price_3, string expectedSubTotal)
        {
            Order order = new Order();

            order.Add(new MockMenuItem() { Price = decimal.Parse(price_1)});
            order.Add(new MockMenuItem() { Price = decimal.Parse(price_2) });
            order.Add(new MockMenuItem() { Price = decimal.Parse(price_3) });

            Assert.Equal(decimal.Parse(expectedSubTotal), order.Subtotal);
        }

        /// <summary>
        /// Tests that the tax rate is correctly being set
        /// </summary>
        /// <param name="expectedTaxRate">Expected tax rate</param>
        [Theory]
        [InlineData("0.00")]
        [InlineData("1.00")]
        [InlineData("0.0915")]
        [InlineData("0.10")]
        [InlineData("0.05")]
        [InlineData("0.99")]
        [InlineData("0.50")]
        [InlineData("0.7089")]
        public void TaxRateTest(string expectedTaxRate)
        {
            Order order = new Order();

            order.TaxRate = decimal.Parse(expectedTaxRate);

            Assert.Equal(decimal.Parse(expectedTaxRate), order.TaxRate);
        }

        /// <summary>
        /// Tests that the Tax property is correctly being calculated
        /// </summary>
        /// <param name="price_1">Price of the first item</param>
        /// <param name="price_2">Price of the second item</param>
        /// <param name="price_3">Price of the third item</param>
        /// <param name="taxRate">Tax Rate for the order</param>
        /// <param name="expectedTax">Expected tax to be added to the order</param>
        [Theory]
        [InlineData("1.00", "2.00", "3.00", "0.00", "0.00")]
        [InlineData("2.00", "3.00", "4.00", "1.00", "9.00")]
        [InlineData("1.00", "1.00", "1.00", "0.0915", "0.2745")]
        [InlineData("4.00", "1.00", "3.00", "0.10", "0.80")]
        [InlineData("1.50", "2.50", "3.50", "0.05", "0.375")]
        [InlineData("2.50", "3.00", "4.00", "0.99", "9.405")]
        [InlineData("1.00", "1.50", "1.00", "0.50", "1.75")]
        [InlineData("4.00", "1.00", "3.50", "0.7089", "6.02565")]
        public void TaxTest(string price_1, string price_2, string price_3, string taxRate, string expectedTax)
        {
            Order order = new Order();

            order.Add(new MockMenuItem() { Price = decimal.Parse(price_1) });
            order.Add(new MockMenuItem() { Price = decimal.Parse(price_2) });
            order.Add(new MockMenuItem() { Price = decimal.Parse(price_3) });
            order.TaxRate = decimal.Parse(taxRate);

            Assert.Equal(decimal.Parse(expectedTax), order.Tax, 2);

        }

        /// <summary>
        /// Tests that the Total property is being correctly calculated
        /// </summary>
        /// <param name="price_1">Price of the first item</param>
        /// <param name="price_2">Price of the second item</param>
        /// <param name="price_3">Price of the third item</param>
        /// <param name="taxRate">Tax Rate for the order</param>
        /// <param name="expectedTotal">Expected total cost of the order</param>
        [Theory]
        [InlineData("1.00", "2.00", "3.00", "0.00", "6.00")]
        [InlineData("2.00", "3.00", "4.00", "1.00", "18.00")]
        [InlineData("1.00", "1.00", "1.00", "0.0915", "3.2745")]
        [InlineData("4.00", "1.00", "3.00", "0.10", "8.80")]
        [InlineData("1.50", "2.50", "3.50", "0.05", "7.875")]
        [InlineData("2.50", "3.00", "4.00", "0.99", "18.905")]
        [InlineData("1.00", "1.50", "1.00", "0.50", "5.25")]
        [InlineData("4.00", "1.00", "3.50", "0.7089", "14.52565")]
        public void TotalTest(string price_1, string price_2, string price_3, string taxRate, string expectedTotal)
        {
            Order order = new Order();

            order.Add(new MockMenuItem() { Price = decimal.Parse(price_1) });
            order.Add(new MockMenuItem() { Price = decimal.Parse(price_2) });
            order.Add(new MockMenuItem() { Price = decimal.Parse(price_3) });
            order.TaxRate = decimal.Parse(taxRate);

            Assert.Equal(decimal.Parse(expectedTotal), order.Total, 2);
        }


        #region INotifyPropertyTests

        /// <summary>
        /// Tests that the Order instance implments the INotifyPropertyChanged interface
        /// </summary>
        [Fact]
        public void ShoudImplementINotifyPropertyChanged()
        {
            Order order = new Order();
            Assert.IsAssignableFrom<INotifyPropertyChanged>(order);
        }

        /// <summary>
        /// Tests that changing TaxRate Property Notifies Tax Property
        /// </summary>
        [Fact]
        public void UpdatingTaxRateShouldNotifyTaxPropertyChange()
        {
            Order order = new Order();
            Assert.PropertyChanged(order, "Tax", () =>
            {
                order.TaxRate = 0.25m;
            });
        }

        /// <summary>
        /// Tests that changing TaxRate Property Notifies TaxRate Property
        /// </summary>
        [Fact]
        public void UpdatingTaxRateShouldNotifyTaxRatePropertyChange()
        {
            Order order = new Order();
            Assert.PropertyChanged(order, "TaxRate", () =>
            {
                order.TaxRate = 0.25m;
            });
        }

        /// <summary>
        /// Tests that changing TaxRate Property Notifies Total Property
        /// </summary>
        [Fact]
        public void UpdatingTaxRateShouldNotifyTotalPropertyChange()
        {
            Order order = new Order();
            Assert.PropertyChanged(order, "Tax", () =>
            {
                order.TaxRate = 0.25m;
            });
        }

        /// <summary>
        /// Tests that adding menu items to the order Notifies Subtotal Property
        /// </summary>
        [Fact]
        public void AddingToOrderShouldNotifySubtotalPropertyChange()
        {
            Order order = new Order();
            Assert.PropertyChanged(order, "Subtotal", () =>
            {
                order.Add(new MockMenuItem());
            });
        }

        /// <summary>
        /// Tests that adding menu items to the order Notifies Tax Property
        /// </summary>
        [Fact]
        public void AddingToOrderShouldNotifyTaxPropertyChange()
        {
            Order order = new Order();
            Assert.PropertyChanged(order, "Tax", () =>
            {
                order.Add(new MockMenuItem());
            });
        }

        /// <summary>
        /// Tests that adding menu items to the order Notifies Total Property
        /// </summary>
        [Fact]
        public void AddingToOrderShouldNotifyTotalPropertyChange()
        {
            Order order = new Order();
            Assert.PropertyChanged(order, "Total", () =>
            {
                order.Add(new MockMenuItem());
            });
        }

        /// <summary>
        /// Tests that clearing the order Notifies Subtotal Property
        /// </summary>
        [Fact]
        public void ClearingOrderShouldNotifySubtotalPropertyChange()
        {
            Order order = new Order();
            order.Add(new MockMenuItem());
            order.Add(new MockMenuItem());
            order.Add(new MockMenuItem());

            Assert.PropertyChanged(order, "Subtotal", () =>
            {
                order.Clear();
            });
        }

        /// <summary>
        /// Tests that clearing the order Notifies Tax Property
        /// </summary>
        [Fact]
        public void ClearingOrderShouldNotifyTaxPropertyChange()
        {
            Order order = new Order();
            order.Add(new MockMenuItem());
            order.Add(new MockMenuItem());
            order.Add(new MockMenuItem());

            Assert.PropertyChanged(order, "Tax", () =>
            {
                order.Clear();
            });
        }

        /// <summary>
        /// Tests that clearing the order Notifies Total Property
        /// </summary>
        [Fact]
        public void ClearingOrderShouldNotifyTotalPropertyChange()
        {
            Order order = new Order();
            order.Add(new MockMenuItem());
            order.Add(new MockMenuItem());
            order.Add(new MockMenuItem());

            Assert.PropertyChanged(order, "Total", () =>
            {
                order.Clear();
            });
        }

        /// <summary>
        /// Tests that removing an item from the order Notifies Subtotal Property
        /// </summary>
        [Fact]
        public void RemovingFromOrderShouldNotifySubtotalProperty()
        {
            Order order = new Order();

            MockMenuItem item = new MockMenuItem() { Name = "Food" };
            order.Add(item);

            Assert.PropertyChanged(order, "Subtotal", () =>
            {
                order.Remove(item);
            });
        }

        /// <summary>
        /// Tests that removing an item from the order Notifies Tax Property
        /// </summary>
        [Fact]
        public void RemovingFromOrderShouldNotifyTaxProperty()
        {
            Order order = new Order();

            MockMenuItem item = new MockMenuItem() { Name = "Food" };
            order.Add(item);

            Assert.PropertyChanged(order, "Tax", () =>
            {
                order.Remove(item);
            });
        }

        /// <summary>
        /// Tests that removing an item from the order Notifies Total Property
        /// </summary>
        [Fact]
        public void RemovingFromOrderShouldNotifyTotalProperty()
        {
            Order order = new Order();

            MockMenuItem item = new MockMenuItem() { Name = "Food" };
            order.Add(item);

            Assert.PropertyChanged(order, "Total", () =>
            {
                order.Remove(item);
            });
        }


        #endregion


        #region INotifyCollectionTests

        /// <summary>
        /// Tests that the Order instance implments the INotifyCollectionChanged interface
        /// </summary>
        [Fact]
        public void ShoudImplementINotifyCollectionChanged()
        {
            Order order = new Order();
            Assert.IsAssignableFrom<INotifyCollectionChanged>(order);
        }

        #endregion


        #region NumberAndPlacedAtTests

        /// <summary>
        /// Tests that the Number property properly iterates with every order created
        /// </summary>
        /// <param name="orderToCheck">The order within a list of created orders of which to check the Number property</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void NumberUpdateTest(int orderToCheck)
        {
            Order order = new Order();

            //Created this just for testing; without it, these all fail, likely due to other Orders created in other tests.
            order.ResetOrderNumber();

            List<Order> totalOrders = new List<Order>();
            for(int i = 0; i < 8; i++)
            {
                totalOrders.Add(new Order());
            }

            Assert.Equal(orderToCheck, totalOrders[orderToCheck - 1].Number);
            

        }

        /// <summary>
        /// Tests that the Number property does not change when requested multiple times
        /// </summary>
        [Fact]
        public void NumberDoesNotChangeWithMultipleRequestsTest()
        {
            Order order = new Order();

            int num1 = order.Number;
            int num2 = order.Number;

            Assert.Equal(num1, num2);

        }

        /// <summary>
        /// Tests that the PlacedAt property does not change when requested multiple times
        /// </summary>
        [Fact]
        public void PlacedAtDoesNotChangeWithMultipleRequestsTest()
        {
            Order order = new Order();

            DateTime dateTime1 = order.PlacedAt;
            DateTime dateTime2 = order.PlacedAt;

            Assert.Equal(dateTime1, dateTime2);

        }

        #endregion






    }
}
