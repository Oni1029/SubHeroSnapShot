using SubHero.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHero.Data.Sides
{
    /// <summary>
    /// Class representing a Chips menu item
    /// </summary>
    public class Chips : Side
    {
        /// <summary>
        /// Private backing field for the Flavor property
        /// </summary>
        private ChipType _flavor = ChipType.Lays;


        /// <summary>
        /// The name of the Chips instance
        /// </summary>
        public override string Name { get; } = "Chips";

        /// <summary>
        /// The description of the Chips instance
        /// </summary>
        public override string Description { get; } = "Your favorite bags of crunchy deliciousness";

        /// <summary>
        /// The flavor of the Chips instance
        /// </summary>
        public ChipType Flavor
        {
            get => _flavor;
            set
            {
                _flavor = value;
                OnPropertyChanged(nameof(Flavor));
                OnPropertyChanged(nameof(Calories));
                OnPropertyChanged(nameof(PreparationInformation));
            }
        }
        /// <summary>
        /// The price of the Chips instance
        /// </summary>
        public override decimal Price
        {
            get
            {
                return 1.95m; //NOTE (future update), using this implemention for now, just in case things need to change later (events, abstraction, etc..)
            }
        }

        /// <summary>
        /// The calories of the Chips instance
        /// </summary>
        public override uint Calories
        {
            get
            {
                uint cals = 0;
                switch (Flavor)
                {
                    case ChipType.Lays:
                        cals += 240;
                        break;
                    case ChipType.Doritos:
                        cals += 250;
                        break;
                    case ChipType.Cheetos:
                        cals += 260;
                        break;
                    case ChipType.SunChips:
                        cals += 210;
                        break;
                    case ChipType.Pretzels:
                        cals += 180;
                        break;
                    default:    
                        break;
                }

                return cals;
            }
        }

        /// <summary>
        /// The preparation instructions for this Chips instance
        /// </summary>
        public override IEnumerable<string> PreparationInformation
        {
            get
            {
                List<string> instructions = new();

                switch (Flavor)
                {
                    case ChipType.Lays:
                        instructions.Add("Lays");
                        break;
                    case ChipType.Doritos:
                        instructions.Add("Doritos");
                        break;
                    case ChipType.Cheetos:
                        instructions.Add("Cheetos");
                        break;
                    case ChipType.SunChips:
                        instructions.Add("Sun Chips");
                        break;
                    case ChipType.Pretzels:
                        instructions.Add("Pretzels");
                        break;
                    default:    
                        break;
                }

                return instructions;

            }
        }

    }
}
