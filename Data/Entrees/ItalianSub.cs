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
    /// The definition of the ItalianSub class
    /// </summary>
    public class ItalianSub : Entree
    {

        /// <summary>
        /// Constructor for ItalianSub class
        /// </summary>
        public ItalianSub()
        {
            _defaultIngredientArray = new IngredientType[]
            {
                IngredientType.Salami,
                IngredientType.Pepperoni,
                IngredientType.Ham,
                IngredientType.ProvoloneCheese,
                IngredientType.CheddarCheese,
                IngredientType.Lettuce,
                IngredientType.Tomato,
                IngredientType.ItalianDressing,
                IngredientType.RoastedRedPeppers
            };

            _specialtyItem = true;
            _bread = BreadType.Hoagie;
            DefaultBread = BreadType.Hoagie;
            _defaultPrice = 10.99m; 

            AdditionalIngredients.Clear();
            foreach (IngredientType ingredient in _defaultIngredientArray)
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
        /// The name of the ItalianSub instance
        /// </summary>
        public override string Name { get; } = "Italian Sub";

        /// <summary>
        /// The description of this sandwich
        /// </summary>
        public override string Description => "An Italian-inspired sub with layers of savory meats and cheeses";

        
    }
}
