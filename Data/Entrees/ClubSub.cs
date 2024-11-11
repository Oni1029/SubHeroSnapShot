using SubHero.Data.Enums;
using System.Security.AccessControl;

namespace SubHero.Data.Entrees
{
    /// <summary>
    /// The definition of the ClubSub class
    /// </summary>
    public class ClubSub : Entree
    {

        /// <summary>
        /// Constructor for VeggieSandwich class
        /// </summary>
        public ClubSub()
        {
            _defaultIngredientArray = new IngredientType[]
            {
                IngredientType.Turkey,
                IngredientType.Ham,
                IngredientType.Bacon,
                IngredientType.CheddarCheese,
                IngredientType.ChipotleMayo,
                IngredientType.Lettuce,
                IngredientType.Tomato,
                IngredientType.RedOnion
                //IngredientType.SwissCheese //Add these as non-default
                //IngredientType.Avocado
            };

            _specialtyItem = true;
            _bread = BreadType.Hoagie;
            DefaultBread = BreadType.Hoagie;
            _defaultPrice = 8.99m;

            AdditionalIngredients.Clear();
            foreach (IngredientType ingredient in _defaultIngredientArray)
            {
                IngredientItem item = new IngredientItem(ingredient);
                item.PropertyChanged += IngredientChanged;
                AdditionalIngredients.Add(ingredient, item);

            }
            //Add any non-default ingredients here
            
            AdditionalIngredients.Add(IngredientType.SwissCheese, new IngredientItem(IngredientType.SwissCheese));
            AdditionalIngredients.Add(IngredientType.Avocado, new IngredientItem(IngredientType.Avocado));

            SetDefaultIngredients(_defaultIngredientArray);
            AddEventToNonDefaultIngredients();

        }

        /// <summary>
        /// The name of the ClubSub instance
        /// </summary>
        /// <remarks>
        /// This is an example of an get-only autoproperty with a default value
        /// </remarks>
        public override string Name { get; } = "Club Sub";

        /// <summary>
        /// The description of this sandwich
        /// </summary>
        /// <remarks>
        /// This is also a get-only autoproperty, but it was declared using lambda syntax
        /// </remarks>
        public override string Description => "Classic club sandwich stacked high";




    }
}