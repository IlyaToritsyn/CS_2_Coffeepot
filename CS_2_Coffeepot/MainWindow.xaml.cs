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

namespace CS_2_Coffeepot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Machine coffepot;
        List<Rectangle> sugarPoints;
        List<Drink> drinks;
        List<Button> drinkButtons;
        Americano americano;
        Black black;
        Cappuccino cappuccino;
        ConPanna conPanna;
        CreamAndSugar creamAndSugar;
        Doppio doppio;
        DryCappuccino dryCappuccino;
        Espresso espresso;
        public MainWindow()
        {
            americano = new Americano();
            black = new Black();
            cappuccino = new Cappuccino();
            conPanna = new ConPanna();
            creamAndSugar = new CreamAndSugar();
            doppio = new Doppio();
            dryCappuccino = new DryCappuccino();
            espresso = new Espresso();
            drinks = new List<Drink>() {
                americano,
                black,
                cappuccino,
                conPanna,
                creamAndSugar,
                doppio,
                dryCappuccino,
                espresso
            };
            coffepot = new Machine(drinks);

            InitializeComponent();

            sugarPoints = new List<Rectangle>() { Point_1, Point_2, Point_3, Point_4, Point_5 };
            drinkButtons = new List<Button>() {
                AmericanoButton,
                BlackButton,
                CappuccinoButton,
                ConPannaButton,
                CreamAndSugarButton,
                DoppioButton,
                DryCappuccinoButton,
                EspressoButton 
           };
        }

        private void Give_10_Click(object sender, RoutedEventArgs e)
        {
            coffepot.Credit += 10;
            CreditValue.Content = coffepot.Credit;
            UpdateDrinkAccess();
        }

        private void Give_50_Click(object sender, RoutedEventArgs e)
        {
            coffepot.Credit += 50;
            CreditValue.Content = coffepot.Credit;
            UpdateDrinkAccess();
        }

        private void Give_100_Click(object sender, RoutedEventArgs e)
        {
            coffepot.Credit += 100;
            CreditValue.Content = coffepot.Credit;
            UpdateDrinkAccess();
        }

        private void UpdateSugarLevel()
        {
            for (int i = 0; i < sugarPoints.Count; i++)
            {
                if (i < coffepot.SugarLevel)
                {
                    sugarPoints[i].Fill = Brushes.Orange;
                }

                else
                {
                    sugarPoints[i].Fill = Brushes.White;
                }
            }
        }

        private void UpdateDrinkAccess()
        {
            for (int i = 0; i < drinkButtons.Count; i++)
            {
                if (drinks[i].Price > coffepot.Credit)
                {
                    drinkButtons[i].IsEnabled = false;
                }

                else
                {
                    drinkButtons[i].IsEnabled = true;
                }
            }
        }

        private void MinusSugar_Click(object sender, RoutedEventArgs e)
        {
            coffepot.SugarLevel--;
            UpdateSugarLevel();
        }

        private void PlusSugar_Click(object sender, RoutedEventArgs e)
        {
            coffepot.SugarLevel++;
            UpdateSugarLevel();
        }

        private void AnimateCooking()
        {
            OrderStatus.Content = "НАПИТОК ГОТОВИТСЯ...";
            OrderStatus.Content = "ПРИЯТНОГО АППЕТИТА!";
        }
        private void AmericanoButton_Click(object sender, RoutedEventArgs e)
        {
            OutputWindow.Background = Brushes.Red;
            AnimateCooking();
        }

        private void BlackButton_Click(object sender, RoutedEventArgs e)
        {
            OutputWindow.Background = Brushes.Black;
            AnimateCooking();
        }

        private void CappuccinoButton_Click(object sender, RoutedEventArgs e)
        {
            OutputWindow.Background = Brushes.Orange;
            AnimateCooking();
        }

        private void ConPannaButton_Click(object sender, RoutedEventArgs e)
        {
            OutputWindow.Background = Brushes.Yellow;
            AnimateCooking();
        }

        private void CreamAndSugarButton_Click(object sender, RoutedEventArgs e)
        {
            OutputWindow.Background = Brushes.Green;
            AnimateCooking();
        }

        private void DoppioButton_Click(object sender, RoutedEventArgs e)
        {
            OutputWindow.Background = Brushes.Cyan;
            AnimateCooking();
        }

        private void DryCappuccinoButton_Click(object sender, RoutedEventArgs e)
        {
            OutputWindow.Background = Brushes.Blue;
            AnimateCooking();
        }

        private void EspressoButton_Click(object sender, RoutedEventArgs e)
        {
            OutputWindow.Background = Brushes.Violet;
            AnimateCooking();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (!coffepot.IsResetAvaible)
            {
                return;
            }

            else
            {
                coffepot.Credit = 0;
                coffepot.SugarLevel = 3;
                CreditValue.Content = "0";
                OrderStatus.Content = "ВНЕСИТЕ ДЕНЬГИ";
                UpdateSugarLevel();
                UpdateDrinkAccess();
            }
        }
    }
}
