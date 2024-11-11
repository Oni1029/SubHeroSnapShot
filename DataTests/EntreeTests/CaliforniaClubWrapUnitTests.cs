using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubHero.Data.Drinks;
using SubHero.Data.Entrees;
using SubHero.Data.Enums;
using SubHero.Data.Sides;

namespace SubHero.DataTests.EntreeTests
{
    /// <summary>
    /// Test Class that verifies that the properties of the CaliforniaClubWrap class are working as intended
    /// </summary>
    public class CaliforniaClubWrapUnitTests
    {


        #region Default value Tests

        /// <summary>
        /// Tests that the default DefaultBread is correct for this entree
        /// </summary>
        [Fact]
        public void DefaultBreadTest()
        {
            CaliforniaClubWrap c = new();
            Assert.Equal(BreadType.Wrap, c.DefaultBread);
        }

        /// <summary>
        /// Tests that the default CurrentBread is correct for this entree
        /// </summary>
        [Fact]
        public void DefaultCurrentBreadTest()
        {
            CaliforniaClubWrap c = new();
            Assert.Equal(BreadType.Wrap, c.CurrentBread);
        }

        /// <summary>
        /// Tests that the default Size value is correct
        /// </summary>
        [Fact]
        public void DefaultSizeTest()
        {
            CaliforniaClubWrap c = new();
            Assert.Equal(SizeType.Medium, c.Size);
        }

        /// <summary>
        /// Tests that the default Price value is correct
        /// </summary>
        [Fact]
        public void PriceDefaultTest()
        {
            CaliforniaClubWrap c = new();
            Assert.Equal(9.49m, c.Price, 2);
        }

        /// <summary>
        /// Tests that the default Calories value is correct
        /// </summary>
        [Fact]
        public void CaloriesDefaultTest()
        {
            CaliforniaClubWrap c = new();
            Assert.Equal((uint)695, c.Calories);
        }

        /// <summary>
        /// Tests that the default Preparation Information value is correct
        /// </summary>
        [Fact]
        public void PreparationInformationDefaultTest()
        {
            CaliforniaClubWrap c = new();

            Assert.Empty(c.PreparationInformation);

        }

        /// <summary>
        /// Tests that the count of default ingredients is correct
        /// </summary>
        [Fact]
        public void IngredientCountDefaultTest()
        {
            CaliforniaClubWrap c = new();

            Assert.Equal(7, c.AdditionalIngredients.Count);
        }


        #endregion


        #region Constraint Tests


        /// <summary>
        /// Tests that the CurrentBread Property updates correctly
        /// </summary>
        /// <param name="newBread">BreadType to be applied</param>
        /// <param name="expectedBreadResult">Expected BreadType after updating</param>
        [Theory]
        [InlineData(BreadType.Wheat, BreadType.Wheat)]
        [InlineData(BreadType.Hoagie, BreadType.Hoagie)]
        [InlineData(BreadType.Wrap, BreadType.Wrap)]
        [InlineData(BreadType.Sourdough, BreadType.Sourdough)]
        public void CurrentBreadRestrictionTest(BreadType newBread, BreadType expectedBreadResult)
        {
            CaliforniaClubWrap c = new CaliforniaClubWrap();

            //set bread (including default)
            c.CurrentBread = newBread;
            //compare new bread to current bread
            Assert.Equal(expectedBreadResult, c.CurrentBread);

        }

        /// <summary>
        /// Tests that the Size Property updates correctly
        /// </summary>
        /// <param name="newSize">Size to be applied</param>
        /// <param name="expectedSizeResult">Expected Size after updating</param>
        [Theory]
        [InlineData(SizeType.Small, SizeType.Medium)]
        [InlineData(SizeType.Medium, SizeType.Medium)]
        [InlineData(SizeType.Large, SizeType.Medium)]
        public void SizeRestrictionTest(SizeType newSize, SizeType expectedSizeResult)
        {
            CaliforniaClubWrap c = new CaliforniaClubWrap();

            //set bread (including default)
            c.Size = newSize;
            //compare new bread to current bread
            Assert.Equal(expectedSizeResult, c.Size);

        }

