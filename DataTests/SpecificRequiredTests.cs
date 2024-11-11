using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubHero.Data.Entrees;
using SubHero.Data.Drinks;
using SubHero.Data.Sides;
using SubHero.Data.Enums;

namespace SubHero.DataTests
{
    /// <summary>
    /// Test Class which conducts specific tests per the professor's instructions
    /// </summary>
    public class SpecificRequiredTests
    {
        /// <summary>
        /// Tests whether a "club sub with both cheeses, avocado added, on small sourdough" is being properly created
        /// </summary>
        [Fact]
        public void ClubSubRequiredTest()
        {
            uint expectedCals = 445;
            decimal expectedPrice = 7.99m;
            string[] expectedPrepInfo = new string[] { "Use Sourdough Bread", "Small", "Add Swiss Cheese", "Add Avocado" };

            ClubSub c = new();
            c.Size = SizeType.Small;
            c.CurrentBread = BreadType.Sourdough;
            c.AdditionalIngredients[IngredientType.SwissCheese].Included = true;
            c.AdditionalIngredients[IngredientType.Avocado].Included = true;
            

            Assert.Equal(expectedCals, c.Calories);
            Assert.Equal(expectedPrice, c.Price, 2);

            Assert.All(expectedPrepInfo, item => Assert.Contains(item, c.PreparationInformation));
            Assert.Equal(expectedPrepInfo.Length, c.PreparationInformation.Count());
        }

        /// <summary>
        /// Tests whether a "mediterranean wrap with no hummus and no italian dressing" is being properly created
        /// </summary>
        [Fact]
        public void MediterraneanWrapRequiredTest()
        {
            uint expectedCals = 335;
            decimal expectedPrice = 7.99m;
            string[] expectedPrepInfo = new string[] { "Hold Hummus", "Hold Italian Dressing" };

            MediterraneanWrap m = new();
            m.AdditionalIngredients[IngredientType.Hummus].Included = false;
            m.AdditionalIngredients[IngredientType.ItalianDressing].Included = false;

            Assert.Equal(expectedCals, m.Calories);
            Assert.Equal(expectedPrice, m.Price, 2);

            Assert.All(expectedPrepInfo, item => Assert.Contains(item, m.PreparationInformation));
            Assert.Equal(expectedPrepInfo.Length, m.PreparationInformation.Count());

        }

        /// <summary>
        /// Tests whether a "custom sandwich with turkey, ham, bacon, ranch dressing, Kalamata Olives, all on a large hoagie" is being properly created
        /// </summary>
        [Fact]
        public void CustomSandwichRequiredTest()
        {
            uint expectedCals = 1147;
            decimal expectedPrice = 11.49m;
            string[] expectedPrepInfo = new string[] { "Use Hoagie", "Large", "Add Turkey", "Add Ham", "Add Bacon", "Add Ranch Dressing", "Add Kalamata Olives" };

            CustomSandwich c = new();
            c.CurrentBread = BreadType.Hoagie;
            c.Size = SizeType.Large;
            c.AdditionalIngredients[IngredientType.Turkey].Included = true;
            c.AdditionalIngredients[IngredientType.Ham].Included = true;
            c.AdditionalIngredients[IngredientType.Bacon].Included = true;
            c.AdditionalIngredients[IngredientType.RanchDressing].Included = true;
            c.AdditionalIngredients[IngredientType.KalamataOlives].Included = true;

            Assert.Equal(expectedCals, c.Calories);
            Assert.Equal(expectedPrice, c.Price, 2);

            Assert.All(expectedPrepInfo, item => Assert.Contains(item, c.PreparationInformation));
            Assert.Equal(expectedPrepInfo.Length, c.PreparationInformation.Count());

        }

        /// <summary>
        /// Tests whether a "side salad with no tomatoes, no red onions, and avocado added" is being properly created
        /// </summary>
        [Fact]
        public void SideSaladRequiredTest()
        {
            uint expectedCals = 470;
            decimal expectedPrice = 5.99m;
            string[] expectedPrepInfo = new string[] { "Hold Tomato", "Hold Red Onion", "Add Avocado" };

            SideSalad s = new();
            s.SaladIngredients[IngredientType.Tomato].Included = false;
            s.SaladIngredients[IngredientType.RedOnion].Included = false;
            s.SaladIngredients[IngredientType.Avocado].Included = true;

            Assert.Equal(expectedCals, s.Calories);
            Assert.Equal(expectedPrice, s.Price, 2);

            Assert.All(expectedPrepInfo, item => Assert.Contains(item, s.PreparationInformation));
            Assert.Equal(expectedPrepInfo.Length, s.PreparationInformation.Count());

        }

        /// <summary>
        /// Tests whether a "large strawberry lemonade" is being properly created
        /// </summary>
        [Fact]
        public void LemonadeRequiredTest()
        {
            uint expectedCals = 320;
            decimal expectedPrice = 4.00m;
            string[] expectedPrepInfo = new string[] { "Large", "Add Strawberry" };

            Lemonade l = new();
            l.Size = SizeType.Large;
            l.Strawberry = true;

            Assert.Equal(expectedCals, l.Calories);
            Assert.Equal(expectedPrice, l.Price, 2);

            Assert.All(expectedPrepInfo, item => Assert.Contains(item, l.PreparationInformation));
            Assert.Equal(expectedPrepInfo.Length, l.PreparationInformation.Count());
        }

        /// <summary>
        /// Tests whether a "Combo with: small turkey cranberry sandwich - add provolone cheese, 3 oatmeal raisin cookies, large mountain dew" is being properly created
        /// </summary>
        [Fact]
        public void ComboRequiredTest()
        {
            uint expectedCals = 1240;
            decimal expectedPrice = 10.99m;
            string[] expectedPrepInfo = new string[] { "Sandwich: Turkey Cranberry Sandwich", "\tSmall", "\tAdd Provolone Cheese", "Side: Cookies", "\t3 Oatmeal Raisin Cookies", "Drink: Fountain Drink", "\tLarge", "\tMountain Dew" };

            Combo c = new();

            c.SandwichChoice = new TurkeyCranberrySandwich() { Size = SizeType.Small };
            c.SandwichChoice.AdditionalIngredients[IngredientType.ProvoloneCheese].Included = true;

            c.SideChoice = new Cookies() { CookieCount = 3, Flavor = CookieType.OatmealRaisin };

            c.DrinkChoice.Size = SizeType.Large;
            if(c.DrinkChoice is FountainDrink f)
            {
                f.Flavor = SodaType.MountainDew;
            }


            Assert.Equal(expectedCals, c.Calories);
            Assert.Equal(expectedPrice, c.Price, 2);

            Assert.All(expectedPrepInfo, item => Assert.Contains(item, c.PreparationInformation));
            Assert.Equal(expectedPrepInfo.Length, c.PreparationInformation.Count());
        }

    }
}
