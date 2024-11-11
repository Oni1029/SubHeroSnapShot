using System.Collections.ObjectModel;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Private field keeping track of the previous control menu opened - used for tracking which menus can be opened or closed
        /// </summary>
        private string _previousControlOpened = "MenuItemMenu";

        /// <summary>
        /// The current order displayed on the OrderSummaryControl
        /// </summary>
        private Order _currentOrder = new Order();

        /// <summary>
        /// Constructs a new MainWindow instance
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _currentOrder;
        }

        /// <summary>
        /// Opens the edit menu of the respective menu item clicked on
        /// </summary>
        /// <param name="sender">Sender of this event</param>
        /// <param name="e">Metadata for this event</param>
        public void EditItemClick(object sender, RoutedEventArgs e)
        {
            if(sender is Button button)
            {
                IMenuItem? item = button.DataContext as IMenuItem;
                if(item != null)
                {
                    EnableEditScreen(item);
                }
               
            }
        }
        
        /// <summary>
        /// Handles a click on the remove button within the OrderSummaryMenu
        /// </summary>
        /// <param name="sender">Sender of this event</param>
        /// <param name="e">Metadata for this event</param>
        public void RemoveItemClick(object sender, RoutedEventArgs e)
        {
            if(sender is Button button)
            {
                IMenuItem? item = null;
                switch (_previousControlOpened)
                {
                    case nameof(EditAppleControl):
                        item = EditAppleMenu.DataContext as IMenuItem;
                        break;
                    case nameof(EditChipsControl):
                        item = EditChipsMenu.DataContext as IMenuItem;
                        break;
                    case nameof(EditCookiesControl):
                        item = EditCookiesMenu.DataContext as IMenuItem;
                        break;
                    case nameof(EditEntreeControl):
                        item = EditEntreeMenu.DataContext as IMenuItem;
                        break;
                    case nameof(EditFountainDrinkControl):
                        item = EditFountainDrinkMenu.DataContext as IMenuItem;
                        break;
                    case nameof(EditIcedTeaControl):
                        item = EditIcedTeaMenu.DataContext as IMenuItem;
                        break;
                    case nameof(EditLemonadeControl):
                        item = EditLemonadeMenu.DataContext as IMenuItem;
                        break;
                    case nameof(EditSideSaladControl):
                        item = EditSideSaladMenu.DataContext as IMenuItem;
                        break;
                    case nameof(EditComboControl):
                        item = EditComboMenu.DataContext as IMenuItem;
                        break;
                    case nameof(PaymentControl):
                        DisableEditScreen();
                        break;
                    default:
                        break;
                }
                if(item != null && button.DataContext == item)
                {
                    DisableEditScreen();
                }
            }
            
        }

        /// <summary>
        /// Handles a click on a menu item and adds the item to the order
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for this event</param>
        public void MenuItemClick(object sender, RoutedEventArgs e)
        {
            if(sender is Button)
            {
                //FIXME may need some more error-checking here.

                IMenuItem item = MenuItemMenu.GetMenuItem();
                OrderSummaryMenu.Add(item);
                

                EnableEditScreen(item);
            }
        }

        /// <summary>
        /// Handles a click on the cancel button on the main menu
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for this event</param>
        public void CancelOrderButtonClick(object sender, RoutedEventArgs e)
        {
            if(sender is Button button)
            {
                DisableEditScreen();
                _currentOrder.Clear();
                DataContext = _currentOrder;
            }
        }

        /// <summary>
        /// Handles a click on the cancel button on the main menu
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for this event</param>
        public void BackToMenuButtonClick(object sender, RoutedEventArgs e)
        {
            if(sender is Button button)
            {
                DisableEditScreen();
            }
        }

        /// <summary>
        /// Handles a click on the Complete Order button on the main menu
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for this event</param>
        public void CompleteOrderButtonClick(object sender, RoutedEventArgs e)
        {
            if(_currentOrder.Count > 0)
            {
                DisableEditScreen();
                MenuItemMenu.Visibility = Visibility.Hidden;

                PaymentMenu.DataContext = new PaymentViewModel(_currentOrder);
                PaymentMenu.Visibility = Visibility.Visible;
                _previousControlOpened = nameof(PaymentControl);
            }
            else
            {
                MessageBox.Show("ERROR - Order is Empty.", "Cannot Complete Order", MessageBoxButton.OK);
            }

        }

        /// <summary>
        /// Completes the current order and begins a new order
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for this event</param>
        public void FinishOrderAndReset(object sender, RoutedEventArgs e)
        {
            DisableEditScreen();
            _currentOrder = new Order();
            DataContext = _currentOrder;
        } 

        /// <summary>
        /// Enables the visibility of the appropriate control depending on which item was clicked on
        /// </summary>
        /// <param name="item">item indicating which control to open</param>
        private void EnableEditScreen(IMenuItem item)
        {
            DisableEditScreen();
            MenuItemMenu.Visibility = Visibility.Hidden;

            if (item is Side)
            {
                switch (item.Name)
                {
                    case "Apple":
                        EditAppleMenu.Visibility = Visibility.Visible;
                        _previousControlOpened = nameof(EditAppleControl);
                        EditAppleMenu.DataContext = item;
                        break;
                    case "Chips":
                        EditChipsMenu.Visibility = Visibility.Visible;
                        _previousControlOpened = nameof(EditChipsControl);
                        EditChipsMenu.DataContext = item;
                        break;
                    case "Cookies":
                        EditCookiesMenu.Visibility = Visibility.Visible;
                        _previousControlOpened = nameof(EditCookiesControl);
                        EditCookiesMenu.DataContext = item;
                        break;
                    case "Side Salad":
                        EditSideSaladMenu.Visibility = Visibility.Visible;
                        _previousControlOpened = nameof(EditSideSaladControl);
                        EditSideSaladMenu.DataContext = item;
                        break;
                    default:
                        //PROBLEM IF REACHED
                        break;
                }
                
            }
            else if(item is Drink)
            {
                //switch between all drink types
                switch (item.Name)
                {
                    case "Fountain Drink":
                        EditFountainDrinkMenu.Visibility = Visibility.Visible;
                        _previousControlOpened = nameof(EditFountainDrinkControl);
                        EditFountainDrinkMenu.DataContext = item;
                        break;
                    case "Iced Tea":
                        EditIcedTeaMenu.Visibility = Visibility.Visible;
                        _previousControlOpened = nameof(EditIcedTeaControl);
                        EditIcedTeaMenu.DataContext = item;
                        break;
                    case "Lemonade":
                        EditLemonadeMenu.Visibility = Visibility.Visible;
                        _previousControlOpened = nameof(EditLemonadeControl);
                        EditLemonadeMenu.DataContext = item;
                        break;
                    default:
                        //PROBLEM IF REACHED
                        break;
                }

            }
            else if(item is Entree itemAsEntree)
            {
                //display catch-all for entrees here
                EditEntreeMenu.Visibility = Visibility.Visible;
                _previousControlOpened = nameof(EditEntreeControl);
                EditEntreeMenu.DataContext = item;
                if (!itemAsEntree.PartOfACombo)
                {
                    EditEntreeMenu.MakeItAComboButton.Visibility = Visibility.Visible;
                }
                else
                {
                    EditEntreeMenu.MakeItAComboButton.Visibility = Visibility.Hidden;
                }

            }
            else if(item is Combo combo)
            {
                EditComboMenu.Visibility = Visibility.Visible;
                _previousControlOpened = nameof(EditComboControl);
                EditComboMenu.DataContext = item;

                EditComboMenu.EditEntreeInsideCombo.DataContext = combo;
                EditComboMenu.EditDrinkInsideCombo.DataContext = combo;
                EditComboMenu.EditSideInsideCombo.DataContext = combo;
                
            }

        }

        /// <summary>
        /// Disables the current Edit control currently visible, if able.
        /// </summary>
        private void DisableEditScreen()
        {
            //switch statement for _previousControlOpened, hide visibility of
            //current control, and switch open control to the-
            //-mainmenu (MenuItemSelectionControl)

            switch (_previousControlOpened)
            {
                case nameof(EditAppleControl):
                    EditAppleMenu.Visibility = Visibility.Hidden;
                    break;
                case nameof(EditChipsControl):
                    EditChipsMenu.Visibility = Visibility.Hidden;
                    break;
                case nameof(EditCookiesControl):
                    EditCookiesMenu.Visibility = Visibility.Hidden;
                    break;
                case nameof(EditSideSaladControl):
                    EditSideSaladMenu.Visibility = Visibility.Hidden;
                    break;
                case nameof(EditFountainDrinkControl):
                    EditFountainDrinkMenu.Visibility = Visibility.Hidden;
                    break;
                case nameof(EditIcedTeaControl):
                    EditIcedTeaMenu.Visibility = Visibility.Hidden;
                    break;
                case nameof(EditLemonadeControl):
                    EditLemonadeMenu.Visibility = Visibility.Hidden;
                    break;
                case nameof(EditEntreeControl):
                    EditEntreeMenu.Visibility = Visibility.Hidden;
                    break;
                case nameof(EditComboControl):
                    EditComboMenu.Visibility = Visibility.Hidden;
                    break;
                case nameof(PaymentControl):
                    PaymentMenu.Visibility = Visibility.Hidden;
                    break;
                default:
                    break;
            }
            MenuItemMenu.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// Handles a click of the "Make it a Combo" button within an entree and sets the appropriate control's visibility
        /// </summary>
        /// <param name="sender">Sender of this event</param>
        /// <param name="e">Metadata for this event</param>
        public void ComboButtonClickedWithinEntree(object sender, RoutedEventArgs e)
        {
            if(sender is Button button)
            {
                IMenuItem item = button.DataContext as Entree;

                _currentOrder.Remove(item);
                DisableEditScreen();

                Combo comboItem = new Combo();
                comboItem.SandwichChoice = item as Entree;
                comboItem.SideChoice = new Chips();
                comboItem.DrinkChoice = new FountainDrink();



                OrderSummaryMenu.Add(comboItem);

                EnableEditScreen(comboItem);

            }
        }

        /// <summary>
        /// Handles the click event of any of the edit buttons within a given combo control
        /// </summary>
        /// <param name="sender">Sender of this event</param>
        /// <param name="e">Metadata for this event</param>
        public void EditItemWithinCombo(object sender, RoutedEventArgs e)
        {
            if(sender is Button button)
            {

                IMenuItem comboItem = button.DataContext as IMenuItem;
                EnableEditScreen(comboItem);
            }
            
        }



    }
}