        #endregion


        #region Derived Property Tests



        /// <summary>
        /// Tests that the CurrentBread property is correct after updating
        /// </summary>
        /// <param name="newSize">Size to be applied</param>
        /// <param name="newBread">BreadType to be applied</param>
        /// <param name="expectedBreadResult">Expected BreadType after updating</param>
        [Theory]
        [InlineData(SizeType.Small, BreadType.Wheat, BreadType.Wheat)]
        [InlineData(SizeType.Medium, BreadType.Hoagie, BreadType.Hoagie)]
        [InlineData(SizeType.Large, BreadType.Wrap, BreadType.Wrap)]
        [InlineData(SizeType.Small, BreadType.Sourdough, BreadType.Sourdough)]
        [InlineData(SizeType.Medium, BreadType.Wheat, BreadType.Wheat)]
        [InlineData(SizeType.Large, BreadType.Hoagie, BreadType.Hoagie)]
        [InlineData(SizeType.Medium, BreadType.Sourdough, BreadType.Sourdough)]
        public void CurrentBreadUpdateTest(SizeType newSize, BreadType newBread, BreadType expectedBreadResult)
        {
            CaliforniaClubWrap c = new CaliforniaClubWrap();

            //set size (including default)
            c.Size = newSize;
            //set bread (including default)
            c.CurrentBread = newBread;
            //compare new bread to current bread
            Assert.Equal(expectedBreadResult, c.CurrentBread);

        }

        /// <summary>
        /// Tests that the Size property is correct after updating
        /// </summary>
        /// <param name="newSize">Size to be applied</param>
        /// <param name="newBread">BreadType to be applied</param>
        /// <param name="expectedSizeResult">Expected Size after updating</param>
        [Theory]
        [InlineData(SizeType.Small, BreadType.Wheat, SizeType.Small)]
        [InlineData(SizeType.Medium, BreadType.Hoagie, SizeType.Medium)]
        [InlineData(SizeType.Large, BreadType.Wrap, SizeType.Medium)]
        [InlineData(SizeType.Small, BreadType.Sourdough, SizeType.Small)]
        [InlineData(SizeType.Medium, BreadType.Wheat, SizeType.Medium)]
        [InlineData(SizeType.Large, BreadType.Hoagie, SizeType.Large)]
        [InlineData(SizeType.Medium, BreadType.Sourdough, SizeType.Medium)]
        public void SizeUpdateTest(SizeType newSize, BreadType newBread, SizeType expectedSizeResult)
        {
            CaliforniaClubWrap c = new CaliforniaClubWrap();

            //set bread (including default)
            c.CurrentBread = newBread;
            //set size (including default)
            c.Size = newSize;
            //compare new bread to current bread
            Assert.Equal(expectedSizeResult, c.Size);

        }

        /// <summary>
        /// Tests that the SizeOptions array is correct after updating
        /// </summary>
        /// <param name="breadUpdate">BreadType to be applied</param>
        /// <param name="sizes">Expected Size array after updating</param>
        [Theory]
        [InlineData(BreadType.Wrap, new SizeType[] { SizeType.Medium })]
        [InlineData(BreadType.Hoagie, new SizeType[] { SizeType.Small, SizeType.Medium, SizeType.Large })]
        [InlineData(BreadType.Wheat, new SizeType[] { SizeType.Small, SizeType.Medium })]
        [InlineData(BreadType.Sourdough, new SizeType[] { SizeType.Small, SizeType.Medium })]
        public void SizeOptionsUpdateTest(BreadType breadUpdate, SizeType[] sizes)
        {
            CaliforniaClubWrap c = new CaliforniaClubWrap();

            c.CurrentBread = breadUpdate;
            Assert.Equal(sizes, c.SizeOptions);
        }

