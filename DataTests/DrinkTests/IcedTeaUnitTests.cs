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
    /// Test Class that verifies that the properties of the IcedTea class are working as intended
    /// </summary>
    public class IcedTeaUnitTests
    {

        #region Property default value Tests

        /// <summary>
        /// Tests that the default "Sweet" value is correct
        /// </summary>
        [Fact]
        public void SweetDefaultsToTrueTest()
        {
            IcedTea d = new();
            Assert.True(d.Sweet);
        }

        /// <summary>
        /// Tests that the default Size value is correct
        /// </summary>
        [Fact]
        public void SizeDefaultTest()
        {
            IcedTea d = new();
            Assert.Equal(SizeType.Medium, d.Size);
        }

        /// <summary>
        /// Tests that the default Ice value is correct
        /// </summary>
        [Fact]
        public void IceDefaultTest()
        {
            IcedTea d = new();
            Assert.True(d.Ice);
        }

        /// <summary>
        /// Tests that the default Price value is correct
        /// </summary>
        [Fact]
        public void PriceDefaultTest()
        {
            IcedTea d = new();
            Assert.Equal(2.50m, d.Price, 2);
        }

        /// <summary>
        /// Tests that the default Calories value is correct
        /// </summary>
        [Fact]
        public void CaloriesDefaultTest()
        {
            IcedTea d = new();
            Assert.Equal((uint)180, d.Calories);
        }

        /// <summary>
        /// Tests that the default Preparation Information value is correct
        /// </summary>
        [Fact]
        public void PreparationInformationDefaultTest()
        {
            IcedTea d = new();
            List<string> temp = new List<string>() { "Medium"};

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
            IcedTea d = new();
            d.Size = size;

            Assert.Equal(size, d.Size);
        }

        /// <summary>
        /// Tests that the Ice property is correct after updating
        /// </summary>
        [Fact]
        public void IceUpdateTest()
        {
            IcedTea d = new();
            d.Ice = false;

            Assert.False(d.Ice);
        }
        
        /// <summary>
        /// Tests that the Sweet property is correct after updating
        /// </summary>
        [Fact]
        public void SweetUpdateTest()
        {
            IcedTea d = new();
            d.Sweet = false;

            Assert.False(d.Sweet);
        }


        #endregion

        #region Derived property Tests

        /// <summary>
        /// Tests that the Price property is correct after updating
        /// </summary>
        /// <param name="ice">Whether this drink contains ice</param>
        /// <param name="sweet">Whether this Tea is sweet</param>
        /// <param name="size">Size to be applied</param>
        /// <param name="expectedPrice">Expected Price after updating</param>
        [Theory]
        [InlineData(true, true, SizeType.Small, "2.25")]
        [InlineData(true, false, SizeType.Medium, "2.50")]
        [InlineData(true, true, SizeType.Large, "3.00")]
        [InlineData(false, false, SizeType.Small, "2.25")]
        [InlineData(false, true, SizeType.Medium, "2.50")]
        [InlineData(false, false, SizeType.Large, "3.00")]
        public void PriceUpdateTest(bool ice, bool sweet, SizeType size, string expectedPrice)
        {
            IcedTea d = new();
            d.Ice = ice;
            d.Size = size;
            d.Sweet = sweet;

            decimal expectedPriceConverted = Decimal.Parse(expectedPrice);

            Assert.Equal(expectedPriceConverted, d.Price, 2);

        }

        /// <summary>
        /// Tests that the Calories property is correct after updating
        /// </summary>
        /// <param name="ice">Whether this drink contains ice</param>
        /// <param name="sweet">Whether this Tea is sweet</param>
        /// <param name="size">Size to be applied</param>
        /// <param name="expectedCalories">Exepected Calories after updating</param>
        [Theory]
        [InlineData(true, true, SizeType.Small, (uint)(180 * 0.80))]
        [InlineData(true, false, SizeType.Medium, (uint)(5))]
        [InlineData(true, true, SizeType.Large, (uint)(180 * 1.60))]
        [InlineData(false, false, SizeType.Small, (uint)(5 * 0.80))]
        [InlineData(false, true, SizeType.Medium, (uint)(180))]
        [InlineData(false, false, SizeType.Large, (uint)(5 * 1.60))]
        [InlineData(true, false, SizeType.Small, (uint)(5 * 0.80))]
        [InlineData(false, false, SizeType.Medium, (uint)(5))]
        public void CaloriesUpdateTest(bool ice, bool sweet, SizeType size, uint expectedCalories)
        {
            IcedTea d = new();
            d.Ice = ice;
            d.Size = size;
            d.Sweet = sweet;

            Assert.Equal(expectedCalories, d.Calories);

        }

        /// <summary>
        /// Tests that the Calories property is correct after updating
        /// </summary>
        /// <param name="ice">Whether this drink contains ice</param>
        /// <param name="sweet">Whether this Tea is sweet</param>
        /// <param name="size">Size to be applied</param>
        /// <param name="expectedPrepInfo">Expected Preparation Info after updating</param>
        [Theory]
        [InlineData(true, true, SizeType.Small, new string[] { "Small" })]
        [InlineData(true, false, SizeType.Medium, new string[] { "Medium", "Unsweetened" })]
        [InlineData(true, true, SizeType.Large, new string[] { "Large" })]
        [InlineData(false, false, SizeType.Small, new string[] { "Small", "Hold Ice", "Unsweetened" })]
        [InlineData(false, true, SizeType.Medium, new string[] { "Medium", "Hold Ice" })]
        [InlineData(false, false, SizeType.Large, new string[] { "Large", "Hold Ice", "Unsweetened" })]
        [InlineData(true, false, SizeType.Small, new string[] { "Small", "Unsweetened" })]
        [InlineData(false, false, SizeType.Medium, new string[] { "Medium", "Hold Ice", "Unsweetened" })]
        public void PreparationInformationUpdateTest(bool ice, bool sweet, SizeType size, string[] expectedPrepInfo)
        {
            IcedTea d = new();
            d.Ice = ice;
            d.Size = size;
            d.Sweet = sweet;

            Assert.All(expectedPrepInfo, item => Assert.Contains(item, d.PreparationInformation));
            Assert.Equal(expectedPrepInfo.Count(), d.PreparationInformation.Count());

        }


        #endregion

        #region Cast Tests

        /// <summary>
        /// Tests that this item can be cast from IMenuItem
        /// </summary>
        [Fact]
        public void IMenuItemCastTest()
        {
            IcedTea d = new();

            Assert.IsAssignableFrom<IMenuItem>(d);
        }

        /// <summary>
        /// Tests that this item can be cast from Drink
        /// </summary>
        [Fact]
        public void DrinkCastTest()
        {
            IcedTea d = new();

            Assert.IsAssignableFrom<Drink>(d);
        }

        #endregion


        /// <summary>
        /// Tests the overriden ToString method
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            IcedTea d = new();
            Assert.Equal(d.Name, d.ToString());
        }


        /// <summary>
        /// Tests that the IcedTea instance implments the INotifyPropertyChanged interface
        /// </summary>
        [Fact]
        public void ShoudImplementINotifyPropertyChanged()
        {
            IcedTea d = new();
            Assert.IsAssignableFrom<INotifyPropertyChanged>(d);
        }

        /// <summary>
        /// Tests that PropertyChanged is triggered appropriately when updating the Sweet Property
        /// </summary>
        /// <param name="sweet">Bool value to update the Sweet property with</param>
        /// <param name="affectedProperty">Property that should update</param>
        [Theory]
        [InlineData(true, "PreparationInformation")]
        [InlineData(true, "Calories")]
        [InlineData(true, "Sweet")]
        [InlineData(false, "PreparationInformation")]
        [InlineData(false, "Calories")]
        [InlineData(false, "Sweet")]
        public void CheckSweetChangeProperties(bool sweet, string affectedProperty)
        {
            IcedTea d = new();
            Assert.PropertyChanged(d, affectedProperty, () => {
                d.Sweet = sweet;
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
            IcedTea d = new();
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
            IcedTea d = new();
            Assert.PropertyChanged(d, affectedProperty, () => {
                d.Size = size;
            });
        }







    }
}
