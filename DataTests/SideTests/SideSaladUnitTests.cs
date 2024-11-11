using SubHero.Data.Entrees;
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
    /// Test Class that verifies that the properties of the SideSalad class are working as intended
    /// </summary>
    public class SideSaladUnitTests
    {

        #region Default Value Tests

        /// <summary>
        /// Tests that the default salad ingredients are actually being set to "default"
        /// </summary>
        [Fact]
        public void DefaultSaladIngredientsTest()
        {
            SideSalad c = new SideSalad();

            foreach (IngredientItem item in c.SaladIngredients.Values)
            {
                if (item.Name != "Avocado")
                {
                    Assert.True(item.Default);
                }
            }
        }

        /// <summary>
        /// Tests that the default salad ingredients are actually being set to "Included"
        /// </summary>
        [Fact]
        public void DefaultSaladIngredientsIncludedTest()
        {
            SideSalad c = new SideSalad();

            foreach (IngredientItem item in c.SaladIngredients.Values)
            {
                if (item.Name != "Avocado")
                {
                    Assert.True(item.Included);
                }
            }
        }

        /// <summary>
        /// Tests that the "included" and "default" attributes for the avocado ingredient are false initially
        /// </summary>
        [Fact]
        public void DefaultStatusesOfAvocadoShouldBeFalse()
        {
            SideSalad c = new SideSalad();

            Assert.False(c.SaladIngredients[IngredientType.Avocado].Default);
            Assert.False(c.SaladIngredients[IngredientType.Avocado].Included);
        }

        /// <summary>
        /// Tests that the default price for a Side Salad is correct
        /// </summary>
        [Fact]
        public void PriceDefaultTest()
        {
            SideSalad c = new SideSalad();

            Assert.Equal(4.99m, c.Price, 2);
        }

        /// <summary>
        /// Tests that the default calories for a Side Salad is correct
        /// </summary>
        [Fact]
        public void CaloriesDefaultTest()
        {
            SideSalad c = new SideSalad();

            Assert.Equal((uint)(20 + (70 * 2 + 10 * 2 + 5 * 2 + 5 * 2 + 200)), c.Calories);
        }

        /// <summary>
        /// Tests that the default Preparation info for a Side Salad is correct
        /// </summary>
        [Fact]
        public void PreparationInformationDefaultTest()
        {
            SideSalad c = new SideSalad();

            Assert.Empty(c.PreparationInformation);

        }

        #endregion


        #region Property Constraint Tests

        /* NOTE: With the amount of subsequent tests that manipulate
         * the SaladIngredients property, there's not really
         * a point of doing a dedicated test for updating the 
         * ingredients' "included" fields
         */


        #endregion


        #region Derived Property Tests
        /// <summary>
        /// Tests that the price property correctly updates when changing the SideSalad's ingredients
        /// </summary>
        /// <param name="expectedPrice">Expected price of the Side Salad</param>
        /// <param name="fetaCheese">Whether this Side Salad includes Feta Cheese</param>
        /// <param name="tomato">Whether this Side Salad includes Tomato</param>
        /// <param name="cucumber">Whether this Side Salad includes Cucumber</param>
        /// <param name="redOnion">Whether this Side Salad includes Red Onion</param>
        /// <param name="ranchDressing">Whether this Side Salad includes Ranch Dressing</param>
        /// <param name="avocado">Whether this Side Salad includes Avocado</param>
        [Theory]
        [InlineData("5.99", true, true, true, true, true, true)]
        [InlineData("4.99", false, false, false, false, false, false)]
        [InlineData("5.99", true, false, true, false, true, true)]
        [InlineData("4.99", true, true, true, true, true, false)]
        [InlineData("5.99", true, false, false, false, true, true)]
        [InlineData("4.99", true, true, true, false, false, false)]
        [InlineData("5.99", true, true, true, true, false, true)]
        [InlineData("5.99", false, false, true, true, true, true)]
        public void PriceUpdateTest(string expectedPrice, bool fetaCheese, bool tomato, bool cucumber, bool redOnion, bool ranchDressing, bool avocado)
        {
            SideSalad c = new SideSalad();
            c.SaladIngredients[IngredientType.FetaCheese].Included = fetaCheese;
            c.SaladIngredients[IngredientType.Tomato].Included = tomato;
            c.SaladIngredients[IngredientType.Cucumber].Included = cucumber;
            c.SaladIngredients[IngredientType.RedOnion].Included = redOnion;
            c.SaladIngredients[IngredientType.RanchDressing].Included = ranchDressing;
            c.SaladIngredients[IngredientType.Avocado].Included = avocado;


            decimal newPrice = Decimal.Parse(expectedPrice);

            Assert.Equal(newPrice, c.Price, 2);
        }

        /// <summary>
        /// Tests that the calories property correctly updates when changing the SideSalad's ingredients
        /// </summary>
        /// <param name="fetaCheese">Whether this Side Salad includes Feta Cheese</param>
        /// <param name="tomato">Whether this Side Salad includes Tomato</param>
        /// <param name="cucumber">Whether this Side Salad includes Cucumber</param>
        /// <param name="redOnion">Whether this Side Salad includes Red Onion</param>
        /// <param name="ranchDressing">Whether this Side Salad includes Ranch Dressing</param>
        /// <param name="avocado">Whether this Side Salad includes Avocado</param>
        /// <param name="expectedCals">Expected calories for the Side Salad</param>
        [Theory]
        [InlineData(true, true, true, true, true, true, (uint)(20 + 140 + 20 + 10 + 10 + 200 + 100))]
        [InlineData(false, false, false, false, false, false, (uint)(20))]
        [InlineData(true, false, true, false, true, true, (uint)(20 + 140 + 10 + 200 + 100))]
        [InlineData(true, true, true, true, true, false, (uint)(20 + 140 + 20 + 10 + 10 + 200))]
        [InlineData(true, false, false, false, true, true, (uint)(20 + 140 + 200 + 100))]
        [InlineData(true, true, true, false, false, false, (uint)(20 + 140 + 20 + 10))]
        [InlineData(true, true, true, true, false, true, (uint)(20 + 140 + 20 + 10 + 10 + 100))]
        [InlineData(false, false, true, true, true, true, (uint)(20 + 10 + 10 + 200 + 100))]
        public void CaloriesUpdateTest(bool fetaCheese, bool tomato, bool cucumber, bool redOnion, bool ranchDressing, bool avocado, uint expectedCals)
        {
            SideSalad c = new SideSalad();
            c.SaladIngredients[IngredientType.FetaCheese].Included = fetaCheese;
            c.SaladIngredients[IngredientType.Tomato].Included = tomato;
            c.SaladIngredients[IngredientType.Cucumber].Included = cucumber;
            c.SaladIngredients[IngredientType.RedOnion].Included = redOnion;
            c.SaladIngredients[IngredientType.RanchDressing].Included = ranchDressing;
            c.SaladIngredients[IngredientType.Avocado].Included = avocado;

            Assert.Equal(expectedCals, c.Calories);
        }

        /// <summary>
        /// Tests that the PreparationInformation property correctly updates when changing the SideSalad's ingredients
        /// </summary>
        /// <param name="fetaCheese">Whether this Side Salad includes Feta Cheese</param>
        /// <param name="tomato">Whether this Side Salad includes Tomato</param>
        /// <param name="cucumber">Whether this Side Salad includes Cucumber</param>
        /// <param name="redOnion">Whether this Side Salad includes Red Onion</param>
        /// <param name="ranchDressing">Whether this Side Salad includes Ranch Dressing</param>
        /// <param name="avocado">Whether this Side Salad includes Avocado</param>
        /// <param name="expectedPrepInfo">Expected preparation info for the Side Salad</param>
        [Theory]
        [InlineData(true, true, true, true, true, true, new string[] { "Add Avocado" })]
        [InlineData(false, false, false, false, false, false, new string[] { "Hold Feta Cheese", "Hold Tomato", "Hold Cucumber", "Hold Red Onion", "Hold Ranch Dressing" })]
        [InlineData(true, false, true, false, true, true, new string[] { "Hold Tomato", "Hold Red Onion", "Add Avocado" })]
        [InlineData(true, true, true, true, true, false, new string[] { })]
        [InlineData(true, false, false, false, true, true, new string[] { "Hold Tomato", "Hold Cucumber", "Hold Red Onion", "Add Avocado" })]
        [InlineData(true, true, true, false, false, false, new string[] { "Hold Red Onion", "Hold Ranch Dressing" })]
        [InlineData(true, true, true, true, false, true, new string[] { "Hold Ranch Dressing", "Add Avocado" })]
        [InlineData(false, false, true, true, true, true, new string[] { "Hold Feta Cheese", "Hold Tomato", "Add Avocado" })]

        public void PreparationInformationUpdateTest(bool fetaCheese, bool tomato, bool cucumber, bool redOnion, bool ranchDressing, bool avocado, string[] expectedPrepInfo)
        {
            SideSalad c = new SideSalad();
            c.SaladIngredients[IngredientType.FetaCheese].Included = fetaCheese;
            c.SaladIngredients[IngredientType.Tomato].Included = tomato;
            c.SaladIngredients[IngredientType.Cucumber].Included = cucumber;
            c.SaladIngredients[IngredientType.RedOnion].Included = redOnion;
            c.SaladIngredients[IngredientType.RanchDressing].Included = ranchDressing;
            c.SaladIngredients[IngredientType.Avocado].Included = avocado;


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
            SideSalad c = new SideSalad();

            Assert.IsAssignableFrom<IMenuItem>(c);
        }

        /// <summary>
        /// Tests that this item can be cast from Side
        /// </summary>
        [Fact]
        public void SideCastTest()
        {
            SideSalad c = new SideSalad();

            Assert.IsAssignableFrom<Side>(c);
        }

        #endregion

        /// <summary>
        /// Tests the overriden ToString method
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            SideSalad d = new();
            Assert.Equal(d.Name, d.ToString());
        }

        /// <summary>
        /// Tests that the Apple instance implments the INotifyPropertyChanged interface
        /// </summary>
        [Fact]
        public void ShoudImplementINotifyPropertyChanged()
        {
            SideSalad d = new();
            Assert.IsAssignableFrom<INotifyPropertyChanged>(d);
        }


        /// <summary>
        /// Tests that PropertyChanged is triggered appropriately when updating the SaladIngredients Property
        /// </summary>
        /// <param name="ingredients">Array of ingredients to update appropriate "Included" values in the SaladIngredients Property</param>
        /// <param name="affectedProperty">Property that should update</param>
        [Theory]
        [InlineData(new IngredientType[] { IngredientType.FetaCheese, IngredientType.Tomato, IngredientType.Cucumber, IngredientType.RedOnion, IngredientType.RanchDressing, IngredientType.Avocado }, "Price")]
        [InlineData(new IngredientType[] { IngredientType.Tomato, IngredientType.Cucumber, IngredientType.RedOnion, IngredientType.RanchDressing, IngredientType.Avocado }, "Calories")]
        [InlineData(new IngredientType[] { IngredientType.Cucumber, IngredientType.RedOnion, IngredientType.RanchDressing, IngredientType.Avocado }, "PreparationInformation")]
        [InlineData(new IngredientType[] { IngredientType.FetaCheese, IngredientType.RanchDressing, IngredientType.Avocado }, "Price")]
        [InlineData(new IngredientType[] { IngredientType.FetaCheese, IngredientType.Tomato, IngredientType.RedOnion,  IngredientType.Avocado }, "Calories")]
        [InlineData(new IngredientType[] { IngredientType.Tomato, IngredientType.Cucumber, IngredientType.RanchDressing, IngredientType.Avocado }, "PreparationInformation")]
        [InlineData(new IngredientType[] { IngredientType.FetaCheese, IngredientType.Tomato, IngredientType.Cucumber, IngredientType.RedOnion }, "Price")]
        [InlineData(new IngredientType[] { IngredientType.Tomato, IngredientType.Cucumber, IngredientType.RedOnion }, "Calories")]
        public void CheckSaladIngredientsChangeProperties(IngredientType[] ingredients, string affectedProperty)
        {
            SideSalad d = new();

            foreach (IngredientType item in ingredients)
            {
                Assert.PropertyChanged(d, affectedProperty, () => {
                    d.SaladIngredients[item].Included = true;
                });
            }
            
        }




    }
}