        /// <summary>
        /// Tests that the SizeOptions array is correct after updating
        /// </summary>
        /// <param name="newSize">Size to be applied</param>
        /// <param name="breads">Expected BreadType array after updating</param>
        [Theory]
        [InlineData(SizeType.Small, new BreadType[] { BreadType.Wheat, BreadType.Wrap, BreadType.Sourdough, BreadType.Hoagie })]
        [InlineData(SizeType.Medium, new BreadType[] { BreadType.Wheat, BreadType.Wrap, BreadType.Sourdough, BreadType.Hoagie })]
        [InlineData(SizeType.Large, new BreadType[] { BreadType.Wheat, BreadType.Wrap, BreadType.Sourdough, BreadType.Hoagie })]
        public void BreadOptionsUpdateTest(SizeType newSize, BreadType[] breads)
        {
            CaliforniaClubWrap c = new CaliforniaClubWrap();

            c.Size = newSize;
            foreach (BreadType item in breads)
            {
                Assert.Contains(item, c.BreadOptions);
            }
            Assert.Equal(breads.Length, c.BreadOptions.Length);
        }

        /// <summary>
        /// Tests that the Price property is correct after updating
        /// </summary>
        /// <param name="turkey">Whether this entree includes Turkey</param>
        /// <param name="bacon">Whether this entree includes Bacon</param>
        /// <param name="avocado">Whether this entree includes Avocado</param>
        /// <param name="swissCheese">Whether this entree includes Swiss Cheese</param>
        /// <param name="tomato">Whether this entree includes Tomato</param>
        /// <param name="sprouts">Whether this entree includes Sprouts</param>
        /// <param name="mayo">Whether this entree includes Mayo</param>
        /// <param name="newSize">Size to be applied</param>
        /// <param name="expectedPrice">Expected Price after updating</param>
        [Theory]
        [InlineData(true, true, true, true, true, true, true, SizeType.Small, "9.49")]
        [InlineData(true, false, true, false, true, false, true, SizeType.Medium, "9.49")]
        [InlineData(false, true, false, true, false, true, false, SizeType.Large, "9.49")]
        [InlineData(true, true, true, false, false, false, false, SizeType.Small, "9.49")]
        [InlineData(false, false, false, true, true, true, true, SizeType.Medium, "9.49")]
        [InlineData(true, true, false, false, false, true, true, SizeType.Large, "9.49")]
        [InlineData(false, false, true, true, true, false, false, SizeType.Small, "9.49")]
        [InlineData(false, false, true, true, false, false, true, SizeType.Medium, "9.49")]
        public void PriceUpdateTest(bool turkey, bool bacon, bool avocado, bool swissCheese, bool tomato, bool sprouts, bool mayo, SizeType newSize, string expectedPrice)
        {
            CaliforniaClubWrap c = new CaliforniaClubWrap();
            c.AdditionalIngredients[IngredientType.Turkey].Included = turkey;
            c.AdditionalIngredients[IngredientType.Bacon].Included = bacon;
            c.AdditionalIngredients[IngredientType.Avocado].Included = avocado;
            c.AdditionalIngredients[IngredientType.SwissCheese].Included = swissCheese;
            c.AdditionalIngredients[IngredientType.Tomato].Included = tomato;
            c.AdditionalIngredients[IngredientType.Sprouts].Included = sprouts;
            c.AdditionalIngredients[IngredientType.Mayo].Included = mayo;

            c.Size = newSize;

            decimal newExpectedPrice = decimal.Parse(expectedPrice);

            Assert.Equal(newExpectedPrice, c.Price, 2);
        }

