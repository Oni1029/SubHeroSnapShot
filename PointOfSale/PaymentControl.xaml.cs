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
using System.IO;

namespace SubHero.PointOfSale
{
    /// <summary>
    /// Interaction logic for PaymentControl.xaml
    /// </summary>
    public partial class PaymentControl : UserControl
    {

        /// <summary>
        /// Event handler for when the user wishes to finalize their order,
        /// to be used in the MainWindow for changing the menus and starting a new order
        /// </summary>
        public event RoutedEventHandler? FinalizeOrderClicked;


        /// <summary>
        /// Constructs a new PaymentControl
        /// </summary>
        public PaymentControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Handles a click on the FinalizeAndPrintReceipt button
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">The metadata for this event</param>
        public void FinalizePaymentButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                PaymentViewModel? payMV =  button.DataContext as PaymentViewModel;

                if(payMV != null)
                {
                    File.AppendAllText("receipts.txt", payMV.Receipt);

                    //FIXME - while not listed in instructions, do we want to make a new file whenever Order # is "1"?,
                    //Otherwise, the same receipts.txt keeps printing new instances because the text file already exists from a previous run.
                    //I.e., there could be multiple of the same order number on the receipt list if we restart the program.

                    MessageBox.Show("Receipt printed. Click OK to start a new order.", "Completed Order", MessageBoxButton.OK);

                    FinalizeOrderClicked?.Invoke(sender, e);
                }
                //1)append summary to receipts.txt via relevant property
                //2)Display messagebox
                //3)call above event handler -> 4)in MainWindow, disable all edit screens and begin a new order.

                


            }
        }






    }
}
