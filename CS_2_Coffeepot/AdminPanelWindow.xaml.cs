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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace CS_2_Coffeepot
{
    /// <summary>
    /// Логика взаимодействия для AdminPanelWindow.xaml
    /// </summary>
    public partial class AdminPanelWindow : Window
    {
        public AdminPanelWindow(List<Drink> drinks)
        {
            InitializeComponent();

            List<string> drinksNames = new List<string>();

            foreach (var i in drinks)
            {
                drinksNames.Add(i.Name);
            }

            DrinksComboBox.ItemsSource = drinksNames;
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            string xmlFilePath = "Drinks.xml";
            XDocument xdoc = XDocument.Load(xmlFilePath);

            var drink = xdoc.Descendants()?.Where(x => x.Value == DrinksComboBox.SelectedItem.ToString())?.Ancestors("Drink");
            var price = drink.Elements("Price").FirstOrDefault();
            if (price != null)
            {
                price.Value = PriceTextBox.Text;
            }

            xdoc.Save(xmlFilePath);
        }
    }
}
