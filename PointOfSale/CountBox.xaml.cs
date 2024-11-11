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
    /// Interaction logic for CountBox.xaml
    /// </summary>
    public partial class CountBox : UserControl
    {
        /// <summary>
        /// Current count of of the CountBox property
        /// </summary>
        public uint Count
        {
            get
            {
                return (uint)GetValue(CountProperty);
            }
            set
            {
                SetValue(CountProperty, value);
            }
        }

        /// <summary>
        /// DependencyProperty for Count to connect to a parent control's Count
        /// </summary>
        public static readonly DependencyProperty CountProperty = DependencyProperty.Register(nameof(Count), typeof(uint), typeof(CountBox), new PropertyMetadata(1u));

        /// <summary>
        /// Handles a click on the Increment button to increase the amount of Count
        /// </summary>
        /// <param name="sender">Sender of this event</param>
        /// <param name="e">Metadata of this event</param>
        private void HandleIncrement(object sender, RoutedEventArgs e)
        {
            if (Count < 6)
            {
                Count++;
            }

        }

        /// <summary>
        /// Handles a click on the Decrement button to decrease the amount of Count
        /// </summary>
        /// <param name="sender">Sender of this event</param>
        /// <param name="e">Metadata of this event</param>
        private void HandleDecrement(object sender, RoutedEventArgs e)
        {
            if (Count > 2)
            {
                Count--;
            }
        }

        /// <summary>
        /// Constructs a new CountBox instance
        /// </summary>
        public CountBox()
        {
            InitializeComponent();
        }


    }
}
