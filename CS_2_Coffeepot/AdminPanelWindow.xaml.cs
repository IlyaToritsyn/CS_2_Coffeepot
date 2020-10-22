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
        List<double> prices;
        public AdminPanelWindow(List<Drink> drinks)
        {
            drinksList = drinks;
            List<string> drinksNames = new List<string>();
            prices = new List<double>();

            foreach (var i in drinks)
            {
                drinksNames.Add(i.Name);
                prices.Add(i.Price);
            }

            InitializeComponent();

            DrinksComboBox.ItemsSource = drinksNames;
        }

        public void ApplyChange()
        {
            if (!int.TryParse(PriceTextBox.Text, out int priceInt) || priceInt <= 0 || priceInt > 1000 || priceInt == drinksList[comboboxItemIndex].Price)
            {
                ChangesAppliedLabel.Visibility = Visibility.Hidden;

                return;
            }

            else
            {
                prices[comboboxItemIndex] = double.Parse(PriceTextBox.Text);
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
            PriceTextBox.Text = prices[DrinksComboBox.SelectedIndex].ToString();
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
            /// <summary>
            /// Для сериализации напитков.
            /// </summary>
            XmlSerializer formatter = new XmlSerializer(typeof(List<Drink>));

            using (FileStream fs = new FileStream("Drinks.xml", FileMode.Truncate))
            {
                formatter.Serialize(fs, drinksList);
            }

            DialogResult = true;
        }
    }
}