        /// <summary>
        /// Tests that the Calories property is correct after updating
        /// </summary>
        /// <param name="turkey">Whether this entree includes Turkey</param>
        /// <param name="bacon">Whether this entree includes Bacon</param>
        /// <param name="avocado">Whether this entree includes Avocado</param>
        /// <param name="swissCheese">Whether this entree includes Swiss Cheese</param>
        /// <param name="tomato">Whether this entree includes Tomato</param>
        /// <param name="sprouts">Whether this entree includes Sprouts</param>
        /// <param name="mayo">Whether this entree includes Mayo</param>
        /// <param name="newSize">Size to be applied</param>
        /// <param name="newBread">BreadType to be applied</param>
        /// <param name="expectedCalories">Expected Calories after updating</param>
        [Theory]
        [InlineData(true, true, true, true, true, true, true, SizeType.Small, BreadType.Wrap, (uint)(220 + 80 + 100 + 100 + 80 + 10 + 5 + 100))]
        [InlineData(true, false, true, false, true, false, true, SizeType.Medium, BreadType.Sourdough, (uint)(250 + 80 + 100 + 10 + 100))]
        [InlineData(false, true, false, true, false, true, false, SizeType.Large, BreadType.Hoagie, (uint)((290 + 100 + 80 + 5) * 1.50))]
        [InlineData(true, true, true, false, false, false, false, SizeType.Small, BreadType.Wheat, (uint)((250 + 80 + 100 + 100) * 0.50))]
        [InlineData(false, false, false, true, true, true, true, SizeType.Medium, BreadType.Wrap, (uint)(220 + 80 + 10 + 5 + 100))]
        [InlineData(true, true, false, false, false, true, true, SizeType.Large, BreadType.Sourdough, (uint)(250 + 80 + 100 + 5 + 100))]
        [InlineData(false, false, true, true, true, false, false, SizeType.Small, BreadType.Hoagie, (uint)((290 + 100 + 80 + 10) * 0.50))]
        [InlineData(false, false, true, true, false, false, true, SizeType.Medium, BreadType.Wheat, (uint)(250 + 100 + 80 + 100))]
        public void CaloriesUpdateTest(bool turkey, bool bacon, bool avocado, bool swissCheese, bool tomato, bool sprouts, bool mayo, SizeType newSize, BreadType newBread, uint expectedCalories)
        {
            CaliforniaClubWrap c = new CaliforniaClubWrap();
            c.AdditionalIngredients[IngredientType.Turkey].Included = turkey;
            c.AdditionalIngredients[IngredientType.Bacon].Included = bacon;
            c.AdditionalIngredients[IngredientType.Avocado].Included = avocado;
            c.AdditionalIngredients[IngredientType.SwissCheese].Included = swissCheese;
            c.AdditionalIngredients[IngredientType.Tomato].Included = tomato;
            c.AdditionalIngredients[IngredientType.Sprouts].Included = sprouts;
            c.AdditionalIngredients[IngredientType.Mayo].Included = mayo;

            c.CurrentBread = newBread;
            c.Size = newSize;

            Assert.Equal(expectedCalories, c.Calories);
        }

