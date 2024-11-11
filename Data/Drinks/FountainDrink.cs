using SubHero.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHero.Data.Drinks
{
    /// <summary>
    /// Class representing a FountainDrink menu item
    /// </summary>
    public class FountainDrink : Drink
    {
        /// <summary>
        /// Private backing field for the Flavor property.
        /// </summary>
        private SodaType _flavor = SodaType.Coke;

        /// <summary>
        /// Constructs a new FountainDrink instance
        /// </summary>
        public FountainDrink()
        {
            _defaultPrice = 2.00m;
            _defaultCalories = 240;
        }

        /// <summary>
        /// The name of the FountainDrink instance
        /// </summary>
        public override string Name { get; } = "Fountain Drink";

        /// <summary>
        /// The description of the FountainDrink instance
        /// </summary>
        public override string Description { get; } = "Standard soda from the fountain";

        /// <summary>
        /// The flavor of the FountainDrink instance
        /// </summary>
        public SodaType Flavor
        {
            get
            {
                return _flavor;
            }
            set
            {
                _flavor = value;
                switch (value) //NOTE-Might change this when we are using events?
                {
                    case SodaType.Coke:
                        _defaultCalories = 240;
                        break;
                    case SodaType.CokeZero:
                        _defaultCalories = 0;
                        break;
                    case SodaType.DrPepper:
                    case SodaType.OrangeFanta:
                        _defaultCalories = 260;
                        break;
                    case SodaType.MountainDew:
                        _defaultCalories = 280;
                        break;
                    default:
                        break;
                }
                OnPropertyChanged(nameof(Flavor));
                OnPropertyChanged(nameof(Calories));
                OnPropertyChanged(nameof(PreparationInformation));
            }
        }

        

        /// <summary>
        /// The preparation instructions for this FountainDrink instance
        /// </summary>
        public override IEnumerable<string> PreparationInformation
        {
            get
            {
                List<string> instructions = new();

                switch (Size)
                {
                    case SizeType.Small:
                        instructions.Add("Small");
                        break;
                    case SizeType.Medium:
                        instructions.Add("Medium");
                        break;
                    case SizeType.Large:
                        instructions.Add("Large");
                        break;
                    default:
                        break;
                }

                switch (Flavor)
                {
                    case SodaType.Coke:
                        instructions.Add("Coke");
                        break;
                    case SodaType.CokeZero:
                        instructions.Add("Coke Zero");
                        break;
                    case SodaType.DrPepper:
                        instructions.Add("Dr. Pepper");
                        break;
                    case SodaType.OrangeFanta:
                        instructions.Add("Orange Fanta");
                        break;
                    case SodaType.MountainDew:
                        instructions.Add("Mountain Dew");
                        break;
                    default:
                        break;
                }

                if (!Ice) instructions.Add("Hold Ice");

                return instructions;

            }
        }
    }
}
