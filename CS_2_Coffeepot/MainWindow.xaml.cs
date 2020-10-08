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
            List<Drink> drinks = new List<Drink>() {
                americano,
                black,
                cappuccino,
                conPanna,
                creamAndSugar,
                doppio,
                dryCappuccino,
                espresso };
            coffepot = new Machine(drinks);

            InitializeComponent();

            sugarPoints = new List<Rectangle>() { Point_1, Point_2, Point_3, Point_4, Point_5 };
        }

        private void Give_10_Click(object sender, RoutedEventArgs e)
        {
            coffepot.Credit += 10;
            CreditValue.Content = coffepot.Credit;
        }

        private void Give_50_Click(object sender, RoutedEventArgs e)
        {
            coffepot.Credit += 50;
            CreditValue.Content = coffepot.Credit;
        }

        private void Give_100_Click(object sender, RoutedEventArgs e)
        {
            coffepot.Credit += 100;
            CreditValue.Content = coffepot.Credit;
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

        private void AmericanoButton_Click(object sender, RoutedEventArgs e)
        {
            OutputWindow.Background = Brushes.Red;
            OrderStatus.Content = "НАПИТОК ГОТОВИТСЯ...";
            americano.Cook();
        }
    }
}
