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

namespace krygki.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
            User user = MainWindow.userValue;
            if (user.Photo != null)
            {
                UserPhoto.Source = new BitmapImage(new Uri(user.Photo));
            }
            FirstName.Text = user.Firstname;
            Name1.Text = user.LastName;
            Patronymic.Text = user.Patronymic;
            if (user.Id_role != 1)
            {
                AdminText.Visibility = Visibility.Hidden;
                //Click_4.Visibility = Visibility.Hidden;
                //Click_5.Visibility = Visibility.Hidden;
            }
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            Title = DateTime.Now.ToString("HH:mm:ss");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Click_1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Tabletime());
            //asdasd
        }

        private void Click_3_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Table());
        }

        private void Click_2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TableStudent());
        }

        private void Click_4_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Authorization());
        }

        private void Click_5_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddClub(null, false));
        }

        private void Click_6_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddStudent(null, false));

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Authorization(false));
            Properties.Settings.Default.login = "";
            Properties.Settings.Default.password = "";
            Properties.Settings.Default.Save();
        }

        private void Click_7_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUser(null, false));

        }

        private void Click_8_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddTimetable(null, false));
        }
    }
}
