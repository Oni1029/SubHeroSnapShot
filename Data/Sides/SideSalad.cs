using SubHero.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHero.Data.Sides
{
    /// <summary>
    /// The definition of the SideSalad class
    /// </summary>
    public class SideSalad : Side
    {
        /// <summary>
        /// Private field for SaladIngredients' default setup
        /// </summary>
        private IngredientType[]? _defaultIngredientArray;



        /// <summary>
        /// The name of the SideSalad instance
        /// </summary>
        public override string Name { get; } = "Side Salad";

        /// <summary>
        /// The description of this salad
        /// </summary>
        public override string Description => "Garden salad with lots of veggies";

        /// <summary>
        /// All of the available salad ingredients
        /// </summary>
        public Dictionary<IngredientType, IngredientItem> SaladIngredients { get; set; } = new Dictionary<IngredientType, IngredientItem>();
        /*
        {
            {IngredientType.FetaCheese, new IngredientItem(IngredientType.FetaCheese){Default = true , Included = true } },
            {IngredientType.Tomato, new IngredientItem(IngredientType.Tomato){Default = true , Included = true } },
            {IngredientType.Cucumber, new IngredientItem(IngredientType.Cucumber){Default = true , Included = true } },
            {IngredientType.RedOnion, new IngredientItem(IngredientType.RedOnion){Default = true , Included = true } },
            {IngredientType.RanchDressing, new IngredientItem(IngredientType.RanchDressing){Default = true , Included = true } },
            {IngredientType.Avocado, new IngredientItem(IngredientType.Avocado) }//not default and not included when created
        };
        */

        /// <summary>
        /// Public list deriving all ingredients for current entree
        /// </summary>
        public List<IngredientItem> AllIngredients => (SaladIngredients).Values.ToList();

        /// <summary>
        /// The price of this salad
        /// </summary>
        public override decimal Price
        {
            get
            {
                decimal cost = 4.99m;

                if (SaladIngredients.ContainsKey(IngredientType.Avocado))
                {
                    if (SaladIngredients[IngredientType.Avocado].Included)
                    {
                        cost += 1.00m;
                    }
                }

                return cost;
            }
        }

        /// <summary>
        /// The total number of calories in this salad
        /// </summary>
        public override uint Calories
        {
            get
            {
                uint cals = 20;

                foreach(KeyValuePair<IngredientType, IngredientItem> item in SaladIngredients)
                {
                    if (item.Value.Included) 
                    {
                        if(item.Value.Name != "Avocado" && item.Value.Name != "Ranch Dressing")
                        {
                            cals += 2 * item.Value.Calories;
                        }
                        else
                        {
                            cals += item.Value.Calories;
                        }
                    }
                }

                return cals;
            }
        }

        /// <summary>
        /// The preparation information of the entree item
        /// </summary>
        public override IEnumerable<string> PreparationInformation
        {
            get
            {
                List<string> instructions = new();

                //Check for additions/substractions last
                foreach (KeyValuePair<IngredientType, IngredientItem> item in SaladIngredients)
                {
                    if (!item.Value.Default && item.Value.Included)
                    {
                        instructions.Add($"Add {item.Value.Name}");
                    }
                    else if (item.Value.Default && !item.Value.Included)
                    {
                        instructions.Add($"Hold {item.Value.Name}");
                    }

                }

                return instructions;//FIXME see if perparation information is actually done

            }
        }

        public SideSalad()
        {
            _defaultIngredientArray = new IngredientType[]
            {
                IngredientType.FetaCheese,
                IngredientType.Tomato,
                IngredientType.Cucumber,
                IngredientType.RedOnion,
                IngredientType.RanchDressing,
                IngredientType.Avocado
            };

            foreach(IngredientType ingredient in _defaultIngredientArray)
            {
                IngredientItem item = new IngredientItem(ingredient);
                item.PropertyChanged += IngredientChanged;
                if (ingredient != IngredientType.Avocado)
                {
                    item.Default = true;
                    item.Included = true;
                }
                SaladIngredients.Add(ingredient, item);
            }
        }


        private void IngredientChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(Calories));
            OnPropertyChanged(nameof(PreparationInformation));

        }
    }
}
