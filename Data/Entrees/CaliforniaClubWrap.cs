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
    /// The definition of the CaliforniaClubWrap class
    /// </summary>
    public class CaliforniaClubWrap : Entree
    {
        

        /// <summary>
        /// Constructor for CaliforniaClubWrap class
        /// </summary>
        public CaliforniaClubWrap()
        {
            _defaultIngredientArray = new IngredientType[]
            {
                IngredientType.Turkey,
                IngredientType.Bacon,
                IngredientType.Avocado,
                IngredientType.SwissCheese,
                IngredientType.Tomato,
                IngredientType.Sprouts,
                IngredientType.Mayo
            };

            _specialtyItem = true;
            _defaultPrice = 9.49m;
            _bread = BreadType.Wrap;
            DefaultBread = BreadType.Wrap;
            

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
        /// The name of the CaliforniaClubWrap instance
        /// </summary>
        public override string Name { get; } = "California Club Wrap";

        /// <summary>
        /// The description of this wrap
        /// </summary>
        public override string Description => "A California-style club in a wrap";

        
    }
}
