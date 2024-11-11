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
using static System.Collections.Specialized.BitVector32;

namespace SubHero.PointOfSale
{
    /// <summary>
    /// Interaction logic for EditComboControl.xaml
    /// </summary>
    public partial class EditComboControl : UserControl
    {
        /// <summary>
        /// Event handler to signal that an edit button was clicked within the combo instance
        /// </summary>
        public event RoutedEventHandler? ComboEditClicked;

        /// <summary>
        /// Constructs a new EditComboControl
        /// </summary>
        public EditComboControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles a click on the Edit Entree button within this combo
        /// </summary>
        /// <param name="sender">Sender of this event</param>
        /// <param name="e">Metadata for this event</param>
        public void EditEntreeButtonForComboClicked(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if(button.DataContext is Combo combo)
                {
                    button.DataContext = combo.SandwichChoice;
                    ComboEditClicked?.Invoke(sender, e);
                    //button.DataContext = combo;
                }
            }
        }

        /// <summary>
        /// Handles a click on the Edit Side button within this combo
        /// </summary>
        /// <param name="sender">Sender of this event</param>
        /// <param name="e">Metadata for this event</param>
        public void EditSideButtonForComboClicked(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.DataContext is Combo combo)
                {
                    button.DataContext = combo.SideChoice;
                    ComboEditClicked?.Invoke(sender, e);
                }
                else
                {
                    ComboEditClicked?.Invoke(sender, e);
                }
            }
        }

        /// <summary>
        /// Handles a click on the Edit Drink button within this combo
        /// </summary>
        /// <param name="sender">Sender of this event</param>
        /// <param name="e">Metadata for this event</param>
        public void EditDrinkButtonForComboClicked(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.DataContext is Combo combo)
                {
                    button.DataContext = combo.DrinkChoice;
                    ComboEditClicked?.Invoke(sender, e);
                }
                else
                {
                    ComboEditClicked?.Invoke(sender, e);
                }
            }
        }



    }
}
