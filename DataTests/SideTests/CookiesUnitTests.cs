using SubHero.Data.Enums;
using SubHero.Data.Sides;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHero.DataTests.SideTests
{
    /// <summary>
    /// Test Class that verifies that the properties of the Cookies class are working as intended
    /// </summary>
    public class CookiesUnitTests
    {

        #region Default Value Tests

        /// <summary>
        /// Tests that the default cookieType is correct
        /// </summary>
        [Fact]
        public void FlavorDefaultsToChocolateChipTest()
        {
            Cookies c = new Cookies();

            Assert.Equal(CookieType.ChocolateChip, c.Flavor);
        }

        /// <summary>
        /// Tests that the default cookieCount is correct
        /// </summary>
        [Fact]
        public void CookieCountDefaultTest()
        {
            Cookies c = new Cookies();

            Assert.Equal((uint)2, c.CookieCount);
        }

        /// <summary>
        /// Tests that the default price for cookies is correct
        /// </summary>
        [Fact]
        public void PriceDefaultTest()
        {
            Cookies c = new Cookies();

            Assert.Equal(3.00m, c.Price, 2);
        }

        /// <summary>
        /// Tests that the default calories for cookies is correct
        /// </summary>
        [Fact]
        public void CaloriesDefaultTest()
        {
            Cookies c = new Cookies();

            Assert.Equal((uint)(2*180), c.Calories);
        }

        /// <summary>
        /// Tests that the default Preparation Info for cookies is correct
        /// </summary>
        [Fact]
        public void PreparationInformationDefaultTest()
        {
            Cookies c = new Cookies();

            Assert.Contains("2 Chocolate Chip Cookies", c.PreparationInformation);
            Assert.Single(c.PreparationInformation); //Checks that string count is 1 for default
        }

        #endregion


        #region Property Constraint/Update Tests

        /// <summary>
        /// Tests that the Flavor property correctly updates
        /// </summary>
        /// <param name="newFlavor">CookieType to update Flavor property</param>
        [Theory]
        [InlineData(CookieType.ChocolateChip)]
        [InlineData(CookieType.PeanutButter)]
        [InlineData(CookieType.OatmealRaisin)]
        [InlineData(CookieType.Sugar)]
        public void FlavorUpdateTest(CookieType newFlavor)
        {
            Cookies c = new Cookies();

            c.Flavor = newFlavor;
            Assert.Equal(newFlavor, c.Flavor); //Should match new value - no restrictions
        }

        /// <summary>
        /// Tests that the CookieCount property correctly updates
        /// </summary>
        /// <param name="newCount">Count to be applied</param>
        /// <param name="expectedCount">Expected CookieCount after updating</param>
        [Theory]
        [InlineData(0, 2)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(4, 4)]
        [InlineData(6, 6)]
        [InlineData(7, 2)]
        [InlineData(8, 2)]
        [InlineData(15, 2)]
        public void CookieCountUpdateTest(int newCount, int expectedCount)
        {
            Cookies c = new Cookies();
            
            c.CookieCount = (uint)newCount;

            Assert.Equal((uint)expectedCount, c.CookieCount); //NOTE if out of range, count should stay as default (2)

        }

        #endregion


        #region Derived Property Tests

        /// <summary>
        /// Tests that the Price property correctly updates
        /// </summary>
        /// <param name="newCount">Count to be applied</param>
        /// <param name="expectedPrice">Expected Price after updating</param>
        [Theory]
        [InlineData((uint)2, ("3.00"))]
        [InlineData((uint)3, ("4.75"))]
        [InlineData((uint)4, ("6.00"))]
        [InlineData((uint)5, ("7.75"))]
        [InlineData((uint)6, ("9.00"))]
        public void PriceUpdateTest(uint newCount, string expectedPrice)
        {
            Cookies c = new Cookies();
            c.CookieCount = newCount;

            decimal newPrice = Decimal.Parse(expectedPrice);

            Assert.Equal(newPrice, c.Price, 2);
        }

        /// <summary>
        /// Tests that the Calories property correctly updates
        /// </summary>
        /// <param name="newCount">Count to be applied</param>
        /// <param name="newFlavor">Flavor of cookie to be applied</param>
        /// <param name="expectedCals">Expected calories after updating</param>
        [Theory]
        [InlineData((uint)2, CookieType.ChocolateChip, (uint)(2*180))]
        [InlineData((uint)3, CookieType.OatmealRaisin, (uint)(3*150))]
        [InlineData((uint)4, CookieType.Sugar, (uint)(4*160))]
        [InlineData((uint)5, CookieType.ChocolateChip, (uint)(5*180))]
        [InlineData((uint)6, CookieType.PeanutButter, (uint)(6*190))]
        public void CaloriesUpdateTest(uint newCount, CookieType newFlavor, uint expectedCals)
        {
            Cookies c = new Cookies();
            c.Flavor = newFlavor;
            c.CookieCount = newCount;

            Assert.Equal(expectedCals, c.Calories);
        }

        /// <summary>
        /// Tests that the Preparation Information property is correctly updating
        /// </summary>
        /// <param name="newCount">Count to be applied</param>
        /// <param name="newFlavor">Flavor of cookie to be applied</param>
        /// <param name="expectedPrepInfo">Expected Preparation Info after updating</param>
        [Theory]
        [InlineData((uint)2, CookieType.ChocolateChip, new string[] { "2 Chocolate Chip Cookies" })]
        [InlineData((uint)3, CookieType.OatmealRaisin, new string[] { "3 Oatmeal Raisin Cookies" })]
        [InlineData((uint)4, CookieType.Sugar, new string[] { "4 Sugar Cookies" })]
        [InlineData((uint)5, CookieType.PeanutButter, new string[] { "5 Peanut Butter Cookies" })]
        [InlineData((uint)6, CookieType.ChocolateChip, new string[] { "6 Chocolate Chip Cookies" })]
        public void PreparationInformationUpdateTest(uint newCount, CookieType newFlavor, string[] expectedPrepInfo)
        {
            Cookies c = new Cookies();
            c.Flavor = newFlavor;
            c.CookieCount = newCount;


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
            Cookies c = new Cookies();

            Assert.IsAssignableFrom<IMenuItem>(c);
        }

        /// <summary>
        /// Tests that this item can be cast from Side
        /// </summary>
        [Fact]
        public void SideCastTest()
        {
            Cookies c = new Cookies();

            Assert.IsAssignableFrom<Side>(c);
        }

        #endregion

        /// <summary>
        /// Tests the overriden ToString method
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            Cookies d = new();
            Assert.Equal(d.Name, d.ToString());
        }


        /// <summary>
        /// Tests that the Apple instance implments the INotifyPropertyChanged interface
        /// </summary>
        [Fact]
        public void ShoudImplementINotifyPropertyChanged()
        {
            Cookies d = new();
            Assert.IsAssignableFrom<INotifyPropertyChanged>(d);
        }

        /// <summary>
        /// Tests that PropertyChanged is triggered appropriately when updating the Flavor Property
        /// </summary>
        /// <param name="flavor">Cookie type to update the Flavor property with</param>
        /// <param name="affectedProperty">Property that should update</param>
        [Theory]
        [InlineData(CookieType.PeanutButter, "Flavor")]
        [InlineData(CookieType.PeanutButter, "Calories")]
        [InlineData(CookieType.PeanutButter, "PreparationInformation")]
        [InlineData(CookieType.OatmealRaisin, "Flavor")]
        [InlineData(CookieType.OatmealRaisin, "Calories")]
        [InlineData(CookieType.OatmealRaisin, "PreparationInformation")]
        [InlineData(CookieType.Sugar, "Flavor")]
        [InlineData(CookieType.Sugar, "Calories")]
        [InlineData(CookieType.Sugar, "PreparationInformation")]
        [InlineData(CookieType.ChocolateChip, "Flavor")]
        [InlineData(CookieType.ChocolateChip, "Calories")]
        [InlineData(CookieType.ChocolateChip, "PreparationInformation")]
        public void CheckFlavorChangeProperties(CookieType flavor, string affectedProperty)
        {
            Cookies d = new();
            Assert.PropertyChanged(d, affectedProperty, () => {
                d.Flavor = flavor;
            });
        }

        /// <summary>
        /// Tests that PropertyChanged is triggered appropriately when updating the Flavor Property
        /// </summary>
        /// <param name="count">Amount of cookies to update the Count property with</param>
        /// <param name="affectedProperty">Property that should update</param>
        [Theory]
        [InlineData(2, "CookieCount")]
        [InlineData(2, "Calories")]
        [InlineData(2, "Price")]
        [InlineData(2, "PreparationInformation")]
        [InlineData(3, "CookieCount")]
        [InlineData(3, "Calories")]
        [InlineData(3, "Price")]
        [InlineData(3, "PreparationInformation")]
        [InlineData(4, "CookieCount")]
        [InlineData(4, "Calories")]
        [InlineData(4, "Price")]
        [InlineData(4, "PreparationInformation")]
        [InlineData(5, "CookieCount")]
        [InlineData(5, "Calories")]
        [InlineData(5, "Price")]
        [InlineData(5, "PreparationInformation")]
        [InlineData(6, "CookieCount")]
        [InlineData(6, "Calories")]
        [InlineData(6, "Price")]
        [InlineData(6, "PreparationInformation")]
        public void CheckCountChangeProperties(uint count, string affectedProperty)
        {
            Cookies d = new();
            Assert.PropertyChanged(d, affectedProperty, () => {
                d.CookieCount = count;
            });
        }
    }
}
