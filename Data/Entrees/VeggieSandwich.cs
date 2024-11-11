using SubHero.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubHero.Data.Entrees
{
    /// <summary>
    /// The definition of the VeggieSandwich class
    /// </summary>
    public class VeggieSandwich : Entree
    {

        /// <summary>
        /// Constructor for VeggieSandwich class
        /// </summary>
        public VeggieSandwich()
        {
            _defaultIngredientArray = new IngredientType[]
            {
                IngredientType.ProvoloneCheese,
                IngredientType.CreamCheese,
                IngredientType.Avocado,
                IngredientType.Lettuce,
                IngredientType.Tomato,
                IngredientType.Cucumber,
                IngredientType.ChipotleMayo
            };

            _specialtyItem = true;
            _bread = BreadType.Sourdough;
            DefaultBread = BreadType.Sourdough;
            _defaultPrice = 7.99m;

            AdditionalIngredients.Clear();
            foreach(IngredientType ingredient in _defaultIngredientArray)
            {
                IngredientItem item = new IngredientItem(ingredient);
                item.PropertyChanged += IngredientChanged;
                AdditionalIngredients.Add(ingredient, item);
            }
            //Add any non-default ingredients here

            SetDefaultIngredients(_defaultIngredientArray);
            AddEventToNonDefaultIngredients();

        }

        /// <summary>
        /// The name of the VeggieSandwich instance
        /// </summary>
        public override string Name { get; } = "Veggie Sandwich";

        /// <summary>
        /// The description of this sandwich
        /// </summary>
        public override string Description => "A vegetarian sandwich piled high with crisp veggies and two cheeses";

        
    }
}
