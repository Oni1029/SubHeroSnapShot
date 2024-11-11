using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubHero.Data.Enums;
using SubHero.Data.Sides;

namespace SubHero.DataTests.SideTests
{
    /// <summary>
    /// Test Class that verifies that the properties of the Chips class are working as intended
    /// </summary>
    public class ChipsUnitTests
    {

        #region Default Value Tests

        /// <summary>
        /// Tests that the default chip Flavor is correct
        /// </summary>
        [Fact]
        public void FlavorDefaultsToLaysTest()
        {
            Chips c = new Chips();

            Assert.Equal(ChipType.Lays, c.Flavor);
        }

        /// <summary>
        /// Tests that the default Price is correct
        /// </summary>
        [Fact]
        public void PriceDefaultTest()
        {
            Chips c = new Chips();

            Assert.Equal(1.95m, c.Price, 2);
        }

        /// <summary>
        /// Tests that the default Calories is correct
        /// </summary>
        [Fact]
        public void CaloriesDefaultTest()
        {
            Chips c = new Chips();

            Assert.Equal((uint)240, c.Calories);
        }

        /// <summary>
        /// Tests that the default Preparation Information is correct
        /// </summary>
        [Fact]
        public void PreparationInformationDefaultTest()
        {
            Chips c = new Chips();

            Assert.Contains("Lays", c.PreparationInformation);
            Assert.Single(c.PreparationInformation); //Checks count is 1 for default
        }

        #endregion


        #region Property Constraint/Update Tests

        /// <summary>
        /// Tests that the Flavor Property is correct after updating
        /// </summary>
        /// <param name="newFlavor">Chip flavor to be applied</param>
        [Theory]
        [InlineData(ChipType.Lays)]
        [InlineData(ChipType.Doritos)]
        [InlineData(ChipType.SunChips)]
        [InlineData(ChipType.Cheetos)]
        [InlineData(ChipType.Pretzels)]
        public void FlavorUpdateTest(ChipType newFlavor)
        {
            Chips c = new Chips();

            c.Flavor = newFlavor;
            Assert.Equal(newFlavor, c.Flavor); //Should match new value - no restrictions
        }

        #endregion


        #region Derived Property Tests

        //Price never changes, so it's not included

        /// <summary>
        /// Tests that the Calories property is correct after updating
        /// </summary>
        /// <param name="newFlavor">Chip flavor to be applied</param>
        /// <param name="expectedCals">Expected calories after updating</param>
        [Theory]
        [InlineData(ChipType.Lays, (uint)240)]
        [InlineData(ChipType.Doritos, (uint)250)]
        [InlineData(ChipType.Cheetos, (uint)260)]
        [InlineData(ChipType.SunChips, (uint)210)]
        [InlineData(ChipType.Pretzels, (uint)180)]
        public void CaloriesUpdateTest(ChipType newFlavor, uint expectedCals)
        {
            Chips c = new Chips();
            c.Flavor = newFlavor;

            Assert.Equal(expectedCals, c.Calories);
        }

        /// <summary>
        /// Tests that the Preparation Information property is correct after updating
        /// </summary>
        /// <param name="newFlavor">Chip flavor to be applied</param>
        /// <param name="expectedPrepInfo">Expected Preparation Info after updating</param>
        [Theory]
        [InlineData(ChipType.Lays, new string[] { "Lays" })]
        [InlineData(ChipType.Doritos, new string[] { "Doritos" })]
        [InlineData(ChipType.Cheetos, new string[] { "Cheetos" })]
        [InlineData(ChipType.SunChips, new string[] { "Sun Chips" })]
        [InlineData(ChipType.Pretzels, new string[] { "Pretzels" })]
        public void PreparationInformationUpdateTest(ChipType newFlavor, string[] expectedPrepInfo)
        {
            Chips c = new Chips();
            c.Flavor = newFlavor;

            Assert.All(expectedPrepInfo, item => Assert.Contains(item, c.PreparationInformation));
            Assert.Equal(expectedPrepInfo.Length, c.PreparationInformation.Count());

        }

        #endregion


        #region Cast Tests

        /// <summary>
        /// Tests that this item can be cast from IMenuItem
        /// </summary>
        [Fact]
        public void IMenuItemCast()
        {
            Chips c = new Chips();

            Assert.IsAssignableFrom<IMenuItem>(c);
        }

        /// <summary>
        /// Tests that this item can be cast from Side
        /// </summary>
        [Fact]
        public void SideCastTest()
        {
            Chips c = new Chips();

            Assert.IsAssignableFrom<Side>(c);
        }

        #endregion


        /// <summary>
        /// Tests the overriden ToString method
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            Chips d = new();
            Assert.Equal(d.Name, d.ToString());
        }

        /// <summary>
        /// Tests that the Chips instance implments the INotifyPropertyChanged interface
        /// </summary>
        [Fact]
        public void ShoudImplementINotifyPropertyChanged()
        {
            Chips d = new();
            Assert.IsAssignableFrom<INotifyPropertyChanged>(d);
        }

        /// <summary>
        /// Tests that PropertyChanged is triggered appropriately when updating the Flavor Property
        /// </summary>
        /// <param name="flavor">Chip flavor to update the Flavor property with</param>
        /// <param name="affectedProperty">Property that should update</param>
        [Theory]
        [InlineData(ChipType.Doritos, "Flavor")]
        [InlineData(ChipType.Doritos, "Calories")]
        [InlineData(ChipType.Doritos, "PreparationInformation")]
        [InlineData(ChipType.SunChips, "Flavor")]
        [InlineData(ChipType.SunChips, "Calories")]
        [InlineData(ChipType.SunChips, "PreparationInformation")]
        [InlineData(ChipType.Lays, "Flavor")]
        [InlineData(ChipType.Lays, "Calories")]
        [InlineData(ChipType.Lays, "PreparationInformation")]
        [InlineData(ChipType.Cheetos, "Flavor")]
        [InlineData(ChipType.Cheetos, "Calories")]
        [InlineData(ChipType.Cheetos, "PreparationInformation")]
        [InlineData(ChipType.Pretzels, "Flavor")]
        [InlineData(ChipType.Pretzels, "Calories")]
        [InlineData(ChipType.Pretzels, "PreparationInformation")]
        public void CheckFlavorChangeProperties(ChipType flavor, string affectedProperty)
        {
            Chips d = new();
            Assert.PropertyChanged(d, affectedProperty, () => {
                d.Flavor = flavor;
            });
        }
    }
}
