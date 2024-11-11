using SubHero.Data.Entrees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubHero.Data.Enums;
using SubHero.Data.Sides;
using SubHero.Data.Drinks;

namespace SubHero.DataTests
{
    /// <summary>
    /// Test Class that verifies that the properties of the PaymentViewModel class are working as intended
    /// </summary>
    public class PaymentViewModelUnitTests
    {
        /// <summary>
        /// Integration Test regarding common scenarios found in the PaymentView and PaymentViewModel classes
        /// </summary>
        [Fact]
        public void ViewModelGeneralTests()
        {

            Order order = new();

            Assert.Equal(1, order.Number); //first order number should be 1.

            //-----------------------------------------------------------------------------

            //Combo section:
            ClubSub sandwich = new ClubSub { Size = SizeType.Small, CurrentBread = BreadType.Wheat };
            sandwich.AdditionalIngredients[IngredientType.SwissCheese].Included = true;
            sandwich.AdditionalIngredients[IngredientType.CheddarCheese].Included = true;
            sandwich.AdditionalIngredients[IngredientType.Bacon].Included = false;

            SideSalad side = new SideSalad();
            side.SaladIngredients[IngredientType.Avocado].Included = true;
            side.SaladIngredients[IngredientType.FetaCheese].Included = false;

            Combo c = new Combo()
            {
                SandwichChoice = sandwich,
                SideChoice = side,
                DrinkChoice = new FountainDrink() { Size = SizeType.Large, Flavor = SodaType.MountainDew, Ice = false }
            };

            //Custom Sandwich section:
            CustomSandwich custom = new CustomSandwich() { CurrentBread = BreadType.Wrap };
            custom.AdditionalIngredients[IngredientType.Ham].Included = true;
            custom.AdditionalIngredients[IngredientType.Turkey].Included = true;
            custom.AdditionalIngredients[IngredientType.Bacon].Included = true;
            custom.AdditionalIngredients[IngredientType.Avocado].Included = true;
            custom.AdditionalIngredients[IngredientType.Mayo].Included = true;
            custom.AdditionalIngredients[IngredientType.Lettuce].Included = true;

            //Cookies section:
            Cookies cookies = new Cookies() { CookieCount = 5, Flavor = CookieType.Sugar };

            order.Add(c);
            order.Add(custom);
            order.Add(cookies);

            Assert.Equal(30.12m, order.Subtotal, 2); //Subtotal should be $30.12
            Assert.Equal(2.76m, order.Tax, 2); //Tax should be $2.76
            Assert.Equal(32.88m, order.Total, 2); //Total should be $32.88

            //-----------------------------------------------------------------------------

            PaymentViewModel pvm = new PaymentViewModel(order);
            pvm.Paid = 40.00m;

            Assert.Equal(7.12m, pvm.Change, 2); //Change should be $7.12

            //-----------------------------------------------------------------------------

            Order order2 = new();

            Assert.Equal(2, order2.Number); //Second order created in this instance should be 2

            //-----------------------------------------------------------------------------

            ItalianSub italian = new ItalianSub() { Size = SizeType.Large };
            italian.AdditionalIngredients[IngredientType.Salami].Included = false;

            Chips chips = new Chips() { Flavor = ChipType.SunChips };

            Lemonade lemonade = new Lemonade() { Size = SizeType.Small, Strawberry = true };

            order2.Add(italian);
            order2.Add(chips);
            order2.Add(lemonade);

            Assert.Equal(18.19m, order2.Subtotal, 2); //Subtotal should be $18.19
            Assert.Equal(1.66m, order2.Tax, 2); //Tax should be $1.66
            Assert.Equal(19.85m, order2.Total, 2); //Total should be $19.85

            //-----------------------------------------------------------------------------

            PaymentViewModel pvm2 = new PaymentViewModel(order2);

            Assert.Throws<ArgumentException>(() => pvm2.Paid = 15.00m);






        }







    }
}
