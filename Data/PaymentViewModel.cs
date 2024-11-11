using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using SubHero.Data;

namespace SubHero.Data
{
    /// <summary>
    /// ViewModel for the PaymentControl
    /// </summary>
    public class PaymentViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The current order to be completed
        /// </summary>
        private Order _overallOrder;

        /// <summary>
        /// private backing field for the Paid property
        /// </summary>
        private decimal _paid;

        /// <summary>
        /// private backing field for the Change property
        /// </summary>
        private readonly decimal _change;

        /// <summary>
        /// private backing field for the EnoughFunds property
        /// </summary>
        private bool _enoughFunds = true;

        /// <summary>
        /// Event Handler to notify when certain properties change
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Constructs a new PaymentViewModel
        /// </summary>
        /// <param name="currentOrder">The current order needing to be paid for & output as a receipt</param>
        public PaymentViewModel(Order currentOrder)
        {
            _overallOrder = currentOrder;
            _paid = Math.Round(currentOrder.Total, 2);

            //FIXME, currently unused as menu will not open if order is empty.
            if (currentOrder.Total == 0.00m)
            {
                _enoughFunds = false;
                Paid = 0.00m;//FIXME - Test for possible empty order error
            }
        }



        /// <summary>
        /// The subtotal of all menu items in the current order instance
        /// </summary>
        public decimal Subtotal
        {
            get => _overallOrder.Subtotal;
        }

        /// <summary>
        /// The Tax for this Order instance
        /// </summary>
        public decimal Tax
        {
            get => _overallOrder.Tax;
        }

        /// <summary>
        /// The total cost of this Order instance
        /// </summary>
        public decimal Total
        {
            get => Math.Round(_overallOrder.Total, 2);
        }

        /// <summary>
        /// The total amount of money paid for this order
        /// </summary>
        public decimal Paid
        {
            get
            {
                return _paid;
            }
            set
            {
                decimal temp = Math.Round(value, 2);
                if (temp < Total)
                {
                    EnoughFunds = false;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnoughFunds)));
                    throw new ArgumentException("Insufficient funds"); //FIXME
                }
                else
                {
                    _paid = value;
                    EnoughFunds = true;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Paid)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Change)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Receipt)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnoughFunds)));
                }
            }
        }

        /// <summary>
        /// The total amount of change the user will be given
        /// dependant on the amount they paid for this order
        /// </summary>
        public decimal Change
        {
            get
            {
                return Paid - Total;
            }
        }

        /// <summary>
        /// A string representation of a receipt for the order
        /// </summary>
        public string Receipt
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Order Number: " + _overallOrder.Number).AppendLine();
                sb.Append(_overallOrder.PlacedAt).AppendLine();
                sb.AppendLine();

                foreach (IMenuItem item in _overallOrder)
                {
                    sb.Append(item.Name + "\t$" + Math.Round(item.Price, 2)).AppendLine();
                    foreach (string s in item.PreparationInformation)
                    {
                        sb.Append("\t" + s).AppendLine();
                    }
                    sb.AppendLine();
                }

                sb.AppendLine();
                sb.Append("Subtotal: $" + Math.Round(Subtotal, 2)).AppendLine();
                sb.Append("Tax: $" + Math.Round(Tax, 2)).AppendLine();
                sb.Append("Total: $" + Math.Round(Total, 2)).AppendLine().AppendLine();
                sb.Append("Amount Paid: $" + Math.Round(Paid, 2)).AppendLine();
                sb.Append("Change: $" + Math.Round(Change, 2)).AppendLine().AppendLine();
                sb.Append("----------------------------------------------").AppendLine();


                return sb.ToString();
            }
        }

        /// <summary>
        /// Boolean value representing whether the user has paid enough for the order
        /// </summary>
        public bool EnoughFunds
        {
            get
            {
                return _enoughFunds;
            }
            set
            {
                _enoughFunds = value;
            }
        }

        /// <summary>
        /// Updates the paymentViewModel's relevant information if an item is removed while the summary menu is open.
        /// </summary>
        /// <remarks>
        /// NOTE: currently unused as-is, I'd like to get the related functionality working for this, but ran out of time.
        /// </remarks>
        /// <param name="updatedOrder">new order with updated information post-'item removal'</param>
        public void UpdateAllValuesDueToRemoval(Order updatedOrder)
        {
            _overallOrder = updatedOrder;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Subtotal)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tax)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Total)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Change)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Receipt)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnoughFunds)));

        }

    }
}

