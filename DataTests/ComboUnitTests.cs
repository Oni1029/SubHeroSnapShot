using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubHero.Data.Entrees;
using SubHero.Data.Drinks;
using SubHero.Data.Sides;
using System.ComponentModel;
using System.Threading;

namespace SubHero.DataTests
{
    /// <summary>
    /// Test Class that verifies that the properties of the Combo class are working as intended
    /// </summary>
    public class ComboUnitTests
    {
        /// <summary>
        /// Tests that this item can be cast from IMenuItem
        /// </summary>
        [Fact]
        public void IMenuItemCastTest()
        {
            Combo c = new Combo();

            Assert.IsAssignableFrom<IMenuItem>(c);
        }

        /// <summary>
        /// Tests that the default sandwich for combos is a CustomSandwich
        /// </summary>
        [Fact]
        public void DefaultSandwichTest()
        {
            Combo c = new Combo();

            Assert.IsType<CustomSandwich>(c.SandwichChoice);
        }

        /// <summary>
        /// Tests that the default side for combos is Chips
        /// </summary>
        [Fact]
        public void DefaultSideTest()
        {
            Combo c = new Combo();

            Assert.IsType<Chips>(c.SideChoice);
        }

        /// <summary>
        /// Tests that the default Drink for combos is a FountainDrink
        /// </summary>
        [Fact]
        public void DefaultDrinkTest()
        {
            Combo c = new Combo();

            Assert.IsType<FountainDrink>(c.DrinkChoice);
        }


        /// <summary>
        /// Tests that the default calories for a combo is correct
        /// </summary>
        [Fact]
        public void DefaultCaloriesTest()
        {
            Combo c = new Combo();

            uint expectedCals = 250 + 240 + 240;

            Assert.Equal(expectedCals, c.Calories);
        }

        /// <summary>
        /// Tests that the default price for a combo is correct
        /// </summary>
        [Fact]
        public void DefaultPriceTest()
        {
            Combo c = new Combo();

            decimal expectedPrice = ((5.99m + 2.00m + 1.95m) * 0.80m);

            Assert.Equal(expectedPrice, c.Price);
        }

        /// <summary>
        /// Tests that the Combo instance implments the INotifyPropertyChanged interface
        /// </summary>
        [Fact]
        public void ShoudImplementINotifyPropertyChanged()
        {
            Combo d = new();
            Assert.IsAssignableFrom<INotifyPropertyChanged>(d);
        }

        /// <summary>
        /// Tests that the SandwichCoice within this Combo instance property notifies property changes
        /// </summary>
        /// <remarks>
        /// Not currently checking for EntreeOptions update here - while it is notified during the set of SandwichChoice,
        /// it is only changed by the comboBox selection in the UI, not a back-end change. I'll be asking in class how to approach this better
        /// as right now I get a "set" error when attempting to verify EntreeOptions's property update.
        /// </remarks>
        /// <param name="entree">New entree to be set in the SandwichChoice property</param>
        /// <param name="propertyNames">Array of property names expected to be notifed</param>
        [Theory]
        [InlineData("California", new string[] {"SandwichChoice", "Price", "Calories", "PreparationInformation"})]
        [InlineData("ClubSub", new string[] { "SandwichChoice", "Price", "Calories", "PreparationInformation" })]
        [InlineData("Custom", new string[] { "SandwichChoice", "Price", "Calories", "PreparationInformation" })]
        [InlineData("Italian", new string[] { "SandwichChoice", "Price", "Calories", "PreparationInformation" })]
        [InlineData("Mediterranean", new string[] { "SandwichChoice", "Price", "Calories", "PreparationInformation" })]
        [InlineData("TurkeyCranberry", new string[] { "SandwichChoice", "Price", "Calories", "PreparationInformation" })]
        [InlineData("Veggie", new string[] { "SandwichChoice", "Price", "Calories", "PreparationInformation" })]
        public void CheckSandwichChoiceChangeProperties(string entree, string[] propertyNames)
        {
            Combo d = new();
            for(int i = 0; i < propertyNames.Length; i++)
            {
                Assert.PropertyChanged(d, propertyNames[i], () =>
                {
                    switch (entree)
                    {
                        case "California":
                            d.SandwichChoice = new CaliforniaClubWrap();
                            break;
                        case "ClubSub":
                            d.SandwichChoice = new ClubSub();
                            break;
                        case "Custom":
                            d.SandwichChoice = new CustomSandwich();
                            break;
                        case "Italian":
                            d.SandwichChoice = new ItalianSub();
                            break;
                        case "Mediterranean":
                            d.SandwichChoice = new MediterraneanWrap();
                            break;
                        case "TurkeyCranberry":
                            d.SandwichChoice = new TurkeyCranberrySandwich();
                            break;
                        case "Veggie":
                            d.SandwichChoice = new VeggieSandwich();
                            break;
                    }
                });
            }

        }

        /// <summary>
        /// Tests that the SideCoice within this Combo instance property notifies property changes
        /// </summary>
        /// <param name="side">New entree to be set in the SideChoice property</param>
        /// <param name="propertyNames">Array of property names expected to be notifed</param>
        [Theory]
        [InlineData("Apple", new string[] { "SideChoice", "Price", "Calories", "PreparationInformation" })]
        [InlineData("Chips", new string[] { "SideChoice", "Price", "Calories", "PreparationInformation" })]
        [InlineData("Cookies", new string[] { "SideChoice", "Price", "Calories", "PreparationInformation" })]
        [InlineData("SideSalad", new string[] { "SideChoice", "Price", "Calories", "PreparationInformation" })]
        public void CheckSideChoiceChangeProperties(string side, string[] propertyNames)
        {
            Combo d = new();
            for (int i = 0; i < propertyNames.Length; i++)
            {
                Assert.PropertyChanged(d, propertyNames[i], () =>
                {
                    switch (side)
                    {
                        case "Apple":
                            d.SideChoice = new Apple();
                            break;
                        case "Chips":
                            d.SideChoice = new Chips();
                            break;
                        case "Cookies":
                            d.SideChoice = new Cookies();
                            break;
                        case "SideSalad":
                            d.SideChoice = new SideSalad();
                            break;
                    }
                });
            }

        }

        /// <summary>
        /// Tests that the DrinkChoice within this Combo instance property notifies property changes
        /// </summary>
        /// <param name="drink">New entree to be set in the DrinkChoice property</param>
        /// <param name="propertyNames">Array of property names expected to be notifed</param>
        [Theory]
        [InlineData("Fountain", new string[] { "DrinkChoice", "Price", "Calories", "PreparationInformation" })]
        [InlineData("Tea", new string[] { "DrinkChoice", "Price", "Calories", "PreparationInformation" })]
        [InlineData("Lemonade", new string[] { "DrinkChoice", "Price", "Calories", "PreparationInformation" })]
        public void CheckDrinkChoiceChangeProperties(string drink, string[] propertyNames)
        {
            Combo d = new();
            for (int i = 0; i < propertyNames.Length; i++)
            {
                Assert.PropertyChanged(d, propertyNames[i], () =>
                {
                    switch (drink)
                    {
                        case "Fountain":
                            d.DrinkChoice = new FountainDrink();
                            break;
                        case "Tea":
                            d.DrinkChoice = new IcedTea();
                            break;
                        case "Lemonade":
                            d.DrinkChoice = new Lemonade();
                            break;
                    }
                });
            }

        }
    }
}
