using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHero.Data
{
    /// <summary>
    /// Properties that make up all menu items
    /// </summary>
    public interface IMenuItem
    {
        /// <summary>
        /// Name of the menu item
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Description of the menu item
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Price of the menu item
        /// </summary>
        decimal Price { get; }

        /// <summary>
        /// Total calories of the menu item
        /// </summary>
        uint Calories { get; }

        /// <summary>
        /// Preparation information for the menu item
        /// </summary>
        IEnumerable<string> PreparationInformation { get; }
    }
}
