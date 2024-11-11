using SubHero.Data.Entrees;
using SubHero.Data.Drinks;
using SubHero.Data.Sides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SubHero.Data
{
    /// <summary>
    /// Represents a combo meal of an Entree, Drink, and Side
    /// </summary>
    public class Combo : IMenuItem, INotifyPropertyChanged
    {
        /// <summary>
        /// private backing field for the SandwichChoice property
        /// </summary>
        private Entree _sandwichChoice = new CustomSandwich();

        /// <summary>
        /// private backing field for the SideChoice property
        /// </summary>
        private Side _sideChoice = new Chips();

        /// <summary>
        /// private backing field for the DrinkChoice property
        /// </summary>
        private Drink _drinkChoice = new FountainDrink();

        /// <summary>
        /// Event handler for INotifyCollectionChanged
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;




        /// <summary>
        /// Name of this Combo instance
        /// </summary>
        public string Name { get; } = "Combo";

        /// <summary>
        /// Description of this Combo instance
        /// </summary>
        public string Description { get; } = "A sandwich plus your choice of side and drink";

        /// <summary>
        /// The Entree choice for this Combo instance
        /// </summary>
        public Entree SandwichChoice
        {
            get => _sandwichChoice;
            set
            {
                _sandwichChoice.PropertyChanged -= ComboPropertyChanged;
                _sandwichChoice = value;
                _sandwichChoice.PropertyChanged += ComboPropertyChanged;
                //_sandwichChoice.PartOfACombo = true;
                ComboPropertyChanged(this, new PropertyChangedEventArgs(nameof(SandwichChoice)));
                
                OnPropertyChanged(nameof(SandwichChoice));
                OnPropertyChanged(nameof(EntreeOptions));
                

            }
        }

        /// <summary>
        /// The Drink choice for this Combo instance
        /// </summary>
        public Drink DrinkChoice
        {
            get => _drinkChoice;
            set
            {
                _drinkChoice.PropertyChanged -= ComboPropertyChanged;
                _drinkChoice = value;
                _drinkChoice.PropertyChanged += ComboPropertyChanged;
                ComboPropertyChanged(this, new PropertyChangedEventArgs(nameof(DrinkChoice)));
                OnPropertyChanged(nameof(DrinkChoice));
                OnPropertyChanged(nameof(DrinkOptions));
            }
        }

        /// <summary>
        /// The Side choice for this Combo instance
        /// </summary>
        public Side SideChoice
        {
            get => _sideChoice;
            set
            {
                _sideChoice.PropertyChanged -= ComboPropertyChanged;
                _sideChoice = value;
                _sideChoice.PropertyChanged += ComboPropertyChanged;
                ComboPropertyChanged(this, new PropertyChangedEventArgs(nameof(SideChoice)));
                OnPropertyChanged(nameof(SideChoice));
                OnPropertyChanged(nameof(SideOptions));
                
                
            }
        }

        /// <summary>
        /// Price of this Combo instance
        /// </summary>
        public decimal Price
        {
            get
            {
                return 0.80m * (SandwichChoice.Price + DrinkChoice.Price + SideChoice.Price);
            }
        }

        /// <summary>
        /// Total Calories of this Combo instance
        /// </summary>
        public uint Calories
        {
            get
            {
                return SandwichChoice.Calories + DrinkChoice.Calories + SideChoice.Calories;
            }
        }

        /// <summary>
        /// Preparation information for this Combo instance
        /// </summary>
        public IEnumerable<string> PreparationInformation
        {
            get
            {
                List<string> instructions = new List<string>();

                instructions.Add($"Sandwich: {SandwichChoice.Name}");

                foreach(string s in SandwichChoice.PreparationInformation)
                {
                    instructions.Add("\t" + s);
                }

                instructions.Add($"Side: {SideChoice.Name}");

                foreach (string s in SideChoice.PreparationInformation)
                {
                    instructions.Add("\t" + s);
                }

                instructions.Add($"Drink: {DrinkChoice.Name}");

                foreach (string s in DrinkChoice.PreparationInformation)
                {
                    instructions.Add("\t" + s);
                }

                return instructions;

            }
        }

        /// <summary>
        /// Keeps track of the current Entree options available for the user to choose from - updates dependant on current sandwichChoice
        /// </summary>
        public IEnumerable<Entree> EntreeOptions
        {
            get
            {
                List<Entree> tempList = new List<Entree>();
                tempList.Add(SandwichChoice);
                if (!(SandwichChoice is ClubSub)) tempList.Add(new ClubSub() { PartOfACombo = true});
                if (!(SandwichChoice is CaliforniaClubWrap)) tempList.Add(new CaliforniaClubWrap() { PartOfACombo = true });
                if (!(SandwichChoice is CustomSandwich)) tempList.Add(new CustomSandwich() { PartOfACombo = true });
                if (!(SandwichChoice is ItalianSub)) tempList.Add(new ItalianSub() { PartOfACombo = true });
                if (!(SandwichChoice is MediterraneanWrap)) tempList.Add(new MediterraneanWrap() { PartOfACombo = true });
                if (!(SandwichChoice is TurkeyCranberrySandwich)) tempList.Add(new TurkeyCranberrySandwich() { PartOfACombo = true });
                if (!(SandwichChoice is VeggieSandwich)) tempList.Add(new VeggieSandwich() { PartOfACombo = true });
                
                return tempList;
            }
        }

        /// <summary>
        /// Keeps track of the current Side options available for the user to choose from - updates dependant on current SideChoice
        /// </summary>
        public IEnumerable<Side> SideOptions
        {
            get
            {
                List<Side> tempList = new List<Side>();
                tempList.Add(SideChoice);
                if (!(SideChoice is Apple)) tempList.Add(new Apple());
                if (!(SideChoice is Chips)) tempList.Add(new Chips());
                if (!(SideChoice is Cookies)) tempList.Add(new Cookies());
                if (!(SideChoice is SideSalad)) tempList.Add(new SideSalad());
                return tempList;
            }
        }

        /// <summary>
        /// Keeps track of the current Drink options available for the user to choose from - updates dependant on current DrinkChoice
        /// </summary>
        public IEnumerable<Drink> DrinkOptions
        {
            get
            {
                List<Drink> tempList = new List<Drink>();
                tempList.Add(DrinkChoice);
                if (!(DrinkChoice is FountainDrink)) tempList.Add(new FountainDrink());
                if (!(DrinkChoice is IcedTea)) tempList.Add(new IcedTea());
                if (!(DrinkChoice is Lemonade)) tempList.Add(new Lemonade());
                return tempList;

            }
        }

        /// <summary>
        /// Triggers a property update on order-related items to reflect in-combo item changes
        /// </summary>
        /// <param name="sender">Sender of this event</param>
        /// <param name="e">MetaData for this event</param>
        public void ComboPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Price)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Calories)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PreparationInformation)));
            PropertyChanged?.Invoke(this, e);

        }

        /// <summary>
        /// Invokes property changed event
        /// </summary>
        /// <param name="propertyName">The name of the property that has been updated</param>
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(propertyName)));
        }



    }
       

}

