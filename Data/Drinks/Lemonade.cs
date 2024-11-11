using SubHero.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHero.Data.Drinks
{
    /// <summary>
    /// The definition of the Lemonade class
    /// </summary>
    public class Lemonade : Drink
    {

        public Lemonade()
        {
            _defaultPrice = 3.00m;
            _defaultCalories = 160;
        }

        /// <summary>
        /// Private backing field for Strawberry instance
        /// </summary>
        private bool _strawberry = false;

        /// <summary>
        /// The name of the Lemonade instance
        /// </summary>
        public override string Name { get; } = "Lemonade";

        /// <summary>
        /// The description of this drink
        /// </summary>
        public override string Description => "Freshly squeezed lemonade in classic or strawberry flavors";


        /// <summary>
        /// Whether this drink contains Strawberry
        /// </summary>
        public bool Strawberry
        {
            get
            {
                return _strawberry;
            }
            set
            {
                _strawberry = value;
                if (value)
                {
                    _defaultPrice = 3.50m;
                    _defaultCalories = 200;
                }
                else
                {
                    _defaultPrice = 3.00m;
                    _defaultCalories = 160;
                }
                OnPropertyChanged(nameof(Strawberry));
                OnPropertyChanged(nameof(Calories));
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(PreparationInformation));
            }
        }

        
        /// <summary>
        /// Information for the preparation of this drink
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

                if (!Ice) instructions.Add("Hold Ice");

                if (Strawberry) instructions.Add("Add Strawberry");

                return instructions;
            }
        }
    }
}
