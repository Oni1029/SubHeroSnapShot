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
    /// Test Class that verifies that the properties of the CustomSandwich class are working as intended
    /// </summary>
    public class CustomSandwichUnitTests
    {

        #region Default value Tests

        /// <summary>
        /// Tests that the default DefaultBread is correct for this entree
        /// </summary>
        [Fact]
        public void DefaultBreadTest()
        {
            CustomSandwich c = new();
            Assert.Equal(BreadType.Wheat, c.DefaultBread);
        }

        /// <summary>
        /// Tests that the default CurrentBread is correct for this entree
        /// </summary>
        [Fact]
        public void DefaultCurrentBreadTest()
        {
            CustomSandwich c = new();
            Assert.Equal(BreadType.Wheat, c.CurrentBread);
        }

        /// <summary>
        /// Tests that the default Size value is correct
        /// </summary>
        [Fact]
        public void DefaultSizeTest()
        {
            CustomSandwich c = new();
            Assert.Equal(SizeType.Medium, c.Size);
        }

        /// <summary>
        /// Tests that the default Price value is correct
        /// </summary>
        [Fact]
        public void PriceDefaultTest()
        {
            CustomSandwich c = new();
            Assert.Equal(5.99m, c.Price, 2);
        }

        /// <summary>
        /// Tests that the default Calories value is correct
        /// </summary>
        [Fact]
        public void CaloriesDefaultTest()
        {
            CustomSandwich c = new();
            Assert.Equal((uint)(250), c.Calories);
        }

        /// <summary>
        /// Tests that the default Preparation Information value is correct
        /// </summary>
        [Fact]
        public void PreparationInformationDefaultTest()
        {
            CustomSandwich c = new();

            Assert.Empty(c.PreparationInformation);
        }

        /// <summary>
        /// Tests that the count of default ingredients is correct
        /// </summary>
        [Fact]
        public void IngredientCountDefaultTest()
        {
            CustomSandwich c = new();

            Assert.Equal(24, c.AdditionalIngredients.Count);
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
            CustomSandwich c = new();

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
            CustomSandwich c = new();

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
            CustomSandwich c = new();

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
            CustomSandwich c = new();

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
            CustomSandwich c = new();

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
            CustomSandwich c = new();

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
        [InlineData(new IngredientType[] { IngredientType.Turkey, IngredientType.CheddarCheese, IngredientType.CranberrySauce, }, SizeType.Small, "5.99")] //5.99 + 1.00 +1.00 + 1.00 - 3.00
        [InlineData(new IngredientType[] { IngredientType.Ham, IngredientType.FetaCheese, }, SizeType.Medium, "7.99")] //5.99 + 1.00 + 1.00
        [InlineData(new IngredientType[] { IngredientType.RedOnion, IngredientType.KalamataOlives, IngredientType.Turkey, }, SizeType.Large, "7.49")] //5.99 + 0.50 + 1.00
        [InlineData(new IngredientType[] { IngredientType.Salami, IngredientType.Tomato, }, SizeType.Small, "3.99")] //5.99 + 1.00 - 3.00
        [InlineData(new IngredientType[] { IngredientType.Sprouts }, SizeType.Medium, "5.99")] //5.99
        [InlineData(new IngredientType[] { IngredientType.Pepperoni, IngredientType.RoastedRedPeppers, }, SizeType.Large, "7.49")] //5.99 + 1.00 + 0.50
        [InlineData(new IngredientType[] { IngredientType.ItalianDressing }, SizeType.Small, "2.99")] //5.99 - 3.00
        [InlineData(new IngredientType[] { IngredientType.RanchDressing, IngredientType.Lettuce, }, SizeType.Medium, "5.99")] //5.99
        public void PriceUpdateTest(IngredientType[] ingredientStatus, SizeType newSize, string expectedPrice)
        {
            CustomSandwich c = new();

            foreach (IngredientType item in ingredientStatus)
            {
                c.AdditionalIngredients[item].Included = true;
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
        [InlineData(new IngredientType[] { }, SizeType.Small, BreadType.Hoagie, (uint)((290) * 0.5))]
        [InlineData(new IngredientType[] { IngredientType.Ham, IngredientType.FetaCheese, }, SizeType.Medium, BreadType.Wrap, (uint)((220 + 80 + 70)))]
        [InlineData(new IngredientType[] { IngredientType.RedOnion, IngredientType.KalamataOlives, IngredientType.Turkey, }, SizeType.Large, BreadType.Sourdough, (uint)((250 + 5 + 15 + 80)))]
        [InlineData(new IngredientType[] { IngredientType.Salami, IngredientType.Tomato, }, SizeType.Small, BreadType.Wheat, (uint)((250 + 120 + 10) * 0.5))]
        [InlineData(new IngredientType[] { IngredientType.Sprouts }, SizeType.Medium, BreadType.Sourdough, (uint)((250 + 5)))]
        [InlineData(new IngredientType[] { IngredientType.Pepperoni, IngredientType.RoastedRedPeppers, }, SizeType.Large, BreadType.Wheat, (uint)((250 + 90 + 15)))]
        [InlineData(new IngredientType[] { IngredientType.ItalianDressing }, SizeType.Small, BreadType.Wrap, (uint)((250 + 70) * 0.5))]
        [InlineData(new IngredientType[] { IngredientType.RanchDressing, IngredientType.Lettuce, }, SizeType.Medium, BreadType.Hoagie, (uint)((290 + 200 + 5)))]
        public void CaloriesUpdateTest(IngredientType[] ingredientStatus, SizeType newSize, BreadType newBread, uint expectedCalories)
        {
            CustomSandwich c = new();

            foreach (IngredientType item in ingredientStatus)
            {
                c.AdditionalIngredients[item].Included = true;
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
        [InlineData(new IngredientType[] { }, SizeType.Small, BreadType.Hoagie, new string[] { "Use Hoagie", "Small" })]
        [InlineData(new IngredientType[] { IngredientType.Ham, IngredientType.FetaCheese, }, SizeType.Medium, BreadType.Wrap, new string[] { "Use Wrap", "Add Ham", "Add Feta Cheese" })]
        [InlineData(new IngredientType[] { IngredientType.RedOnion, IngredientType.KalamataOlives, IngredientType.Turkey, }, SizeType.Large, BreadType.Sourdough, new string[] { "Use Sourdough Bread", "Add Red Onion", "Add Kalamata Olives", "Add Turkey" })]
        [InlineData(new IngredientType[] { IngredientType.Salami, IngredientType.Tomato, }, SizeType.Small, BreadType.Wheat, new string[] { "Add Salami", "Small", "Add Tomato" })]
        [InlineData(new IngredientType[] { IngredientType.Sprouts }, SizeType.Medium, BreadType.Sourdough, new string[] { "Use Sourdough Bread", "Add Sprouts" })]
        [InlineData(new IngredientType[] { IngredientType.Pepperoni, IngredientType.RoastedRedPeppers, }, SizeType.Large, BreadType.Wheat, new string[] { "Add Pepperoni", "Add Roasted Red Peppers" })]
        [InlineData(new IngredientType[] { IngredientType.ItalianDressing }, SizeType.Small, BreadType.Wrap, new string[] { "Use Wrap", "Add Italian Dressing" })]
        [InlineData(new IngredientType[] { IngredientType.RanchDressing, IngredientType.Lettuce, }, SizeType.Medium, BreadType.Hoagie, new string[] { "Use Hoagie", "Add Ranch Dressing", "Add Lettuce" })]
        public void PreparationInformationUpdateTest(IngredientType[] ingredientStatus, SizeType newSize, BreadType newBread, string[] expectedPrepInfo)
        {
            CustomSandwich c = new();

            foreach (IngredientType item in ingredientStatus)
            {
                c.AdditionalIngredients[item].Included = true;
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
            CustomSandwich c = new();

            Assert.IsAssignableFrom<IMenuItem>(c);
        }

        /// <summary>
        /// Tests that this item can be cast from Entree
        /// </summary>
        [Fact]
        public void EntreeCastTest()
        {
            CustomSandwich c = new();

            Assert.IsAssignableFrom<Entree>(c);
        }

        #endregion


        /// <summary>
        /// Tests the overriden ToString method
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            CustomSandwich d = new();
            Assert.Equal(d.Name, d.ToString());
        }


        /// <summary>
        /// Tests that the CustomSandwich instance implments the INotifyPropertyChanged interface
        /// </summary>
        [Fact]
        public void ShoudImplementINotifyPropertyChanged()
        {
            CustomSandwich d = new();
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
            CustomSandwich d = new();
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
            CustomSandwich d = new();
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
        [InlineData(new IngredientType[] { IngredientType.Turkey, IngredientType.Ham, IngredientType.Bacon, IngredientType.CheddarCheese, IngredientType.SwissCheese, IngredientType.ChipotleMayo, IngredientType.Lettuce }, "Price")]
        [InlineData(new IngredientType[] { IngredientType.Tomato, IngredientType.RedOnion, IngredientType.CranberrySauce }, "Calories")]
        [InlineData(new IngredientType[] { IngredientType.CreamCheese, IngredientType.ProvoloneCheese, IngredientType.Mayo, IngredientType.Sprouts }, "PreparationInformation")]
        [InlineData(new IngredientType[] { IngredientType.FetaCheese, IngredientType.KalamataOlives, IngredientType.Cucumber }, "Price")]
        [InlineData(new IngredientType[] { IngredientType.RoastedRedPeppers, IngredientType.Salami, IngredientType.Pepperoni, IngredientType.Hummus }, "Calories")]
        [InlineData(new IngredientType[] { IngredientType.ItalianDressing, IngredientType.RanchDressing, IngredientType.Turkey, IngredientType.Ham }, "PreparationInformation")]
        [InlineData(new IngredientType[] { IngredientType.Bacon, IngredientType.CheddarCheese, IngredientType.SwissCheese, IngredientType.ChipotleMayo }, "Price")]
        [InlineData(new IngredientType[] { IngredientType.Lettuce, IngredientType.Tomato, IngredientType.RedOnion }, "Calories")]
        public void CheckAddtionalIngredientsChangeProperties(IngredientType[] ingredients, string affectedProperty)
        {
            CustomSandwich d = new();

            foreach (IngredientType item in ingredients)
            {
                Assert.PropertyChanged(d, affectedProperty, () => {
                    d.AdditionalIngredients[item].Included = true;
                });
            }

        }
    }
}
