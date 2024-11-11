using SubHero.Data.Entrees;
using SubHero.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHero.DataTests.EntreeTests
{
    /// <summary>
    /// Test Class that verifies that the properties of the TurkeyCranberrySandwich class are working as intended
    /// </summary>
    public class TurkeyCranberrySandwichUnitTests
    {

        #region Default value Tests

        /// <summary>
        /// Tests that the default DefaultBread is correct for this entree
        /// </summary>
        [Fact]
        public void DefaultBreadTest()
        {
            TurkeyCranberrySandwich c = new();
            Assert.Equal(BreadType.Wheat, c.DefaultBread);
        }

        /// <summary>
        /// Tests that the default CurrentBread is correct for this entree
        /// </summary>
        [Fact]
        public void DefaultCurrentBreadTest()
        {
            TurkeyCranberrySandwich c = new();
            Assert.Equal(BreadType.Wheat, c.CurrentBread);
        }

        /// <summary>
        /// Tests that the default Size value is correct
        /// </summary>
        [Fact]
        public void DefaultSizeTest()
        {
            TurkeyCranberrySandwich c = new();
            Assert.Equal(SizeType.Medium, c.Size);
        }

        /// <summary>
        /// Tests that the default Price value is correct
        /// </summary>
        [Fact]
        public void PriceDefaultTest()
        {
            TurkeyCranberrySandwich c = new();
            Assert.Equal(8.49m, c.Price, 2);
        }

        /// <summary>
        /// Tests that the default Calories value is correct
        /// </summary>
        [Fact]
        public void CaloriesDefaultTest()
        {
            TurkeyCranberrySandwich c = new();
            Assert.Equal((uint)(250 + 80 + 120 + 150 + 5), c.Calories); 
        }

        /// <summary>
        /// Tests that the default Preparation Information value is correct
        /// </summary>
        [Fact]
        public void PreparationInformationDefaultTest()
        {
            TurkeyCranberrySandwich c = new();

            Assert.Empty(c.PreparationInformation);
        }

        /// <summary>
        /// Tests that the count of default ingredients is correct
        /// </summary>
        [Fact]
        public void IngredientCountDefaultTest()
        {
            TurkeyCranberrySandwich c = new();

            Assert.Equal(5, c.AdditionalIngredients.Count);
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
            TurkeyCranberrySandwich c = new();

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
        [InlineData(SizeType.Small, SizeType.Small)]
        [InlineData(SizeType.Medium, SizeType.Medium)]
        [InlineData(SizeType.Large, SizeType.Medium)]
        public void SizeRestrictionTest(SizeType newSize, SizeType expectedSizeResult)
        {
            TurkeyCranberrySandwich c = new();

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
            TurkeyCranberrySandwich c = new();

            //set size first (including default)
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
            TurkeyCranberrySandwich c = new();

            //set bread first (including default)
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
            TurkeyCranberrySandwich c = new();

            c.CurrentBread = breadUpdate;
            Assert.Equal(sizes, c.SizeOptions);
        }

        /// <summary>
        /// Tests that the SizeOptions array is correct after updating
        /// </summary>
        /// <param name="newSize">Size to be applied</param>
        /// <param name="breads">Expected BreadType array after updating</param>
        [Theory]
        [InlineData(SizeType.Small, new BreadType[] { BreadType.Wheat, BreadType.Sourdough, BreadType.Hoagie })]
        [InlineData(SizeType.Medium, new BreadType[] { BreadType.Wheat, BreadType.Wrap, BreadType.Sourdough, BreadType.Hoagie })]
        [InlineData(SizeType.Large, new BreadType[] { BreadType.Wheat, BreadType.Wrap, BreadType.Sourdough, BreadType.Hoagie })]
        public void BreadOptionsUpdateTest(SizeType newSize, BreadType[] breads)
        {
            TurkeyCranberrySandwich c = new();

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
        /// <param name="ingredientStatus">Array of values representing ingredients' inclusion for this entree</param>
        /// <param name="newSize">Size to be applied</param>
        /// <param name="expectedPrice">Expected Price after updating</param>
        [Theory]
        [InlineData(new bool[] { true, true, true, true, true }, SizeType.Small, "6.49")] //8.49 + 1.00 - 3.00
        [InlineData(new bool[] { true, false, true, false, true }, SizeType.Medium, "9.49")] //8.49 + 1.00
        [InlineData(new bool[] { false, true, false, true, false }, SizeType.Large, "8.49")] //8.49
        [InlineData(new bool[] { true, true, true, false, false }, SizeType.Small, "5.49")] //8.49 - 3.00
        [InlineData(new bool[] { false, false, false, true, true }, SizeType.Medium, "9.49")] //8.49 + 1.00
        [InlineData(new bool[] { true, true, false, false, false }, SizeType.Large, "8.49")] //8.49
        [InlineData(new bool[] { false, false, true, true, true }, SizeType.Small, "6.49")] //8.49 + 1.00 - 3.00
        [InlineData(new bool[] { false, false, true, true, false }, SizeType.Medium, "8.49")] //8.49
        public void PriceUpdateTest(bool[] ingredientStatus, SizeType newSize, string expectedPrice)
        {
            TurkeyCranberrySandwich c = new();
            int i = 0;
            foreach (IngredientItem item in c.AdditionalIngredients.Values)
            {
                item.Included = ingredientStatus[i];
                i++;
            }

            c.Size = newSize;

            decimal newExpectedPrice = decimal.Parse(expectedPrice);

            Assert.Equal(newExpectedPrice, c.Price, 2);
        }

        /// <summary>
        /// Tests that the Calories property is correct after updating
        /// </summary>
        /// <param name="ingredientStatus">Array of values representing ingredients' inclusion for this entree</param>
        /// <param name="newSize">Size to be applied</param>
        /// <param name="newBread">BreadType to be applied</param>
        /// <param name="expectedCalories">Expected Calories after updating</param>
        [Theory]
        [InlineData(new bool[] { true, true, true, true, true }, SizeType.Small, BreadType.Hoagie, (uint)((290 + 80 + 120 + 150 + 5 + 80) * 0.5))]
        [InlineData(new bool[] { true, false, true, false, true }, SizeType.Medium, BreadType.Wrap, (uint)((220 + 80 + 150 + 80)))]
        [InlineData(new bool[] { false, true, false, true, false }, SizeType.Large, BreadType.Sourdough, (uint)((250 + 120 + 5)))]
        [InlineData(new bool[] { true, true, true, false, false }, SizeType.Small, BreadType.Wheat, (uint)((250 + 80 + 120 + 150) * 0.5))]
        [InlineData(new bool[] { false, false, false, true, true }, SizeType.Medium, BreadType.Sourdough, (uint)((250 + 5 + 80)))]
        [InlineData(new bool[] { true, true, false, false, false }, SizeType.Large, BreadType.Wheat, (uint)((250 + 80 + 120)))]
        [InlineData(new bool[] { false, false, true, true, true }, SizeType.Small, BreadType.Wrap, (uint)((250 + 150 + 5 + 80) * 0.5))]
        [InlineData(new bool[] { false, false, true, true, false }, SizeType.Medium, BreadType.Hoagie, (uint)((290 + 150 + 5)))]
        public void CaloriesUpdateTest(bool[] ingredientStatus, SizeType newSize, BreadType newBread, uint expectedCalories)
        {
            TurkeyCranberrySandwich c = new();
            int i = 0;
            foreach (IngredientItem item in c.AdditionalIngredients.Values)
            {
                item.Included = ingredientStatus[i];
                i++;
            }

            c.Size = newSize;
            c.CurrentBread = newBread; //NOTE will have restrictions applied since size is changing first.

            Assert.Equal(expectedCalories, c.Calories);
        }

        /// <summary>
        /// Tests that the Preparation Information property is correct after updating
        /// </summary>
        /// <param name="ingredientStatus">Array of values representing ingredients' inclusion for this entree</param>
        /// <param name="newSize">Size to be applied</param>
        /// <param name="newBread">BreadType to be applied</param>
        /// <param name="expectedPrepInfo">Expected Preparation Info after updating</param>
        [Theory]
        [InlineData(new bool[] { true, true, true, true, true }, SizeType.Small, BreadType.Hoagie, new string[] { "Use Hoagie", "Small", "Add Provolone Cheese"})]
        [InlineData(new bool[] { true, false, true, false, true }, SizeType.Medium, BreadType.Wrap, new string[] { "Use Wrap", "Hold Cranberry Sauce", "Hold Red Onion", "Add Provolone Cheese" })]
        [InlineData(new bool[] { false, true, false, true, false }, SizeType.Large, BreadType.Sourdough, new string[] { "Use Sourdough Bread", "Hold Turkey", "Hold Cream Cheese" })]
        [InlineData(new bool[] { true, true, true, false, false }, SizeType.Small, BreadType.Wheat, new string[] { "Hold Red Onion", "Small"})]
        [InlineData(new bool[] { false, false, false, true, true }, SizeType.Medium, BreadType.Sourdough, new string[] { "Use Sourdough Bread", "Hold Turkey", "Hold Cranberry Sauce", "Hold Cream Cheese", "Add Provolone Cheese" })]
        [InlineData(new bool[] { true, true, false, false, false }, SizeType.Large, BreadType.Wheat, new string[] { "Hold Cream Cheese", "Hold Red Onion" })]
        [InlineData(new bool[] { false, false, true, true, true }, SizeType.Small, BreadType.Wrap, new string[] { "Use Wrap", "Hold Turkey", "Hold Cranberry Sauce", "Add Provolone Cheese" })]
        [InlineData(new bool[] { false, false, true, true, false }, SizeType.Medium, BreadType.Hoagie, new string[] { "Use Hoagie", "Hold Turkey", "Hold Cranberry Sauce" })]
        public void PreparationInformationUpdateTest(bool[] ingredientStatus, SizeType newSize, BreadType newBread, string[] expectedPrepInfo)
        {
            TurkeyCranberrySandwich c = new();
            int i = 0;
            foreach (IngredientItem item in c.AdditionalIngredients.Values)
            {
                item.Included = ingredientStatus[i];
                i++;
            }

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
            TurkeyCranberrySandwich c = new();

            Assert.IsAssignableFrom<IMenuItem>(c);
        }

        /// <summary>
        /// Tests that this item can be cast from Entree
        /// </summary>
        [Fact]
        public void EntreeCastTest()
        {
            TurkeyCranberrySandwich c = new();

            Assert.IsAssignableFrom<Entree>(c);
        }

        #endregion

        /// <summary>
        /// Tests the overriden ToString method
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            TurkeyCranberrySandwich d = new();
            Assert.Equal(d.Name, d.ToString());
        }

        /// <summary>
        /// Tests that the TurkeyCranberrySandwich instance implments the INotifyPropertyChanged interface
        /// </summary>
        [Fact]
        public void ShoudImplementINotifyPropertyChanged()
        {
            TurkeyCranberrySandwich d = new();
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
            TurkeyCranberrySandwich d = new();
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
            TurkeyCranberrySandwich d = new();
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
        [InlineData(new IngredientType[] { IngredientType.Turkey, IngredientType.CranberrySauce, IngredientType.CreamCheese, IngredientType.RedOnion, IngredientType.ProvoloneCheese }, "Price")]
        [InlineData(new IngredientType[] { IngredientType.Turkey, IngredientType.CranberrySauce, IngredientType.CreamCheese }, "Calories")]
        [InlineData(new IngredientType[] { IngredientType.RedOnion, IngredientType.ProvoloneCheese, IngredientType.Turkey }, "PreparationInformation")]
        [InlineData(new IngredientType[] { IngredientType.CranberrySauce, IngredientType.CreamCheese, IngredientType.RedOnion }, "Price")]
        [InlineData(new IngredientType[] { IngredientType.ProvoloneCheese, IngredientType.Turkey, IngredientType.CranberrySauce, IngredientType.CreamCheese }, "Calories")]
        [InlineData(new IngredientType[] { IngredientType.RedOnion, IngredientType.CranberrySauce, IngredientType.ProvoloneCheese, IngredientType.Turkey }, "PreparationInformation")]
        [InlineData(new IngredientType[] { IngredientType.ProvoloneCheese, IngredientType.RedOnion, IngredientType.CreamCheese, IngredientType.Turkey }, "Price")]
        [InlineData(new IngredientType[] { IngredientType.CreamCheese, IngredientType.CranberrySauce }, "Calories")]
        public void CheckAddtionalIngredientsChangeProperties(IngredientType[] ingredients, string affectedProperty)
        {
            TurkeyCranberrySandwich d = new();

            foreach (IngredientType item in ingredients)
            {
                Assert.PropertyChanged(d, affectedProperty, () => {
                    d.AdditionalIngredients[item].Included = true;
                });
            }

        }
    }
}
