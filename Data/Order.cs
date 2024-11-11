using SubHero.Data.Entrees;
using SubHero.Data.Drinks;
using SubHero.Data.Sides;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace SubHero.Data
{
    /// <summary>
    /// Represents an order containing multiple menu items
    /// </summary>
    public class Order : ICollection<IMenuItem>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        /// <summary>
        /// Private backing field containing the collection of menu items for this order
        /// </summary>
        private List<IMenuItem> _order = new List<IMenuItem>();

        /// <summary>
        /// Private backing field for the TaxRate property
        /// </summary>
        /// <remarks>
        /// Default tax rate set to Manhattan, KS tax rate (i.e. 9.15%)
        /// </remarks>
        private decimal _taxRate = 0.0915m;

        /// <summary>
        /// Static field containing number of all orders since program started
        /// </summary>
        private static int _nextOrderNumber = 1;


        public Order()
        {
            PlacedAt = DateTime.Now;
            Number = _nextOrderNumber++;
        }

        /// <summary>
        /// Event listener for INotifyCollectionChanged
        /// </summary>
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        /// <summary>
        /// Event listener for INotifyCollectionChanged
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>
        /// The number of items in this Order instance
        /// </summary>
        public int Count
        {
            get
            {
                return _order.Count;
            }
        }

        /// <summary>
        /// Whether this collection is read-only
        /// </summary>
        public bool IsReadOnly => false; 

        /// <summary>
        /// Adds the menu item: <paramref name="item"/> to the current Order instance
        /// </summary>
        /// <param name="item">Menu Item to be added</param>
        public void Add(IMenuItem item)
        {
            _order.Add(item);

            if(item is Side side)
            {
                side.PropertyChanged += HandlePropertyChanged;
            }
            else if (item is Drink drink)
            {
                drink.PropertyChanged += HandlePropertyChanged;
            }
            else if(item is Entree entree)
            {
                entree.PropertyChanged += HandlePropertyChanged;
            }
            else if(item is Combo combo)
            {
                combo.PropertyChanged += HandlePropertyChanged;
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Subtotal)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tax)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Total)));
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        /// <summary>
        /// Clears the current Order instance of all menu items
        /// </summary>
        public void Clear()
        {
            _order.Clear();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Subtotal)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tax)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Total)));
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// Returns whether <paramref name="item"/> is currently in this order instance
        /// </summary>
        /// <param name="item">The menu item to be searched for</param>
        /// <returns>True if the Order contains <paramref name="item"/>, false otherwise</returns>
        public bool Contains(IMenuItem item)
        {
            return _order.Contains(item);
        }

        /// <summary>
        /// Copies the entire Order list to a provided IMenuItem array
        /// </summary>
        /// <param name="array">Array to be copied to</param>
        /// <param name="arrayIndex">The index to begin copying to in <paramref name="array"/></param>
        public void CopyTo(IMenuItem[] array, int arrayIndex)
        {
            _order.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Provides an enumaration for all items contained in this order instance
        /// </summary>
        /// <returns>All items in this order instance (FIXME specify order?)</returns>
        public IEnumerator<IMenuItem> GetEnumerator()
        {
            foreach (IMenuItem item in _order)
            {
                if (item is Combo)
                {
                    yield return item;
                }
            }

            foreach (IMenuItem item in _order)
            {
                if(item is Entree)
                {
                    yield return item;
                }
            }

            foreach (IMenuItem item in _order)
            {
                if (item is Side)
                {
                    yield return item;
                }
            }

            foreach (IMenuItem item in _order)
            {
                if (item is Drink)
                {
                    yield return item;
                }
            }

            
 
        }

        /// <summary>
        /// Removes <paramref name="item"/>, if able
        /// </summary>
        /// <param name="item">The menu item to be removed</param>
        /// <returns>True if <paramref name="item"/> is sucessfully removed, false otherwise</returns>
        public bool Remove(IMenuItem item)
        {
            int index = _order.IndexOf(item);

            if(index != -1)
            {
                _order.Remove(item);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Subtotal)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tax)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Total)));
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Provides an enumeration for all items contained in this order instance
        /// </summary>
        /// <returns>All items in this order instance (FIXME specify order?)</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// The subtotal of all menu items in the current order instance
        /// </summary>
        public decimal Subtotal
        {
            get
            {
                decimal price = 0.00m;

                foreach(IMenuItem item in _order)
                {
                    price += item.Price;
                }

                return price;
            }
        }

        /// <summary>
        /// The TaxRate for this Order instance
        /// </summary>
        public decimal TaxRate
        {
            get
            {
                return _taxRate;
            }
            set
            {
                if(value >= 0.00m && value <= 1.00m)
                {
                    _taxRate = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tax)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TaxRate)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Total)));
                }
            }
        }

        /// <summary>
        /// The Tax for this Order instance
        /// </summary>
        public decimal Tax
        {
            get
            {
                return (Subtotal * TaxRate);
            }
        }

        /// <summary>
        /// The total cost of this Order instance
        /// </summary>
        public decimal Total
        {
            get
            {
                return Subtotal + Tax;
            }
        }

        /// <summary>
        /// The current order number
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// The Date and Time the current order was started
        /// </summary>
        public DateTime PlacedAt { get; } = DateTime.Now;

        /// <summary>
        /// Resets the Order Number back to zero - primarily used in testing
        /// </summary>
        public void ResetOrderNumber()
        {
            _nextOrderNumber = 1;
        }


        /// <summary>
        /// Triggers a property update on order-related items to reflect changes within menu items currently in the order
        /// </summary>
        /// <param name="sender">Sender of this event</param>
        /// <param name="e">Metadata for this event</param>
        private void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Subtotal)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tax)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Total)));
        }

    }
}
