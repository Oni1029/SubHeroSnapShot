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
    /// Interaction logic for MenuItemSelectionControl.xaml
    /// </summary>
    public partial class MenuItemSelectionControl : UserControl
    {
        /// <summary>
        /// Current menu item from most recent menu click
        /// </summary>
        private IMenuItem _currentItem;


        /// <summary>
        /// Event handler in reference to clicking on one of the menu items
        /// </summary>
        public event RoutedEventHandler? ButtonClick;



        /// <summary>
        /// Constructs a new MenuItemSelectionControl instance
        /// </summary>
        /// <remarks>
        /// FIXME, will possibly make nullable after asking about these in class next week
        /// </remarks>
        public MenuItemSelectionControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles a click of a button for any menu item
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="e">Metadata for the event</param>
        public void MenuClickEvent(object sender, RoutedEventArgs e)
        {
            if(sender is Button b)
            {
                string section = "";
                if(VisualTreeHelper.GetParent(b) is StackPanel s)
                {
                    section = s.Name;
                }

                if(section == "Entrees_1")
                {
                    switch (b.Name)
                    {
                        case "CustomSandButton":
                            _currentItem = new CustomSandwich();
                            break;
                        case "CaliWrapButton":
                            _currentItem = new CaliforniaClubWrap();
                            break;
                        case "ClubSubButton":
                            _currentItem = new ClubSub();
                            break;
                        case "ItalianSubButton":
                            _currentItem = new ItalianSub();
                            break;
                        default:
                            break;
                    }
                }
                else if(section == "Entrees_2")
                {
                    switch (b.Name)
                    {
                        case "MediterrWrapButton":
                            _currentItem = new MediterraneanWrap();
                            break;
                        case "TurkeyCranberryButton":
                            _currentItem = new TurkeyCranberrySandwich();
                            break;
                        case "VeggieSandButton":
                            _currentItem = new VeggieSandwich();
                            break;
                        default:
                            break;
                    }
                }
                else if(section == "Sides")
                {
                    switch (b.Name)
                    {
                        case "AppleButton":
                            _currentItem = new Apple();
                            break;
                        case "ChipsButton":
                            _currentItem = new Chips();
                            break;
                        case "SideSaladButton":
                            _currentItem = new SideSalad();
                            break;
                        case "CookiesButton":
                            _currentItem = new Cookies();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (b.Name)
                    {
                        case "FountainDrinkButton":
                            _currentItem = new FountainDrink();
                            break;
                        case "IcedTeaButton":
                            _currentItem = new IcedTea();
                            break;
                        case "LemonadeButton":
                            _currentItem = new Lemonade();
                            break;
                        default:
                            break;
                    }
                }

                ButtonClick?.Invoke(sender, e);
            }
        }



        /// <summary>
        /// Gets the last menu item sucessfully clicked on
        /// </summary>
        /// <returns>The most recently clicked on IMenuItem</returns>
        public IMenuItem GetMenuItem()
        {
            return _currentItem;
        }






    }
}
