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
    /// Interaction logic for EntreeDisplayControl.xaml
    /// </summary>
    public partial class EditEntreeControl : UserControl
    {

        public event RoutedEventHandler ComboButtonClicked;

        /// <summary>
        /// Constructs a new EditEntreeControl
        /// </summary>
        public EditEntreeControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles a click on the "Make it a combo" button within this entree
        /// </summary>
        /// <param name="sender">Sender of this event</param>
        /// <param name="e">Metadata for this event</param>
        public void ComboButtonClickEvent(object sender, RoutedEventArgs e)
        {
            
            if (sender is Button button)
            {
                Entree item = button.DataContext as Entree;
                item.PartOfACombo = true;
                //button.Visibility = Visibility.Hidden;
            }
            
            ComboButtonClicked?.Invoke(sender, e);

        }


    }
}
