using SubHero.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubHero.Data.Sides
{
    /// <summary>
    /// Class representing the Cookies menu item
    /// </summary>
    public class Cookies : Side
    {
        /// <summary>
        /// private backing field for the Count property
        /// </summary>
        private uint _count = 2;

        /// <summary>
        /// private backing field for the Flavor property
        /// </summary>
        private CookieType _flavor = CookieType.ChocolateChip;

        /// <summary>
        /// The name of the Cookies instance
        /// </summary>
        public override string Name { get; } = "Cookies"; 

        /// <summary>
        /// The description of the Cookies instance
        /// </summary>
        public override string Description { get; } = "Freshly baked cookies in a variety of flavors";

        /// <summary>
        /// The flavor of the Cookies instance
        /// </summary>
        public CookieType Flavor
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
        /// The total count of cookies in this Cookies instance
        /// </summary>
        public uint CookieCount
        {
            get
            {
                return _count;
            }
            set
            {
                if(value >= 2 && value <= 6) //range is 2-6 cookies per instance.
                {
                    _count = value;
                    OnPropertyChanged(nameof(CookieCount));
                    OnPropertyChanged(nameof(Price));
                    OnPropertyChanged(nameof(Calories));
                    OnPropertyChanged(nameof(PreparationInformation));
                }
            }
        }

        /// <summary>
        /// The price of the Cookies instance
        /// </summary>
        public override decimal Price
        {
            get
            {
                decimal price = 0.00m;

                if(CookieCount%2 == 0) //case of even count
                {
                    price = 3.00m * (CookieCount / 2);
                }
                else //case of odd count
                {
                    price = (3.00m * (CookieCount / 2)) + 1.75m;
                }

                return price;
            }
        }

        /// <summary>
        /// The calories of the Cookies instance
        /// </summary>
        public override uint Calories
        {
            get
            {
                uint cals = CookieCount; //calories will be multiplied per cookie in instance.
                switch (Flavor)
                {
                    case CookieType.ChocolateChip:
                        cals *= 180;
                        break;
                    case CookieType.OatmealRaisin:
                        cals *= 150;
                        break;
                    case CookieType.Sugar:
                        cals *= 160;
                        break;
                    case CookieType.PeanutButter:
                        cals *= 190;
                        break;
                    default:    
                        break;
                }

                return cals;
            }
        }

        /// <summary>
        /// The preparation instructions for this Cookies instance
        /// </summary>
        public override IEnumerable<string> PreparationInformation
        {
            get
            {
                List<string> instructions = new();

                switch (Flavor)
                {
                    case CookieType.ChocolateChip:
                        instructions.Add($"{CookieCount} Chocolate Chip Cookies");
                        break;
                    case CookieType.OatmealRaisin:
                        instructions.Add($"{CookieCount} Oatmeal Raisin Cookies");
                        break;
                    case CookieType.Sugar:
                        instructions.Add($"{CookieCount} Sugar Cookies");
                        break;
                    case CookieType.PeanutButter:
                        instructions.Add($"{CookieCount} Peanut Butter Cookies");
                        break;
                    default:    
                        break;
                }

                return instructions;

            }
        }

    }
}
