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

namespace CS_2_Coffeepot
{
    /// <summary>
    /// Логика взаимодействия для PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        public string Password
        {
            get { return PasswordPasswordBox.Password; }
        }

        public PasswordWindow()
        {
            InitializeComponent();

            PasswordPasswordBox.Focus();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (Password == "12345678")
            {
                this.DialogResult = true;
            }

            else
            {
                PasswordPasswordBox.Background = Brushes.Red;
                InvalidPasswordLabel.Visibility = Visibility.Visible;
            }
        }

        private void PasswordPasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordPasswordBox.Background = Brushes.White;
            InvalidPasswordLabel.Visibility = Visibility.Hidden;
        }
    }
}
