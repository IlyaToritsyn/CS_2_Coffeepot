using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CS_2_Coffeepot
{
    /// <summary>
    /// Логика взаимодействия для AdminPanelWindow.xaml
    /// </summary>
    public partial class AdminPanelWindow : Window
    {
        int comboboxItemIndex = 0;
        List<Drink> drinksList;

        public AdminPanelWindow(List<Drink> drinks)
        {
            InitializeComponent();
            drinksList = drinks;
            List<string> drinksNames = new List<string>();

            foreach (var i in drinksList)
            {
                drinksNames.Add(i.Name);
            }

            DrinksComboBox.ItemsSource = drinksNames;
        }

        public void ApplyChange()
        {
            if (!int.TryParse(PriceTextBox.Text, out int priceInt) || priceInt <= 0 || priceInt > 1000 || priceInt == drinksList[comboboxItemIndex].Price)
            {
                return;
            }

            else
            {
                string xmlFilePath = "Drinks.xml";
                XDocument xdoc = XDocument.Load(xmlFilePath);

                var drink = xdoc.Descendants()?.Where(x => x.Value == DrinksComboBox.Items[comboboxItemIndex].ToString())?.Ancestors("Drink");
                var price = drink.Elements("Price").FirstOrDefault();
                if (price != null)
                {
                    price.Value = PriceTextBox.Text;
                }

                xdoc.Save(xmlFilePath);

                ChangesAppliedLabel.Visibility = Visibility.Visible;
            }
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyChange();
        }

        private void PriceTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PriceTextBox.Background = Brushes.White;
            ChangesAppliedLabel.Visibility = Visibility.Hidden;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyChange();

            DialogResult = true;
        }

        private void DrinksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyChange();

            comboboxItemIndex = DrinksComboBox.SelectedIndex;
            //ХОЧУ, ЧТОБЫ ПРИ ВЫБОРЕ В КОМБОБОКСЕ НАПИТКА В ТЕКСТБОКСЕ С ЦЕНОЙ ПО УМОЛЧАНИЮ БЫЛА ТЕКУЩАЯ ЦЕНА СООТВЕТСВУЮЩЕГО НАПИТКА
            PriceTextBox.Text = drinksList[DrinksComboBox.SelectedIndex].Price.ToString();
        }

        private void PriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (!int.TryParse(textBox.Text, out int priceInt) || priceInt <= 0 || priceInt > 1000)
            {
                textBox.Background = Brushes.Pink;
            }

            else
            {
                textBox.Background = Brushes.White;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
