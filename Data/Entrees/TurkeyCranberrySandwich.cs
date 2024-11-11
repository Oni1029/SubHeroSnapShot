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
    /// The definition of the TurkeyCranberrySandwich class
    /// </summary>
    public class TurkeyCranberrySandwich : Entree
    {
        
        /// <summary>
        /// Constructor for TurkeyCranberrySandwich class
        /// </summary>
        public TurkeyCranberrySandwich()
        {
            _defaultIngredientArray = new IngredientType[]
            {
                IngredientType.Turkey,
                IngredientType.CranberrySauce,
                IngredientType.CreamCheese,
                IngredientType.RedOnion,
                //IngredientType.ProvoloneCheese //Add this as non-default
            };

            _specialtyItem = true;
            _bread = BreadType.Wheat;
            DefaultBread = BreadType.Wheat;
            _defaultPrice = 8.49m; 

            AdditionalIngredients.Clear();
            foreach (IngredientType ingredient in _defaultIngredientArray)
            {
                IngredientItem item = new IngredientItem(ingredient);
                item.PropertyChanged += IngredientChanged;
                AdditionalIngredients.Add(ingredient, item);
            }
            //Add any non-default ingredients here
            AdditionalIngredients.Add(IngredientType.ProvoloneCheese, new IngredientItem(IngredientType.ProvoloneCheese));

            SetDefaultIngredients(_defaultIngredientArray);
            AddEventToNonDefaultIngredients();

        }

        /// <summary>
        /// The name of the TurkeyCranberrySandwich instance
        /// </summary>
        public override string Name { get; } = "Turkey Cranberry Sandwich";

        /// <summary>
        /// The description of this sandwich
        /// </summary>
        public override string Description => "A festive sandwich with turkey, cranberry sauce, and cream cheese";

        
    }
}
