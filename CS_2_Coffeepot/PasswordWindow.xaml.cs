using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            string passwordFilePath = "_.txt";

            if (File.Exists(passwordFilePath))
            {
                Regex regex = new Regex("^([a-zA-Z0-9]{5,15})$");
                string filePassword = File.ReadAllText(passwordFilePath);

                if (regex.IsMatch(filePassword))
                {
                    if (filePassword == Password)
                    {
                        DialogResult = true;

                        return;
                    }
                    
                    else
                    {
                        PasswordPasswordBox.Background = Brushes.Pink;
                        InvalidPasswordLabel.Visibility = Visibility.Visible;

                        return;
                    }
                }
            }

            if (Password == "admin")
            {
                DialogResult = true;
            }

            else
            {
                PasswordPasswordBox.Background = Brushes.Pink;
                InvalidPasswordLabel.Visibility = Visibility.Visible;
            }
        }

        private void PasswordPasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordPasswordBox.Background = Brushes.White;
            InvalidPasswordLabel.Visibility = Visibility.Hidden;
        }

        private void PasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Password.Length > 15)
            {
                PasswordPasswordBox.Password = Password.Substring(0, Password.Length - 1);
            }
        }
    }
}
