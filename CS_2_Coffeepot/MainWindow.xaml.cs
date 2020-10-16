using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace CS_2_Coffeepot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int test = 0;
        Machine coffepot;
        List<Rectangle> sugarPoints;
        public List<Drink> drinks = null;
        List<Button> drinkButtons;
        XmlSerializer formatter = new XmlSerializer(typeof(List<Drink>));

        private void DeserializeDrinks()
        {
            using (FileStream fs = new FileStream("Drinks.xml", FileMode.Open))
            {
                drinks = (List<Drink>)formatter.Deserialize(fs);
            }
        }

        private void UpdateButtonsTexts()
        {
            for (int i = 0; i < drinkButtons.Count; i++)
            {
                drinkButtons[i].Content = drinks[i].Price + " - " + drinks[i].Name;
            }
        }

        public MainWindow()
        {
            if (!File.Exists("Drinks.xml"))
            {
                drinks = new List<Drink>() {
                new Americano(),
                new Black(),
                new Cappuccino(),
                new ConPanna(),
                new CreamAndSugar(),
                new Doppio(),
                new DryCappuccino(),
                new Espresso()
            };

                using (FileStream fs = new FileStream("Drinks.xml", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, drinks);
                }
            }

            else
            {
                DeserializeDrinks();
            }

            coffepot = new Machine(drinks);

            InitializeComponent();

            sugarPoints = new List<Rectangle>() { Point_1, Point_2, Point_3, Point_4, Point_5 };
            drinkButtons = new List<Button>() {
                Drink_1_Button,
                Drink_2_Button,
                Drink_3_Button,
                Drink_4_Button,
                Drink_5_Button,
                Drink_6_Button,
                Drink_7_Button,
                Drink_8_Button
           };

            UpdateButtonsTexts();
            UpdateSugarLevel();
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

        private void DisplaySugarChange()
        {
            CancelButton.IsEnabled = coffepot.IsResetAvaible;

            UpdateSugarLevel();
        }

        private void MinusSugar_Click(object sender, RoutedEventArgs e)
        {
            coffepot.DecreaseSugar();
            DisplaySugarChange();
        }

        private void PlusSugar_Click(object sender, RoutedEventArgs e)
        {
            coffepot.IncreaseSugar();
            DisplaySugarChange();
        }

        private void AnimateCooking(Drink drink)
        {
            coffepot.Credit -= drink.Price;

            Reset();

            OrderStatus.Content = "НАПИТОК ГОТОВИТСЯ...";

            var animation = Task.Factory.StartNew(() =>
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    OutputWindow.Background = Brushes.White;
                }));
                
                Thread.Sleep(1000);

                this.Dispatcher.Invoke(new Action(() =>
                {
                    OutputWindow.Background = Brushes.Red;
                }));

                Thread.Sleep(1000);

                this.Dispatcher.Invoke(new Action(() =>
                {
                    OutputWindow.Background = Brushes.Yellow;
                }));

                Thread.Sleep(1000);

                this.Dispatcher.Invoke(new Action(() =>
                {
                    OutputWindow.Background = Brushes.Green;
                    OrderStatus.Content = "ПРИЯТНОГО АППЕТИТА!";
                }));
            });
        }

        private void ProcessChange()
        {
            if (coffepot.Credit != 0)
            {
                coffepot.GiveChange();

                ChangeWindow.Background = Brushes.Red;
            }
        }

        private void Reset()
        {
            ProcessChange();

            coffepot.SugarLevel = coffepot.SugarLevelDefault;
            CreditValue.Content = coffepot.Credit;
            OrderStatus.Content = "ВНЕСИТЕ ДЕНЬГИ";

            UpdateSugarLevel();
            UpdateDrinkAccess();

            coffepot.IsResetAvaible = false;
            CancelButton.IsEnabled = false;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (!coffepot.IsResetAvaible)
            {
                return;
            }

            else
            {
                Reset();
            }
        }

        private void OutputWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (OutputWindow.Background != Brushes.White)
            {
                OutputWindow.Background = Brushes.White;
                OrderStatus.Content = "ВНЕСИТЕ ДЕНЬГИ";
            }
        }

        private void ChangeWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ChangeWindow.Background != Brushes.White)
            {
                ChangeWindow.Background = Brushes.White;
            }
        }

        private void DisplayDeposit()
        {
            CreditValue.Content = coffepot.Credit;

            UpdateDrinkAccess();

            CancelButton.IsEnabled = true;
        }

        private void Deposit_1_Click(object sender, RoutedEventArgs e)
        {
            coffepot.Deposit(1);
            DisplayDeposit();
        }

        private void Deposit_2_Click(object sender, RoutedEventArgs e)
        {
            coffepot.Deposit(2);
            DisplayDeposit();
        }

        private void Deposit_5_Click(object sender, RoutedEventArgs e)
        {
            coffepot.Deposit(5);
            DisplayDeposit();
        }

        private void Deposit_10_Click(object sender, RoutedEventArgs e)
        {
            coffepot.Deposit(10);
            DisplayDeposit();
        }

        private void Drink_1_Button_Click(object sender, RoutedEventArgs e)
        {
            AnimateCooking(drinks[0]);
        }

        private void Drink_2_Button_Click(object sender, RoutedEventArgs e)
        {
            AnimateCooking(drinks[1]);
        }

        private void Drink_3_Button_Click(object sender, RoutedEventArgs e)
        {
            AnimateCooking(drinks[2]);
        }

        private void Drink_4_Button_Click(object sender, RoutedEventArgs e)
        {
            AnimateCooking(drinks[3]);
        }

        private void Drink_5_Button_Click(object sender, RoutedEventArgs e)
        {
            AnimateCooking(drinks[4]);
        }

        private void Drink_6_Button_Click(object sender, RoutedEventArgs e)
        {
            AnimateCooking(drinks[5]);
        }

        private void Drink_7_Button_Click(object sender, RoutedEventArgs e)
        {
            AnimateCooking(drinks[6]);
        }

        private void Drink_8_Button_Click(object sender, RoutedEventArgs e)
        {
            AnimateCooking(drinks[7]);
        }

        private void AdminPanelButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordWindow passwordWindow = new PasswordWindow();

            if (passwordWindow.ShowDialog() == true)
            {
                if (passwordWindow.Password == "12345678")
                {
                    AdminPanelWindow adminPanelWindow = new AdminPanelWindow(drinks);

                    adminPanelWindow.ShowDialog();

                    DeserializeDrinks();
                    UpdateButtonsTexts();
                }

                else
                    MessageBox.Show("Неверный пароль");
            }
            else
            {
                MessageBox.Show("Авторизация не пройдена");
            }
        }
    }
}
