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
    /// Test Class that verifies that the properties of the Lemonade class are working as intended
    /// </summary>
    public class LemonadeUnitTests
    {


        #region Property default value Tests

        /// <summary>
        /// Tests that the default "Strawberry" value is correct
        /// </summary>
        [Fact]
        public void StrawberryDefaultsToFalse()
        {
            Lemonade d = new();
            Assert.False(d.Strawberry);
        }

        /// <summary>
        /// Tests that the default Size value is correct
        /// </summary>
        [Fact]
        public void SizeDefaultsToMediumTest()
        {
            Lemonade d = new();
            Assert.Equal(SizeType.Medium, d.Size);
        }

        /// <summary>
        /// Tests that the default Ice value is correct
        /// </summary>
        [Fact]
        public void IceDefaultsToTrueTest()
        {
            Lemonade d = new();
            Assert.True(d.Ice);
        }

        /// <summary>
        /// Tests that the default Price value is correct
        /// </summary>
        [Fact]
        public void PriceDefaultTest()
        {
            Lemonade d = new();
            Assert.Equal(3.00m, d.Price, 2);
        }

        /// <summary>
        /// Tests that the default Calories value is correct
        /// </summary>
        [Fact]
        public void CaloriesDefaultTest()
        {
            Lemonade d = new();
            Assert.Equal((uint)160, d.Calories);
        }

        /// <summary>
        /// Tests that the default Preparation Information value is correct
        /// </summary>
        [Fact]
        public void PreparationInformationDefaultTest()
        {
            Lemonade d = new();
            List<string> temp = new List<string>() { "Medium" };

            Assert.All(temp, item => Assert.Contains(item, d.PreparationInformation));
            Assert.Equal(temp.Count(), d.PreparationInformation.Count());
        }


        #endregion

        #region Property Constraint/Update Tests

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
            Lemonade d = new();
            d.Size = size;

            Assert.Equal(size, d.Size);
        }

        /// <summary>
        /// Tests that the Ice property is correct after updating
        /// </summary>
        [Fact]
        public void IceUpdateTest()
        {
            Lemonade d = new();
            d.Ice = false;

            Assert.False(d.Ice);
        }

        /// <summary>
        /// Tests that the Strawberry property is correct after updating
        /// </summary>
        [Fact]
        public void StrawberryUpdateTest()
        {
            Lemonade d = new();
            d.Strawberry = true;

            Assert.True(d.Strawberry);
        }


        #endregion

        #region Derived property Tests

        /// <summary>
        /// Tests that the Price property is correct after updating
        /// </summary>
        /// <param name="ice">Whether this drink contains ice</param>
        /// <param name="strawberry">Whether this drink contains strawberry</param>
        /// <param name="size">Size to be applied</param>
        /// <param name="expectedPrice">Expected Price after updating</param>
        [Theory]
        [InlineData(true, true, SizeType.Small, "3.25")]
        [InlineData(true, false, SizeType.Medium, "3.00")]
        [InlineData(true, true, SizeType.Large, "4.00")]
        [InlineData(false, false, SizeType.Small, "2.75")]
        [InlineData(false, true, SizeType.Medium, "3.50")]
        [InlineData(false, false, SizeType.Large, "3.50")]
        public void PriceUpdateTest(bool ice, bool strawberry, SizeType size, string expectedPrice)
        {
            Lemonade d = new();
            d.Ice = ice;
            d.Size = size;
            d.Strawberry = strawberry;

            decimal expectedPriceConverted = Decimal.Parse(expectedPrice);

            Assert.Equal(expectedPriceConverted, d.Price, 2);

        }

        /// <summary>
        /// Tests that the Calories property is correct after updating
        /// </summary>
        /// <param name="ice">Whether this drink contains ice</param>
        /// <param name="strawberry">Whether this drink contains strawberry</param>
        /// <param name="size">Size to be applied</param>
        /// <param name="expectedCalories">Expected Calories after updating</param>
        [Theory]
        [InlineData(true, true, SizeType.Small, (uint)(200 * 0.80))]
        [InlineData(true, false, SizeType.Medium, (uint)(160))]
        [InlineData(true, true, SizeType.Large, (uint)(200 * 1.60))]
        [InlineData(false, false, SizeType.Small, (uint)(160 * 0.80))]
        [InlineData(false, true, SizeType.Medium, (uint)(200))]
        [InlineData(false, false, SizeType.Large, (uint)(160 * 1.60))]
        [InlineData(true, false, SizeType.Small, (uint)(160 * 0.80))]
        [InlineData(false, false, SizeType.Medium, (uint)(160))]
        public void CaloriesUpdateTest(bool ice, bool strawberry, SizeType size, uint expectedCalories)
        {
            Lemonade d = new();
            d.Ice = ice;
            d.Size = size;
            d.Strawberry = strawberry;

            Assert.Equal(expectedCalories, d.Calories);

        }

        /// <summary>
        /// Tests that the Preparation Information property is correct after updating
        /// </summary>
        /// <param name="ice">Whether this drink contains ice</param>
        /// <param name="strawberry">Whether this drink contains strawberry</param>
        /// <param name="size">Size to be applied</param>
        /// <param name="expectedPrepInfo">Expected Preparation Info after updating</param>
        [Theory]
        [InlineData(true, true, SizeType.Small, new string[] { "Small", "Add Strawberry" })]
        [InlineData(true, false, SizeType.Medium, new string[] { "Medium" })]
        [InlineData(true, true, SizeType.Large, new string[] { "Large", "Add Strawberry" })]
        [InlineData(false, false, SizeType.Small, new string[] { "Small", "Hold Ice" })]
        [InlineData(false, true, SizeType.Medium, new string[] { "Medium", "Hold Ice", "Add Strawberry" })]
        [InlineData(false, false, SizeType.Large, new string[] { "Large", "Hold Ice" })]
        [InlineData(true, false, SizeType.Small, new string[] { "Small" })]
        [InlineData(false, false, SizeType.Medium, new string[] { "Medium", "Hold Ice"})]
        public void PreparationInformationUpdateTest(bool ice, bool strawberry, SizeType size, string[] expectedPrepInfo)
        {
            Lemonade d = new();
            d.Ice = ice;
            d.Size = size;
            d.Strawberry = strawberry;

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
            Lemonade d = new();

            Assert.IsAssignableFrom<IMenuItem>(d);
        }

        /// <summary>
        /// Tests that this item can be cast from Drink
        /// </summary>
        [Fact]
        public void DrinkCastTest()
        {
            Lemonade d = new();

            Assert.IsAssignableFrom<Drink>(d);
        }

        #endregion


        /// <summary>
        /// Tests the overriden ToString method
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            Lemonade d = new();
            Assert.Equal(d.Name, d.ToString());
        }

        /// <summary>
        /// Tests that the Lemonade instance implments the INotifyPropertyChanged interface
        /// </summary>
        [Fact]
        public void ShoudImplementINotifyPropertyChanged()
        {
            Lemonade d = new();
            Assert.IsAssignableFrom<INotifyPropertyChanged>(d);
        }

        /// <summary>
        /// Tests that PropertyChanged is triggered appropriately when updating the Strawberry Property
        /// </summary>
        /// <param name="strawberry">Bool value to update the Strawberry property with</param>
        /// <param name="affectedProperty">Property that should update</param>
        [Theory]
        [InlineData(true, "PreparationInformation")]
        [InlineData(true, "Calories")]
        [InlineData(true, "Price")]
        [InlineData(true, "Strawberry")]
        [InlineData(false, "PreparationInformation")]
        [InlineData(false, "Calories")]
        [InlineData(false, "Price")]
        [InlineData(false, "Strawberry")]
        public void CheckStrawberryChangeProperties(bool strawberry, string affectedProperty)
        {
            Lemonade d = new();
            Assert.PropertyChanged(d, affectedProperty, () => {
                d.Strawberry = strawberry;
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
            Lemonade d = new();
            Assert.PropertyChanged(d, affectedProperty, () => {
                d.Ice = ice;
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
            Lemonade d = new();
            Assert.PropertyChanged(d, affectedProperty, () => {
                d.Size = size;
            });
        }
    }
}
