using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace CS_2_Coffeepot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Непосредственно сама кофемашина.
        /// </summary>
        Machine coffepot;

        /// <summary>
        /// Индикаторы уровня сахара в интерфейсе.
        /// </summary>
        List<Rectangle> sugarPoints;

        /// <summary>
        /// Напитки.
        /// </summary>
        List<Drink> drinks;

        /// <summary>
        /// Картинки напитков (условные кнопки автомата).
        /// </summary>
        List<Image> drinkImages;

        /// <summary>
        /// Подписи к кнопкам автомата.
        /// </summary>
        List<TextBlock> drinkTextBlocks;

        /// <summary>
        /// Все кнопки основной формы.
        /// </summary>
        List<Button> buttons;

        /// <summary>
        /// Для сериализации напитков.
        /// </summary>
        XmlSerializer formatter = new XmlSerializer(typeof(List<Drink>));

        /// <summary>
        /// Десериализация напитков.
        /// </summary>
        private void DeserializeDrinks()
        {
            using (FileStream fs = new FileStream("Drinks.xml", FileMode.Open))
            {
                drinks = (List<Drink>)formatter.Deserialize(fs);
            }
        }

        /// <summary>
        /// Обновить подписи к кнопкам.
        /// </summary>
        private void UpdateTextBlockTexts()
        {
            for (int i = 0; i < drinkTextBlocks.Count; i++)
            {
                drinkTextBlocks[i].Text = drinks[i].Price + " - " + drinks[i].Name;
            }
        }

        /// <summary>
        /// Отобразить уровень сахара в интерфейсе через цветные индикаторы.
        /// </summary>
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

        /// <summary>
        /// Отобразить в интерфейсе: какие напитки доступны, а какие - нет.
        /// </summary>
        private void UpdateDrinkAccess()
        {
            for (int i = 0; i < drinkImages.Count; i++)
            {
                if (drinks[i].Price > coffepot.Credit)
                {
                    drinkImages[i].IsEnabled = false;
                    drinkImages[i].Opacity = 0.5;
                }

                else
                {
                    drinkImages[i].IsEnabled = true;
                    drinkImages[i].Opacity = 1;
                }
            }
        }

        /// <summary>
        /// Отразить изменение уровня сахара в интерфейсе (индикаторы + кнопка отмены).
        /// </summary>
        private void DisplaySugarChange()
        {
            CancelButton.IsEnabled = coffepot.IsResetAvaible;

            UpdateSugarLevel();
        }

        /// <summary>
        /// Если нажата кнопка изменения уровня сахара (+ или -).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeSugarButton_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag.ToString() == "+")
            {
                coffepot.IncreaseSugar();
            }

            else if (((Button)sender).Tag.ToString() == "-")
            {
                coffepot.DecreaseSugar();
            }

            DisplaySugarChange();
        }

        /// <summary>
        /// Когда наш любимый пользователь выбрал себе напиток.
        /// </summary>
        /// <param name="drink">Выбранный напиток</param>
        private void AnimateCooking(Drink drink)
        {
            coffepot.Credit -= drink.Price; //Вычитаем цену напитка из внесённых денег. В дальнейшем сможем определить, выдавать ли сдачу.

            Reset();

            foreach (var i in buttons)
            {
                i.IsEnabled = false;
            }
            
            OutputWindowImage.IsEnabled = false;
            OrderStatus.Content = "НАПИТОК ГОТОВИТСЯ...";

            var animation = Task.Factory.StartNew(() =>
            {
                for (int i = 1; i <= 5; i++)
                {
                    Thread.Sleep(1000);

                    Dispatcher.Invoke(new Action(() =>
                    {
                        OutputWindowImage.Source = new BitmapImage(new Uri("pack://application:,,,/img/Coffee_cup_" + i + ".jpg", UriKind.RelativeOrAbsolute));
                    }));
                }

                //Выводится только в конце анимации.
                Dispatcher.Invoke(new Action(() =>
                {
                    OrderStatus.Content = "ПРИЯТНОГО АППЕТИТА!";
                    OutputWindowImage.IsEnabled = true;
                }));
            });
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
            drinkImages = new List<Image>()
            {
                Drink1Image,
                Drink2Image,
                Drink3Image,
                Drink4Image,
                Drink5Image,
                Drink6Image,
                Drink7Image,
                Drink8Image
            };
            buttons = new List<Button>()
            {
                AdminPanelButton,
                Deposit_1,
                Deposit_2,
                Deposit_5,
                Deposit_10,
                MinusSugar,
                PlusSugar,
                CancelButton
            };
            drinkTextBlocks = new List<TextBlock>()
            {
                Drink1Label,
                Drink2Label,
                Drink3Label,
                Drink4Label,
                Drink5Label,
                Drink6Label,
                Drink7Label,
                Drink8Label
            };

            UpdateTextBlockTexts();
            UpdateSugarLevel();
        }

        private void ProcessChange()
        {
            if (coffepot.Credit != 0)
            {
                coffepot.GiveChange();

                ChangeWindowImage.Source = new BitmapImage(new Uri("pack://application:,,,/img/Coin.jpg", UriKind.RelativeOrAbsolute));
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
            OutputWindowImage.Source = new BitmapImage(new Uri("pack://application:,,,/img/Nothing.jpg", UriKind.RelativeOrAbsolute));
            OrderStatus.Content = "ВНЕСИТЕ ДЕНЬГИ";
            Deposit_1.IsEnabled = true;
            Deposit_2.IsEnabled = true;
            Deposit_5.IsEnabled = true;
            Deposit_10.IsEnabled = true;
            MinusSugar.IsEnabled = true;
            PlusSugar.IsEnabled = true;
            AdminPanelButton.IsEnabled = true;
        }

        private void ChangeWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ChangeWindowImage.Source != null)
            {
                ChangeWindowImage.Source = null;
            }
        }

        private void DisplayDeposit()
        {
            CreditValue.Content = coffepot.Credit;

            UpdateDrinkAccess();

            CancelButton.IsEnabled = true;
        }

        private void Deposit_Click(object sender, RoutedEventArgs e)
        {
            coffepot.Deposit(Convert.ToInt32(((Button)sender).Tag));
            DisplayDeposit();
        }

        private void AdminPanelButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordWindow passwordWindow = new PasswordWindow();

            if (passwordWindow.ShowDialog() == true)
            {
                AdminPanelWindow adminPanelWindow = new AdminPanelWindow(drinks);

                if (adminPanelWindow.ShowDialog() == false)
                {
                    adminPanelWindow.ApplyChange();
                }

                DeserializeDrinks();
                UpdateDrinkAccess();
                UpdateTextBlockTexts();
            }
        }

        private void DrinkImage_Click(object sender, MouseButtonEventArgs e)
        {
            AnimateCooking(drinks[Convert.ToInt32(((Image)sender).Tag)]);
        }
    }
}
