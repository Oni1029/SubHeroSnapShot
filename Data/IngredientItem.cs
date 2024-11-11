using SubHero.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHero.Data
{
    /// <summary>
    /// Represents an ingredient for a menu item
    /// </summary>
    public class IngredientItem : INotifyPropertyChanged
    {

        /// <summary>
        /// private backing field for Included property
        /// </summary>
        private bool _included = false;


        public event PropertyChangedEventHandler? PropertyChanged;

        public IngredientItem(IngredientType itemType)
        {
            Ingredient = itemType;
        }



        /// <summary>
        /// The type of ingredient
        /// </summary>
        public IngredientType Ingredient { get; init; } 

        /// <summary>
        /// Name of the ingredient
        /// </summary>
        public string Name
        {
            get
            {
                string name = "";

                switch (Ingredient.ToString())
                {
                    case "CheddarCheese":
                        name = "Cheddar Cheese";
                        break;
                    case "SwissCheese":
                        name = "Swiss Cheese";
                        break;
                    case "ChipotleMayo":
                        name = "Chipotle Mayo";
                        break;
                    case "RedOnion":
                        name = "Red Onion";
                        break;
                    case "CranberrySauce":
                        name = "Cranberry Sauce";
                        break;
                    case "CreamCheese":
                        name = "Cream Cheese";
                        break;
                    case "ProvoloneCheese":
                        name = "Provolone Cheese";
                        break;
                    case "FetaCheese":
                        name = "Feta Cheese";
                        break;
                    case "KalamataOlives":
                        name = "Kalamata Olives";
                        break;
                    case "RoastedRedPeppers":
                        name = "Roasted Red Peppers";
                        break;
                    case "ItalianDressing":
                        name = "Italian Dressing";
                        break;
                    case "RanchDressing":
                        name = "Ranch Dressing";
                        break;
                    default:
                        name = Ingredient.ToString(); //all remaining ingredient possibilities are one word items, so this should be fine as a default
                        break;
                }

                return name;

            }
        }

        /// <summary>
        /// Calories of the ingredient
        /// </summary>
        public uint Calories
        {
            get
            {
                uint cals = 0;

                switch (Ingredient.ToString())
                {
                    case "Turkey":
                    case "Ham":
                    case "CheddarCheese":
                    case "SwissCheese":
                    case "ProvoloneCheese":
                        cals = 80;
                        break;
                    case "Bacon":
                    case "Avocado":
                    case "Mayo":
                    case "ChipotleMayo":
                        cals = 100;
                        break;
                    case "Lettuce":
                    case "RedOnion":
                    case "Cucumber":
                    case "Sprouts":
                        cals = 5;
                        break;
                    case "Tomato":
                        cals = 10;
                        break;
                    case "CranberrySauce":
                    case "Salami":
                    case "Hummus":
                        cals = 120;
                        break;
                    case "CreamCheese":
                        cals = 150;
                        break;
                    case "FetaCheese":
                    case "ItalianDressing":
                        cals = 70;
                        break;
                    case "KalamataOlives":
                    case "RoastedRedPeppers":
                        cals = 15;
                        break;
                    case "Pepperoni":
                        cals = 90;
                        break;
                    case "RanchDressing":
                        cals = 200;
                        break;
                    default:
                        break;
                }

                return cals;
            }
        }

        /// <summary>
        /// The cost of this ingredient (per unit)
        /// </summary>
        public decimal UnitCost
        {
            get
            {
                decimal price;

                switch (Ingredient.ToString())
                {
                    case "ChipotleMayo":
                    case "Lettuce":
                    case "Tomato":
                    case "RedOnion":
                    case "Mayo":
                    case "Sprouts":
                    case "Cucumber":
                    case "ItalianDressing":
                    case "RanchDressing":
                        price = 0.00m;
                        break;
                    case "KalamataOlives":
                    case "RoastedRedPeppers":
                        price = 0.50m;
                        break;
                    default:
                        price = 1.00m; //All remaining items should be: meats, cheeses, avocado, cranberry sauce, and hummus } all of which are $1.00
                        break;
                }

                return price;
            }
        }

        /// <summary>
        /// Whether this ingredient is currently included in a containing menu item
        /// </summary>
        public bool Included
        {
            get => _included;
            set
            {
                _included = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Included)));
            }
        }

        /// <summary>
        /// Whether this ingredient is included in its containing menu item by default
        /// </summary>
        public bool Default { get; set; } = false;


    }
}
