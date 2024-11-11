using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHero.Data.Sides
{
    /// <summary>
    /// Represents all Side menu items
    /// </summary>
    public abstract class Side : IMenuItem, INotifyPropertyChanged
    {

        /// <summary>
        /// Event listener for INotifyCollectionChanged
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>
        /// The name of the Side instance
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The description of this Side instance
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// The price of this Side instance
        /// </summary>
        public abstract decimal Price { get; }

        /// <summary>
        /// The calories of this Side instance
        /// </summary>
        public abstract uint Calories { get; }

        /// <summary>
        /// The Preparation information for this Side instance
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
