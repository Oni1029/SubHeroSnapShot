using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHero.Data.Sides
{
    /// <summary>
    /// The definition of the Apple class
    /// </summary>
    public class Apple : Side
    {
        /// <summary>
        /// Private backing field for the Sliced property
        /// </summary>
        private bool _sliced = false;


        /// <summary>
        /// The name of the Apple instance
        /// </summary>
        public override string Name { get; } = "Apple";

        /// <summary>
        /// The description of this side
        /// </summary>
        public override string Description => "Honeycrisp apple";


        /// <summary>
        /// Whether this apple is sliced
        /// </summary>
        public bool Sliced
        {
            get => _sliced;
            set
            {
                _sliced = value;
                OnPropertyChanged(nameof(Sliced));
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(PreparationInformation));
            }
        }


        /// <summary>
        /// The price of this side
        /// </summary>
        public override decimal Price
        {
            get
            {
                decimal cost = 1.25m;

                if (Sliced) cost += 0.50m; //Cost goes up by 50 cents if apple is ordered as sliced

                return cost;
            }
        }

        /// <summary>
        /// The total number of calories in this side
        /// </summary>
        public override uint Calories
        {
            get
            {
                uint cals = 100;

                return cals;
            }
        }

        /// <summary>
        /// Information for the preparation of this side
        /// </summary>
        public override IEnumerable<string> PreparationInformation
        {
            get
            {
                List<string> instructions = new();

                if (Sliced) instructions.Add("Sliced");

                return instructions;
            }
        }
    }
}
