using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace krygki.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddClub : Page
    {
        bool edit;
        Club newUser;
        public AddClub(Club user, bool Edit)
        {
            edit = Edit;
            newUser = null;
            newUser = user;
            InitializeComponent();
            if (Edit)
            {
                FirstName.Text = user.Name;
                CategoryComboBox.SelectedIndex = user.Status;
            }
            else
            {
                FirstName.Text = "";
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (edit)
            {
                newUser.Name = FirstName.Text;
                newUser.Status = CategoryComboBox.SelectedIndex;
            }
            else
            {
                Club client = new Club();
                client.Name = FirstName.Text;
                client.Status = CategoryComboBox.SelectedIndex;
                MainWindow.DB.Club.Add(client);
            }
            try
            {
                MainWindow.DB.SaveChanges();
            }
            catch (Exception ex)
            {
                var innexc = ex.InnerException;
                while (innexc != null)
                {
                    MessageBox.Show("Внутренняя Ошибка" + innexc.HResult + "\n" + innexc.Message);
                    innexc = innexc.InnerException;
                }
            }
            NavigationService.Navigate(new MainMenu());


        }
    }
}
