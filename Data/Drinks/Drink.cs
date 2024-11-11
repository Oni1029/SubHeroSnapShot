using SubHero.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHero.Data
{
    /// <summary>
    /// Represents all Drink menu items
    /// </summary>
    public abstract class Drink : IMenuItem, INotifyPropertyChanged
    {

        /// <summary>
        /// Event listener for INotifyCollectionChanged
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>
        /// Protected field to be used as a default price for all Drink types
        /// </summary>
        protected decimal _defaultPrice; //set this in the child constructors

        /// <summary>
        /// Protected field to be used as a default calorie count for all Drink types
        /// </summary>
        protected uint _defaultCalories;//set this both in constructor and when changing flavor for certain drink types

        /// <summary>
        /// Protected field to be used as a default size for all Drink types
        /// </summary>
        protected SizeType _size = SizeType.Medium;

        /// <summary>
        /// Protected field to be used as a default size for all Drink types
        /// </summary>
        protected bool _ice = true;


        /// <summary>
        /// The name of this Drink instance
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The Description of this Drink instance
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// The size of this drink
        /// </summary>
        public SizeType Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged(nameof(Size));
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(Calories));
                OnPropertyChanged(nameof(PreparationInformation));
            }
        }

        /// <summary>
        /// Wether this FountainDrink instance contains ice
        /// </summary>
        public bool Ice
        {
            get => _ice;
            set
            {
                _ice = value;
                OnPropertyChanged(nameof(Ice));
                OnPropertyChanged(nameof(PreparationInformation));
            }
        }

        /// <summary>
        /// The Price of this Drink instance
        /// </summary>
        public virtual decimal Price
        {
            get
            {
                decimal p = _defaultPrice;
                switch (Size)
                {
                    case SizeType.Small:
                        p -= 0.25m;
                        break;
                    case SizeType.Large:
                        p += 0.50m;
                        break;
                    default:
                        break;
                }

                return p;
            }
        }

        /// <summary>
        /// The Calories for this Drink instance
        /// </summary>
        public uint Calories
        {
            get
            {
                uint cals = _defaultCalories;

                if (Size == SizeType.Small) cals = (uint)(cals * 0.80); //NOTE ask if this works in class for correct answer
                else if (Size == SizeType.Large) cals = (uint)(cals * 1.60); //NOTE see above.

                return cals;
            }
        }

        /// <summary>
        /// The preparation information for this Drink instance
        /// </summary>
        public abstract IEnumerable<string> PreparationInformation { get; }

        /// <summary>
        /// Overrides ToString to return this Drink's Name property
        /// </summary>
        /// <returns>Name property of this drink instance</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Invokes property changed event
        /// </summary>
        /// <param name="propertyName">Name of the property that has been updated</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