        /// <summary>
        /// Tests that the Preparation Information property is correct after updating
        /// </summary>
        /// <param name="turkey">Whether this entree includes Turkey</param>
        /// <param name="bacon">Whether this entree includes Bacon</param>
        /// <param name="avocado">Whether this entree includes Avocado</param>
        /// <param name="swissCheese">Whether this entree includes Swiss Cheese</param>
        /// <param name="tomato">Whether this entree includes Tomato</param>
        /// <param name="sprouts">Whether this entree includes Sprouts</param>
        /// <param name="mayo">Whether this entree includes Mayo</param>
        /// <param name="newSize">Size to be applied</param>
        /// <param name="newBread">BreadType to be applied</param>
        /// <param name="expectedPrepInfo">Expected Preparation Info after updating</param>
        [Theory]
        [InlineData(true, true, true, true, true, true, true, SizeType.Small, BreadType.Wrap, new string[] { })]
        [InlineData(true, false, true, false, true, false, true, SizeType.Medium, BreadType.Sourdough, new string[] { "Use Sourdough Bread", "Hold Bacon", "Hold Swiss Cheese", "Hold Sprouts" })]
        [InlineData(false, true, false, true, false, true, false, SizeType.Large, BreadType.Hoagie, new string[] { "Use Hoagie", "Large", "Hold Turkey", "Hold Avocado", "Hold Tomato", "Hold Mayo" })]
        [InlineData(true, true, true, false, false, false, false, SizeType.Small, BreadType.Wheat, new string[] { "Use Wheat Bread", "Small", "Hold Swiss Cheese", "Hold Tomato", "Hold Sprouts", "Hold Mayo" })]
        [InlineData(false, false, false, true, true, true, true, SizeType.Medium, BreadType.Wrap, new string[] { "Hold Turkey", "Hold Bacon", "Hold Avocado" })]
        [InlineData(true, true, false, false, false, true, true, SizeType.Large, BreadType.Sourdough, new string[] { "Use Sourdough Bread", "Hold Avocado", "Hold Swiss Cheese", "Hold Tomato" })]
        [InlineData(false, false, true, true, true, false, false, SizeType.Small, BreadType.Hoagie, new string[] { "Use Hoagie", "Small", "Hold Turkey", "Hold Bacon", "Hold Sprouts", "Hold Mayo" })]
        [InlineData(false, false, true, true, false, false, true, SizeType.Medium, BreadType.Wheat, new string[] { "Use Wheat Bread", "Hold Turkey", "Hold Bacon", "Hold Tomato", "Hold Sprouts" })]
        public void PreparationInformationUpdateTest(bool turkey, bool bacon, bool avocado, bool swissCheese, bool tomato, bool sprouts, bool mayo, SizeType newSize, BreadType newBread, string[] expectedPrepInfo)
        {
            CaliforniaClubWrap c = new CaliforniaClubWrap();
            c.AdditionalIngredients[IngredientType.Turkey].Included = turkey;
            c.AdditionalIngredients[IngredientType.Bacon].Included = bacon;
            c.AdditionalIngredients[IngredientType.Avocado].Included = avocado;
            c.AdditionalIngredients[IngredientType.SwissCheese].Included = swissCheese;
            c.AdditionalIngredients[IngredientType.Tomato].Included = tomato;
            c.AdditionalIngredients[IngredientType.Sprouts].Included = sprouts;
            c.AdditionalIngredients[IngredientType.Mayo].Included = mayo;

            c.CurrentBread = newBread;
            c.Size = newSize;

            foreach (string s in expectedPrepInfo)
            {
                Assert.Contains(s, c.PreparationInformation);
            }

            Assert.Equal(expectedPrepInfo.Length, c.PreparationInformation.Count());
        }

        #endregion


        #region Cast Tests

        /// <summary>
        /// Tests that this item can be cast from IMenuItem
        /// </summary>
        [Fact]
        public void IMenuItemCastTest()
        {
            CaliforniaClubWrap c = new CaliforniaClubWrap();

            Assert.IsAssignableFrom<IMenuItem>(c);
        }

        /// <summary>
        /// Tests that this item can be cast from Entree
        /// </summary>
        [Fact]
        public void EntreeCastTest()
        {
            CaliforniaClubWrap c = new CaliforniaClubWrap();

            Assert.IsAssignableFrom<Entree>(c);
        }

        #endregion

        /// <summary>
        /// Tests the overriden ToString method
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            CaliforniaClubWrap d = new();
            Assert.Equal(d.Name, d.ToString());
        }

        /// <summary>
        /// Tests that the CaliforniaClubWrap instance implments the INotifyPropertyChanged interface
        /// </summary>
        [Fact]
        public void ShoudImplementINotifyPropertyChanged()
        {
            CaliforniaClubWrap d = new();
            Assert.IsAssignableFrom<INotifyPropertyChanged>(d);
        }

        /// <summary>
        /// Tests that PropertyChanged is triggered appropriately when updating the CurrentBread Property
        /// </summary>
        /// <param name="bread">bread type to update the CurrentBread property with</param>
        /// <param name="propertyName">Property that should update</param>
        [Theory]
        [InlineData(BreadType.Sourdough, "CurrentBread")]
        [InlineData(BreadType.Wrap, "SizeOptions")]
        [InlineData(BreadType.Hoagie, "Calories")]
        [InlineData(BreadType.Wheat, "PreparationInformation")]
        [InlineData(BreadType.Sourdough, "PreparationInformation")]
        [InlineData(BreadType.Wrap, "Calories")]
        [InlineData(BreadType.Hoagie, "SizeOptions")]
        [InlineData(BreadType.Wheat, "CurrentBread")]
        public void CheckCurrentBreadChangeProperties(BreadType bread, string propertyName)
        {
            CaliforniaClubWrap d = new();
            Assert.PropertyChanged(d, propertyName, () =>
            {
                d.CurrentBread = bread;
            });
        }

