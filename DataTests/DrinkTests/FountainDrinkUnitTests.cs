using SubHero.Data.Drinks;
using SubHero.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHero.DataTests.DrinkTests
{
    /// <summary>
    /// Test Class that verifies that the properties of the FountainDrink class are working as intended
    /// </summary>
    public class FountainDrinkUnitTests
    {

        #region Property default value Tests

        /// <summary>
        /// Tests that the default Flavor value is correct
        /// </summary>
        [Fact]
        public void FlavorDefaultTest()
        {
            FountainDrink d = new();
            Assert.Equal(SodaType.Coke, d.Flavor);
        }

        /// <summary>
        /// Tests that the default Size value is correct
        /// </summary>
        [Fact]
        public void SizeDefaultTest()
        {
            FountainDrink d = new();
            Assert.Equal(SizeType.Medium, d.Size);
        }

        /// <summary>
        /// Tests that the default Ice value is correct
        /// </summary>
        [Fact]
        public void IceDefaultTest()
        {
            FountainDrink d = new();
            Assert.True(d.Ice);
        }

        /// <summary>
        /// Tests that the default Price value is correct
        /// </summary>
        [Fact]
        public void PriceDefaultTest()
        {
            FountainDrink d = new();
            Assert.Equal(2.00m, d.Price, 2);
        }

        /// <summary>
        /// Tests that the default Calories value is correct
        /// </summary>
        [Fact]
        public void CaloriesDefaultTest()
        {
            FountainDrink d = new();
            Assert.Equal((uint)240, d.Calories);
        }

        /// <summary>
        /// Tests that the default Preparation Information value is correct
        /// </summary>
        [Fact]
        public void PreparationInformationDefaultTest()
        {
            FountainDrink d = new();
            List<string> temp = new List<string>() { "Medium", "Coke" };

            Assert.All(temp, item => Assert.Contains(item, d.PreparationInformation));
            Assert.Equal(temp.Count(), d.PreparationInformation.Count());
        }


        #endregion

        #region Property Constraint/Update Tests

        /// <summary>
        /// Tests that the Flavor property is correct after updating
        /// </summary>
        /// <param name="flavor">Soda flavor to be applied</param>
        [Theory]
        [InlineData(SodaType.Coke)]
        [InlineData(SodaType.CokeZero)]
        [InlineData(SodaType.DrPepper)]
        [InlineData(SodaType.OrangeFanta)]
        [InlineData(SodaType.MountainDew)]
        public void FlavorUpdateTest(SodaType flavor)
        {
            FountainDrink d = new();
            d.Flavor = flavor;

            Assert.Equal(flavor, d.Flavor);
        }

        /// <summary>
        /// Tests that the Size property is correct after updating
        /// </summary>
        /// <param name="size">Size to be applied</param>
        [Theory]
        [InlineData(SizeType.Small)]
        [InlineData(SizeType.Medium)]
        [InlineData(SizeType.Large)]
        public void SizeUpdateTest(SizeType size)
        {
            FountainDrink d = new();
            d.Size = size;

            Assert.Equal(size, d.Size);
        }

        /// <summary>
        /// Tests that the Ice property is correct after updating
        /// </summary>
        [Fact]
        public void IceUpdateTest()
        {
            FountainDrink d = new();
            d.Ice = false;

            Assert.False(d.Ice);
        }


        #endregion

        #region Derived property Tests

        /// <summary>
        /// Tests that the Price property is correct after updating
        /// </summary>
        /// <param name="ice">Wether this drink contains ice</param>
        /// <param name="size">Size to be applied</param>
        /// <param name="flavor">Soda flavor to be applied</param>
        /// <param name="expectedPrice">Expected price after updating</param>
        [Theory]
        [InlineData(true, SizeType.Small, SodaType.Coke, "1.75")]
        [InlineData(false, SizeType.Medium, SodaType.CokeZero, "2.00")]
        [InlineData(true, SizeType.Large, SodaType.DrPepper, "2.50")]
        [InlineData(false, SizeType.Small, SodaType.OrangeFanta, "1.75")]
        [InlineData(true, SizeType.Medium, SodaType.MountainDew, "2.00")]
        [InlineData(false, SizeType.Large, SodaType.Coke, "2.50")]
        [InlineData(true, SizeType.Small, SodaType.CokeZero, "1.75")]
        [InlineData(false, SizeType.Medium, SodaType.DrPepper, "2.00")]
        public void PriceTest(bool ice, SizeType size, SodaType flavor, string expectedPrice)
        {
            FountainDrink d = new();
            d.Ice = ice;
            d.Size = size;
            d.Flavor = flavor;

            decimal expectedPriceConverted = Decimal.Parse(expectedPrice);

            Assert.Equal(expectedPriceConverted, d.Price, 2);

        }

        /// <summary>
        /// Tests that the Calories property is correct after updating
        /// </summary>
        /// <param name="ice">Wether this drink contains ice</param>
        /// <param name="size">Size to be applied</param>
        /// <param name="flavor">Soda flavor to be applied</param>
        /// <param name="expectedCalories">Expected Calories after updating</param>
        [Theory]
        [InlineData(true, SizeType.Small, SodaType.Coke, (uint)(240 * 0.80))]
        [InlineData(false, SizeType.Medium, SodaType.CokeZero, (uint)(0))]
        [InlineData(true, SizeType.Large, SodaType.DrPepper, (uint)(260 * 1.60))]
        [InlineData(false, SizeType.Small, SodaType.OrangeFanta, (uint)(260 * 0.80))]
        [InlineData(true, SizeType.Medium, SodaType.MountainDew, (uint)(280))]
        [InlineData(false, SizeType.Large, SodaType.Coke, (uint)(240 * 1.60))]
        [InlineData(true, SizeType.Small, SodaType.CokeZero, (uint)(0 * 0.80))]
        [InlineData(false, SizeType.Medium, SodaType.DrPepper, (uint)(260))]
        public void CaloriesTest(bool ice, SizeType size, SodaType flavor, uint expectedCalories)
        {
            FountainDrink d = new();
            d.Ice = ice;
            d.Size = size;
            d.Flavor = flavor;

            Assert.Equal(expectedCalories, d.Calories);

        }

        /// <summary>
        /// Tests that the Calories property is correct after updating
        /// </summary>
        /// <param name="ice">Wether this drink contains ice</param>
        /// <param name="size">Size to be applied</param>
        /// <param name="flavor">Soda flavor to be applied</param>
        /// <param name="expectedPrepInfo">Expected Preparation Info after updating</param>
        [Theory]
        [InlineData(true, SizeType.Small, SodaType.Coke, new string[] { "Small", "Coke"})]
        [InlineData(false, SizeType.Medium, SodaType.CokeZero, new string[] { "Medium", "Coke Zero", "Hold Ice"})]
        [InlineData(true, SizeType.Large, SodaType.DrPepper, new string[] { "Large", "Dr. Pepper"})]
        [InlineData(false, SizeType.Small, SodaType.OrangeFanta, new string[] { "Small", "Orange Fanta", "Hold Ice"})]
        [InlineData(true, SizeType.Medium, SodaType.MountainDew, new string[] { "Medium", "Mountain Dew"})]
        [InlineData(false, SizeType.Large, SodaType.Coke, new string[] { "Large", "Coke", "Hold Ice"})]
        [InlineData(true, SizeType.Small, SodaType.CokeZero, new string[] { "Small", "Coke Zero"})]
        [InlineData(false, SizeType.Medium, SodaType.DrPepper, new string[] { "Medium", "Dr. Pepper", "Hold Ice"})]
        public void PreparationInformationTest(bool ice, SizeType size, SodaType flavor, string[] expectedPrepInfo)
        {
            FountainDrink d = new();
            d.Ice = ice;
            d.Size = size;
            d.Flavor = flavor;

            Assert.All(expectedPrepInfo, item => Assert.Contains(item, d.PreparationInformation));
            Assert.Equal(expectedPrepInfo.Length, d.PreparationInformation.Count());

        }


        #endregion

        #region Cast Tests

        /// <summary>
        /// Tests that this item can be cast from IMenuItem
        /// </summary>
        [Fact]
        public void IMenuItemCastTest()
        {
            FountainDrink d = new();

            Assert.IsAssignableFrom<IMenuItem>(d);
        }

        /// <summary>
        /// Tests that this item can be cast from Drink
        /// </summary>
        [Fact]
        public void DrinkCastTest()
        {
            FountainDrink d = new();

            Assert.IsAssignableFrom<Drink>(d);
        }

        #endregion

        /// <summary>
        /// Tests the overriden ToString method
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            FountainDrink d = new();
            Assert.Equal(d.Name, d.ToString());
        }

        /// <summary>
        /// Tests that the FountainDrink instance implments the INotifyPropertyChanged interface
        /// </summary>
        [Fact]
        public void ShoudImplementINotifyPropertyChanged()
        {
            FountainDrink d = new();
            Assert.IsAssignableFrom<INotifyPropertyChanged>(d);
        }

        /// <summary>
        /// Tests that PropertyChanged is triggered appropriately when updating the Flavor Property
        /// </summary>
        /// <param name="flavor">Soda flavor to update the Flavor property with</param>
        /// <param name="affectedProperty">Property that should update</param>
        [Theory]
        [InlineData(SodaType.Coke, "PreparationInformation")]
        [InlineData(SodaType.CokeZero, "Calories")]
        [InlineData(SodaType.OrangeFanta, "Flavor")]
        [InlineData(SodaType.CokeZero, "PreparationInformation")]
        [InlineData(SodaType.MountainDew, "Calories")]
        [InlineData(SodaType.DrPepper, "Flavor")]
        [InlineData(SodaType.OrangeFanta, "Calories")]
        [InlineData(SodaType.MountainDew, "Flavor")]
        public void CheckFlavorChangeProperties(SodaType flavor, string affectedProperty)
        {
            FountainDrink drink = new();
            Assert.PropertyChanged(drink, affectedProperty, () => {
                drink.Flavor = flavor;
            });
        }

        /// <summary>
        /// Tests that PropertyChanged is triggered appropriately when updating the Ice Property
        /// </summary>
        /// <param name="ice">Bool value to update the Ice property with</param>
        /// <param name="affectedProperty">Property that should update</param>
        [Theory]
        [InlineData(true, "PreparationInformation")]
        [InlineData(true, "Ice")]
        [InlineData(false, "PreparationInformation")]
        [InlineData(false, "Ice")]
        public void CheckIceChangeProperties(bool ice, string affectedProperty)
        {
            FountainDrink drink = new();
            Assert.PropertyChanged(drink, affectedProperty, () => {
                drink.Ice = ice;
            });
        }

        /// <summary>
        /// Tests that PropertyChanged is triggered appropriately when updating the Size Property
        /// </summary>
        /// <param name="size">Size type to update the Size property with</param>
        /// <param name="affectedProperty">Property that should update</param>
        [Theory]
        [InlineData(SizeType.Small, "Price")]
        [InlineData(SizeType.Small, "Calories")]
        [InlineData(SizeType.Small, "PreparationInformation")]
        [InlineData(SizeType.Small, "Size")]
        [InlineData(SizeType.Medium, "Price")]
        [InlineData(SizeType.Medium, "Calories")]
        [InlineData(SizeType.Medium, "PreparationInformation")]
        [InlineData(SizeType.Medium, "Size")]
        [InlineData(SizeType.Large, "Price")]
        [InlineData(SizeType.Large, "Calories")]
        [InlineData(SizeType.Large, "PreparationInformation")]
        [InlineData(SizeType.Large, "Size")]
        public void CheckSizeChangeProperties(SizeType size, string affectedProperty)
        {
            FountainDrink drink = new();
            Assert.PropertyChanged(drink, affectedProperty, () => {
                drink.Size = size;
            });
        }






    }
}