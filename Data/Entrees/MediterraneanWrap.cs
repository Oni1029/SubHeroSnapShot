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
    /// The definition of the MediterraneanWrap class
    /// </summary>
    public class MediterraneanWrap : Entree
    {
        

        /// <summary>
        /// Constructor for MediterraneanWrap class
        /// </summary>
        public MediterraneanWrap()
        {
            _defaultIngredientArray = new IngredientType[] 
            {
                IngredientType.Hummus,
                IngredientType.FetaCheese,
                IngredientType.KalamataOlives,
                IngredientType.Cucumber,
                IngredientType.Tomato,
                IngredientType.ItalianDressing,
                IngredientType.RoastedRedPeppers
                //IngredientType.Avocado
            };

            _specialtyItem = true;
            _bread = BreadType.Wrap;
            DefaultBread = BreadType.Wrap;
            _defaultPrice = 7.99m;

            AdditionalIngredients.Clear();
            foreach (IngredientType ingredient in _defaultIngredientArray)
            {
                IngredientItem item = new IngredientItem(ingredient);
                item.PropertyChanged += IngredientChanged;
                AdditionalIngredients.Add(ingredient, item);
            }
            //Add any non-default ingredients here
            AdditionalIngredients.Add(IngredientType.Avocado, new IngredientItem(IngredientType.Avocado));

            SetDefaultIngredients(_defaultIngredientArray);
            AddEventToNonDefaultIngredients();

        }


        /// <summary>
        /// The name of the MediterraneanWrap instance
        /// </summary>
        public override string Name { get; } = "Mediterranean Wrap";

        /// <summary>
        /// The description of this wrap
        /// </summary>
        public override string Description => "A vegetarian Mediterranean-style wrap";

        
    }
}
