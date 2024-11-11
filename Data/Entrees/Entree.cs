using SubHero.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubHero.Data.Entrees
{
    /// <summary>
    /// Abstract class representing all Entree menu items
    /// </summary>
    public abstract class Entree : IMenuItem, INotifyPropertyChanged
    {
        /// <summary>
        /// Protected field denoting whether this Entree is a specialty entree
        /// </summary>
        protected bool _specialtyItem = false;

        /// <summary>
        /// Backing field for price default values (for specialty entrees - set this value in the constructor)
        /// </summary>
        protected decimal _defaultPrice = 5.99m;

        /// <summary>
        /// Backing field for the CurrentBread property (for specialty entrees - set this value in the constructor)
        /// </summary>
        protected BreadType _bread = BreadType.Wheat;

        /// <summary>
        /// Backing field for the Size property, defaults to Medium
        /// </summary>
        protected SizeType _size = SizeType.Medium;

        /// <summary>
        /// Protected field for this entree item's default ingredients
        /// </summary>
        /// <remarks>
        /// Nullable due to Visual Studio being Visual Studio - no default implementation provided in entree class given that the default
        /// dictionary for additional ingredients is already set to the CustomSandwich's requirements.
        /// </remarks>
        protected IngredientType[]? _defaultIngredientArray;


        /// <summary>
        /// Event listener for INotifyCollectionChanged
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>
        /// Name of this entree
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Description of this entree
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// The default bread type for this entree item
        /// </summary>
        public virtual BreadType DefaultBread { get; protected set; } = BreadType.Wheat;

        /// <summary>
        /// The current bread choice for this entree item
        /// </summary>
        public virtual BreadType CurrentBread
        {
            get
            {
                return _bread;
            }
            set
            {
                BreadType[] temp = BreadOptions; //gets current options for what you can set the bread type to
                foreach (BreadType b in temp)
                {
                    if (value == b)
                    {
                        _bread = value;
                        OnPropertyChanged(nameof(CurrentBread));
                        OnPropertyChanged(nameof(SizeOptions));
                        OnPropertyChanged(nameof(Calories));
                        OnPropertyChanged(nameof(PreparationInformation));
                        break;
                    }
                }
                //if the bread value is not in the current options, nothing happens.
            }
        }

        /// <summary>
        /// Array of the bread options available based off the current size.
        /// </summary>
        public BreadType[] BreadOptions
        {
            get
            {
                BreadType[] bArray;
                if (Size == SizeType.Small)
                {
                    bArray = new BreadType[]
                    {
                        BreadType.Wheat,
                        BreadType.Sourdough,
                        BreadType.Hoagie
                    };
                }
                else if (Size == SizeType.Medium)
                {
                    bArray = new BreadType[]
                    {
                        BreadType.Wheat,
                        BreadType.Wrap,
                        BreadType.Sourdough,
                        BreadType.Hoagie
                    };
                }
                else
                {
                    bArray = new BreadType[]
                    {
                        BreadType.Hoagie
                    };
                }
                return bArray;
            }
        }

        /// <summary>
        /// The current size of the entree
        /// </summary>
        public SizeType Size
        {
            get
            {
                return _size;
            }
            set
            {
                SizeType[] temp = SizeOptions;
                foreach (SizeType s in temp)
                {
                    if (value == s)
                    {
                        _size = s;
                        OnPropertyChanged(nameof(Size));
                        OnPropertyChanged(nameof(BreadOptions));
                        OnPropertyChanged(nameof(Price));
                        OnPropertyChanged(nameof(Calories));
                        OnPropertyChanged(nameof(PreparationInformation));
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Array of the size options available based off the current bread.
        /// </summary>
        public SizeType[] SizeOptions
        {
            get
            {
                SizeType[] sArray;
                if (CurrentBread == BreadType.Wheat || CurrentBread == BreadType.Sourdough)
                {
                    sArray = new SizeType[]
                    {
                        SizeType.Small,
                        SizeType.Medium
                    };
                }
                else if (CurrentBread == BreadType.Wrap)
                {
                    sArray = new SizeType[]
                    {
                        SizeType.Medium
                    };
                }
                else //if the breadtype is hoagie
                {
                    sArray = new SizeType[]
                    {
                        SizeType.Small,
                        SizeType.Medium,
                        SizeType.Large
                    };
                }
                return sArray;
            }
        }

        /// <summary>
        /// Collection of additional ingredients possible for this entree (the key/value pairs will need to be adjusted as needed for specialty entrees via their constructor)
        /// </summary>
        public virtual Dictionary<IngredientType, IngredientItem> AdditionalIngredients { get; set; } = new Dictionary<IngredientType, IngredientItem>();

        /// <summary>
        /// Public list deriving all ingredients for current entree
        /// </summary>
        public List<IngredientItem> AllIngredients => (AdditionalIngredients).Values.ToList();

        /// <summary>
        /// The price of the entree item
        /// </summary>
        public decimal Price
        {
            get
            {
                decimal price = _defaultPrice;

                if (_specialtyItem)
                {
                    foreach (KeyValuePair<IngredientType, IngredientItem> item in AdditionalIngredients)
                    {
                        if (!item.Value.Default && item.Value.Included)
                        {
                            price += item.Value.UnitCost;
                        }
                    }
                }
                else
                {
                    foreach (KeyValuePair<IngredientType, IngredientItem> item in AdditionalIngredients)
                    {
                        if (item.Value.Included)
                        {
                            price += item.Value.UnitCost;
                        }
                    }
                }


                if (Size == SizeType.Small)
                {
                    price -= 3.00m;
                }
                else if (Size == SizeType.Large)
                {
                    price += 2.00m;
                }

                return price;

            }
        }

        /// <summary>
        /// The calories of the entree item
        /// </summary>
        public uint Calories
        {
            get
            {
                uint cals = 0;
                switch (CurrentBread)
                {
                    case BreadType.Wrap:
                        cals += 220;
                        break;
                    case BreadType.Wheat:
                    case BreadType.Sourdough:
                        cals += 250;
                        break;
                    case BreadType.Hoagie:
                        cals += 290;
                        break;
                    default:
                        break;
                }

                foreach (KeyValuePair<IngredientType, IngredientItem> item in AdditionalIngredients)
                {
                    if (item.Value.Included)
                    {
                        cals += item.Value.Calories;
                    }
                }

                switch (Size)
                {
                    case SizeType.Small:
                        cals = (uint)(cals * 0.50);
                        break;
                    case SizeType.Large:
                        cals = (uint)(cals * 1.50);
                        break;
                    default:
                        break;
                }

                return cals;

            }
        }

        /// <summary>
        /// The preparation information of the entree item
        /// </summary>
        public IEnumerable<string> PreparationInformation
        {
            get
            {
                List<string> instructions = new();

                //check if bread is not the default, and add to instructions if true
                //NOTE: added extra check given specificity on MS4 instructions to include "bread" for Wheat & Sourdough.
                if ((CurrentBread != DefaultBread) && (CurrentBread != BreadType.Wrap) && (CurrentBread != BreadType.Hoagie))
                {
                    instructions.Add($"Use {CurrentBread.ToString()} Bread");
                }
                else if(CurrentBread != DefaultBread)
                {
                    instructions.Add($"Use {CurrentBread.ToString()}");
                }

                //NOTE: added Size to prep info below, even though it is never specified in the instructions.
                if(Size != SizeType.Medium)
                {
                    instructions.Add($"{Size.ToString()}");
                }

                //Check for additions/substractions last
                foreach (KeyValuePair<IngredientType, IngredientItem> item in AdditionalIngredients)
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

                return instructions;//FIXME ask if we need to add "If size is not default, add "small" or "large"?

            }
        }

        /// <summary>
        /// Boolean value as to whether this entree is currently part of a combo
        /// </summary>
        public bool PartOfACombo { get; set; } = false;

        /// <summary>
        /// Sets the default values for the AdditionalIngredients property via a provided ingredient array: <paramref name="defaults"/> 
        /// </summary>
        /// <param name="defaults">Array of ingredients to be set as defaults for this entree instance.</param>
        public void SetDefaultIngredients(IngredientType[] defaults)
        {
            foreach (IngredientType item in defaults)
            {
                if (AdditionalIngredients.ContainsKey(item))
                {
                    AdditionalIngredients[item].Default = true; //FIXME may need to account for default CustomSandwich setup? Doublecheck this.
                    AdditionalIngredients[item].Included = true;
                }
            }
        }

        /// <summary>
        /// Adds the IngredientChanged event to all ingredients within the AddtionalIngredients property
        /// </summary>
        public void AddEventToNonDefaultIngredients()
        {
            foreach(KeyValuePair<IngredientType, IngredientItem> item in AdditionalIngredients)
            {
                if (!item.Value.Included)
                {
                    item.Value.PropertyChanged += IngredientChanged;
                }
            }
        }


        /// <summary>
        /// Overrides ToString to return this Entree's Name property
        /// </summary>
        /// <returns>Name property of this Entree instance</returns>
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

        /// <summary>
        /// Triggers a propertyChange event when an ingredients' status is changed
        /// </summary>
        /// <param name="sender">Sender for this event</param>
        /// <param name="e">Metadata for this event</param>
        protected void IngredientChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(Calories));
            OnPropertyChanged(nameof(PreparationInformation));
        }


    }
}
