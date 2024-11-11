using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubHero.Data.Drinks;
using SubHero.Data.Sides;

namespace SubHero.DataTests.SideTests
{
    /// <summary>
    /// Test Class that verifies that the properties of the Apple class are working as intended
    /// </summary>
    public class AppleUnitTests
    {

        #region Default Value Tests

        /// <summary>
        /// Tests that the default Sliced property value is correct
        /// </summary>
        [Fact]
        public void SlicedDefaultsToFalseTest()
        {
            Apple a = new Apple();

            Assert.False(a.Sliced);
        }

        /// <summary>
        /// Tests that the default Price is correct
        /// </summary>
        [Fact]
        public void PriceDefaultTest()
        {
            Apple a = new Apple();

            Assert.Equal(1.25m, a.Price, 2);
        }

        /// <summary>
        /// Tests that the default Calories is correct
        /// </summary>
        [Fact]
        public void CaloriesDefaultTest()
        {
            Apple a = new Apple();

            Assert.Equal((uint)100, a.Calories);
        }

        /// <summary>
        /// Tests that the default Preparation Information is correct
        /// </summary>
        [Fact]
        public void PreparationInformationDefaultTest()
        {
            Apple a = new Apple();

            Assert.Empty(a.PreparationInformation);
        }

        #endregion


        #region Property Constraint/Update Tests

        /// <summary>
        /// Tests that the Sliced property is correct after updating
        /// </summary>
        /// <param name="newSliced">Whether this apple is sliced</param>
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SlicedUpdateTest(bool newSliced)
        {
            Apple a = new Apple();

            a.Sliced = newSliced;
            Assert.Equal(newSliced, a.Sliced); //Should match new value - no restrictions
        }

        #endregion


        #region Derived Property Tests

        /// <summary>
        /// Tests that the Price property is correct after updating
        /// </summary>
        /// <param name="sliced">Whether this apple is sliced</param>
        /// <param name="expectedPrice">Expected price after updating</param>
        [Theory]
        [InlineData(true, "1.75")]
        [InlineData(false, "1.25")]
        public void PriceUpdateTest(bool sliced, string expectedPrice)
        {
            Apple a = new Apple();

            a.Sliced = sliced;
            decimal newPrice = decimal.Parse(expectedPrice);
            Assert.Equal(newPrice, a.Price, 2);
        }

        //NOTE Calories never change, so it's not included

        /// <summary>
        /// Tests that the Preparation Information property is correct after updating
        /// </summary>
        /// <param name="sliced">Whether this apple is sliced</param>
        /// <param name="expectedPrepInfo">Expected Preparation Info after updating</param>
        [Theory]
        [InlineData(true, new string[] { "Sliced" })]
        [InlineData(false, new string[] {})]
        public void PreparationInformationUpdateTest(bool sliced, string[] expectedPrepInfo)
        {
            Apple a = new Apple();
            a.Sliced = sliced;

            Assert.All(expectedPrepInfo, item => Assert.Contains(item, a.PreparationInformation));
            Assert.Equal(expectedPrepInfo.Length, a.PreparationInformation.Count());

        }

        #endregion


        #region Cast Tests

        /// <summary>
        /// Tests that this item can be cast from IMenuItem
        /// </summary>
        [Fact]
        public void IMenuItemCast()
        {
            Apple a = new Apple();

            Assert.IsAssignableFrom<IMenuItem>(a);
        }

        /// <summary>
        /// Tests that this item can be cast from Side
        /// </summary>
        [Fact]
        public void SideCastTest()
        {
            Apple a = new Apple();

            Assert.IsAssignableFrom<Side>(a);
        }

        #endregion


        /// <summary>
        /// Tests the overriden ToString method
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            Apple d = new();
            Assert.Equal(d.Name, d.ToString());
        }

        /// <summary>
        /// Tests that the Apple instance implments the INotifyPropertyChanged interface
        /// </summary>
        [Fact]
        public void ShoudImplementINotifyPropertyChanged()
        {
            Apple d = new();
            Assert.IsAssignableFrom<INotifyPropertyChanged>(d);
        }

        /// <summary>
        /// Tests that PropertyChanged is triggered appropriately when updating the Sliced Property
        /// </summary>
        /// <param name="sliced">Bool value to update the Sliced property with</param>
        /// <param name="affectedProperty">Property that should update</param>
        [Theory]
        [InlineData(true, "Sliced")]
        [InlineData(true, "Price")]
        [InlineData(true, "PreparationInformation")]
        [InlineData(false, "Sliced")]
        [InlineData(false, "Price")]
        [InlineData(false, "PreparationInformation")]
        public void CheckSlicedChangeProperties(bool sliced, string affectedProperty)
        {
            Apple d = new();
            Assert.PropertyChanged(d, affectedProperty, () => {
                d.Sliced = sliced;
            });
        }
    }
}
