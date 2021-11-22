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
    public partial class AddStudent : Page
    {
        bool edit;
        Student newUser;
        public AddStudent(Student user, bool Edit)
        {
            edit = Edit;
            newUser = null;
            newUser = user;
            InitializeComponent();
            if (Edit)
            {
                FirstName.Text = user.Firstname;
                Name1.Text = user.LastName;
                Patronymic.Text = user.Patronymic;
                Telephone.Text = user.Phone;
                Birthday.SelectedDate = user.Birthday;
                CategoryComboBox.SelectedIndex = user.Status;
            }
            else
            {
                FirstName.Text = Name1.Text = Patronymic.Text = Telephone.Text = "";
                Birthday.SelectedDate = DateTime.ParseExact("1995-01-01", "yyyy-MM-dd",
                                           System.Globalization.CultureInfo.InvariantCulture);
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
                newUser.Firstname = FirstName.Text;
                newUser.LastName = Name1.Text;
                newUser.Patronymic = Patronymic.Text;
                newUser.Birthday = Convert.ToDateTime(Birthday.Text);
                newUser.Phone = Telephone.Text;
                newUser.Status = CategoryComboBox.SelectedIndex;
            }
            else
            {
                Student client = new Student();
                client.Firstname = FirstName.Text;
                client.LastName = Name1.Text;
                client.Patronymic = Patronymic.Text;
                client.Birthday = Convert.ToDateTime(Birthday.Text);
                client.Phone = Telephone.Text;
                client.RegistrationDate = Convert.ToDateTime(DateTime.Today);
                client.Status = CategoryComboBox.SelectedIndex;
                MainWindow.DB.Student.Add(client);
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
