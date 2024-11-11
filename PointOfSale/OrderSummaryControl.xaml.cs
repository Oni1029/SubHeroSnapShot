using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SubHero.PointOfSale
{
    /// <summary>
    /// Interaction logic for OrderSummaryControl.xaml
    /// </summary>
    public partial class OrderSummaryControl : UserControl
    {
        /// <summary>
        /// Event handler for when the edit button is clicked on a given item
        /// </summary>

        public event RoutedEventHandler EditItemSwitchMenu;

        /// <summary>
        /// Event handler for when the remove button is clicked on a given item
        /// </summary>
        public event RoutedEventHandler RemoveItemNotifier;


        /// <summary>
        /// Constructs a new OrderSummaryControl instance
        /// </summary>
        public OrderSummaryControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Adds <paramref name="item"/> to the current order
        /// </summary>
        /// <param name="item">IMenuItem to be added</param>
        public void Add(IMenuItem item)
        {
            if(DataContext is ICollection<IMenuItem> currentOrder)
            {
                currentOrder.Add(item);
            }
        }


        /// <summary>
        /// Handles a click on the remove button per menu item
        /// </summary>
        /// <param name="sender">Sender of this event</param>
        /// <param name="e">Metadata for this event</param>
        public void RemoveItemClick(object sender, RoutedEventArgs e)
        {
            if(sender is Button button)
            {
                if(DataContext is ICollection<IMenuItem> currentOrder)
                {
                    IMenuItem? item = button.DataContext as IMenuItem;

                    if(item != null)
                    {
                        RemoveItemNotifier?.Invoke(sender, e);
                        currentOrder.Remove(item);
                    }
                        
                }
            }
        }

        /// <summary>
        /// Handles the click of the edit button for a given item in the order
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Metadata for the event</param>
        public void EditItemClick(object sender, RoutedEventArgs e)
        {
            if(sender is Button button){
                EditItemSwitchMenu?.Invoke(sender, e);
            }
        }









    }
}
