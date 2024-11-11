using SubHero.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHero.Data.Drinks
{
    /// <summary>
    /// The definition of the IcedTea class
    /// </summary>
    public class IcedTea : Drink
    {
        /// <summary>
        /// Private backing field for the Sweet property
        /// </summary>
        private bool _sweet = true;

        /// <summary>
        /// Constructs a new IcedTea instance
        /// </summary>
        public IcedTea()
        {
            _defaultPrice = 2.50m;
            _defaultCalories = 180;
        }

        /// <summary>
        /// The name of the IcedTea instance
        /// </summary>
        public override string Name { get; } = "Iced Tea";

        /// <summary>
        /// The description of this drink
        /// </summary>
        public override string Description => "Freshly brewed iced tea with choice of sweetness";

        

        /// <summary>
        /// Whether this drink is Sweet tea
        /// </summary>
        public bool Sweet
        {
            get
            {
                return _sweet;
            }
            set
            {
                _sweet = value;
                if (value)
                {
                    _defaultCalories = 180;
                }
                else
                {
                    _defaultCalories = 5;
                }
                OnPropertyChanged(nameof(Sweet));
                OnPropertyChanged(nameof(Calories));
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
                if (!Sweet) instructions.Add("Unsweetened");

                return instructions;
            }
        }
    }
}
