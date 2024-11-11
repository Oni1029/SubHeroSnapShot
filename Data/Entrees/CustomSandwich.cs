using SubHero.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHero.Data.Entrees
{
    /// <summary>
    /// Represents a build-your-own sandwich entree item
    /// </summary>
    public class CustomSandwich : Entree
    {
        /// <summary>
        /// Name of this CustomSandwich instance
        /// </summary>
        public override string Name => "Custom Sandwich";

        /// <summary>
        /// Description of this CustomSandwich instance
        /// </summary>
        public override string Description => "Create your own sandwich with custom bread and toppings";

        public CustomSandwich()
        {
            _defaultIngredientArray = new IngredientType[]
            {
                IngredientType.Turkey,
                IngredientType.Ham,
                IngredientType.Bacon,
                IngredientType.CheddarCheese,
                IngredientType.SwissCheese,
                IngredientType.ChipotleMayo,
                IngredientType.Lettuce,
                IngredientType.Tomato,
                IngredientType.RedOnion,
                IngredientType.Avocado,
                IngredientType.CranberrySauce,
                IngredientType.CreamCheese,
                IngredientType.ProvoloneCheese,
                IngredientType.Mayo,
                IngredientType.Sprouts,
                IngredientType.FetaCheese,
                IngredientType.KalamataOlives,
                IngredientType.Cucumber,
                IngredientType.RoastedRedPeppers,
                IngredientType.Salami,
                IngredientType.Pepperoni,
                IngredientType.Hummus,
                IngredientType.ItalianDressing,
                IngredientType.RanchDressing
            };

            foreach (IngredientType ingredient in _defaultIngredientArray)
            {
                IngredientItem item = new IngredientItem(ingredient);
                item.PropertyChanged += IngredientChanged;
                AdditionalIngredients.Add(ingredient, item);

            }
        }

    }

}
