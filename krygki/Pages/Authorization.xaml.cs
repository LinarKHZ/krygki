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
using krygki.Properties;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace krygki.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        bool Auto;
        public Authorization(bool auto)
        {
            InitializeComponent();
            Auto = auto;
            SaveMe.IsChecked = true;
        }
        int countWar = 0;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Auto)
            {
                if (!string.IsNullOrEmpty(Properties.Settings.Default.login))
                {
                    Logn.Text = Properties.Settings.Default.login.Trim();
                    if (!string.IsNullOrEmpty(Properties.Settings.Default.password))
                    {
                        Pass.Password = Properties.Settings.Default.password.Trim();
                        PassText.Text = Properties.Settings.Default.password.Trim();
                        Enter_Click(sender, null);
                    }
                }
            }
        }
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (Logn.Text != "" && Pass.Password != "")
            {
                User user = MainWindow.DB.User.Where(u => u.Login == Logn.Text).ToList().FirstOrDefault();
                if (user != null)
                {
                    if (user.Status == 1)
                    {
                        if (user.Password == Pass.Password)
                        {
                            MainWindow.userValue = user;
                            DataContext = user;
                            this.NavigationService.Navigate(new Pages.MainMenu());
                            if (SaveMe.IsChecked == true)
                            {
                                Properties.Settings.Default.login = user.Login;
                                Properties.Settings.Default.password = user.Password;
                                Properties.Settings.Default.Save();
                            }
                            else
                            {
                                Properties.Settings.Default.login = "";
                                Properties.Settings.Default.Save();
                            }
                        }
                        else
                        {
                            countWar++;
                            MessageBox.Show("Пароль не правильный!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не активный");
                    }                    
                }
                else MessageBox.Show("Пользователь не найден");
            }
            else MessageBox.Show("Заполните все поля!");
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Enter_Click(sender, e);
                Logn.Focus();
            }
            
        }

        private void Path_Click(object sender, MouseButtonEventArgs e)
        {
            if (PassText.Visibility == Visibility.Visible)
            {
                Pass.Password = PassText.Text;
                PassText.Visibility = Visibility.Hidden;
                Pass.Visibility = Visibility.Visible;
            }
            else
            {
                PassText.Text = Pass.Password;
                PassText.Visibility = Visibility.Visible;
                Pass.Visibility = Visibility.Hidden;
            }
        }

        private void Logn_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Logn.Text == "Логин" && PassText.Text == "Пароль" || PassText.Text == "")
                Logn.Text = "";
            else if (Logn.Text == "Логин")
            {
                Logn.Text = "";
                PassText.Visibility = Visibility.Hidden;
                Pass.Visibility = Visibility.Visible;
            }
        }

        private void Logn_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Logn.Text == "")
                Logn.Text = "Логин";
        }

        private void PassText_GotFocus(object sender, RoutedEventArgs e)
        {
            Pass.Focus();
            PassText.Visibility = Visibility.Hidden;
            Pass.Visibility = Visibility.Visible;
            Pass.Focus();
        }

        private void Pass_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Pass.Password == "")
            {
                Pass.Visibility = Visibility.Hidden;
                PassText.Visibility = Visibility.Visible;
                PassText.Text = "Пароль";
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Обратитесь к администратору");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