        /// <summary>
        /// Tests that PropertyChanged is triggered appropriately when updating the Size Property
        /// </summary>
        /// <param name="size">Size type to update the Size property with</param>
        /// <param name="bread">Bread type to update the CurrentBread property with</param>
        /// <param name="propertyName">Property that should update</param>
        [Theory]
        [InlineData(SizeType.Small, BreadType.Sourdough, "Size")]
        [InlineData(SizeType.Medium, BreadType.Wrap, "SizeOptions")]
        [InlineData(SizeType.Large, BreadType.Hoagie, "Calories")]
        [InlineData(SizeType.Medium, BreadType.Sourdough, "PreparationInformation")]
        [InlineData(SizeType.Small, BreadType.Wheat, "Price")]
        [InlineData(SizeType.Small, BreadType.Sourdough, "PreparationInformation")]
        [InlineData(SizeType.Medium, BreadType.Wrap, "Calories")]
        [InlineData(SizeType.Large, BreadType.Hoagie, "SizeOptions")]
        [InlineData(SizeType.Medium, BreadType.Wheat, "Size")]
        [InlineData(SizeType.Small, BreadType.Hoagie, "Price")]
        public void CheckSizeChangeProperties(SizeType size, BreadType bread, string propertyName)
        {
            CaliforniaClubWrap d = new();
            Assert.PropertyChanged(d, propertyName, () =>
            {
                d.CurrentBread = bread; //updating bread so that other size options are available for testing
                d.Size = size;
            });
        }


        /// <summary>
        /// Tests that PropertyChanged is triggered appropriately when updating the AdditionalIngredients Property
        /// </summary>
        /// <param name="ingredients">Array of ingredients to update appropriate "Included" values in the AddtionalIngredients Property</param>
        /// <param name="affectedProperty">Property that should update</param>
        [Theory]
        [InlineData(new IngredientType[] { IngredientType.Turkey, IngredientType.Bacon, IngredientType.Avocado, IngredientType.SwissCheese, IngredientType.Tomato, IngredientType.Sprouts, IngredientType.Mayo }, "Price")]
        [InlineData(new IngredientType[] { IngredientType.Tomato, IngredientType.Bacon, IngredientType.SwissCheese, IngredientType.Sprouts, IngredientType.Tomato }, "Calories")]
        [InlineData(new IngredientType[] { IngredientType.Bacon, IngredientType.SwissCheese, IngredientType.Sprouts, IngredientType.Avocado }, "PreparationInformation")]
        [InlineData(new IngredientType[] { IngredientType.Turkey, IngredientType.Sprouts, IngredientType.Avocado }, "Price")]
        [InlineData(new IngredientType[] { IngredientType.Turkey, IngredientType.Tomato, IngredientType.SwissCheese, IngredientType.Avocado }, "Calories")]
        [InlineData(new IngredientType[] { IngredientType.Tomato, IngredientType.Bacon, IngredientType.Sprouts, IngredientType.Avocado }, "PreparationInformation")]
        [InlineData(new IngredientType[] { IngredientType.Turkey, IngredientType.Tomato, IngredientType.Bacon, IngredientType.SwissCheese }, "Price")]
        [InlineData(new IngredientType[] { IngredientType.Tomato, IngredientType.Bacon, IngredientType.SwissCheese }, "Calories")]
        public void CheckAddtionalIngredientsChangeProperties(IngredientType[] ingredients, string affectedProperty)
        {
            CaliforniaClubWrap d = new();

            foreach (IngredientType item in ingredients)
            {
                Assert.PropertyChanged(d, affectedProperty, () => {
                    d.AdditionalIngredients[item].Included = true;
                });
            }

        }




    }
}
