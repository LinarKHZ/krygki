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
    /// Логика взаимодействия для AddHistory.xaml
    /// </summary>
    public partial class AddHistory : Page
    {
        bool edit;
        History newUser;
        public AddHistory(History user, bool Edit)
        {
            edit = Edit;
            newUser = null;
            newUser = user;
            InitializeComponent();
            if (Edit)
            {
                FirstName.Text = user.Id_timetable.ToString();
                CategoryComboBox.SelectedIndex = user.Statusof;
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
                newUser.Id_timetable = Int32.Parse( FirstName.Text);
                newUser.Statusof = CategoryComboBox.SelectedIndex;
            }
            else
            {
                History client = new History();
                client.Id_timetable = Int32.Parse(FirstName.Text);
                client.Datetime = DateTime.Now;
                client.Statusof = CategoryComboBox.SelectedIndex;
                MainWindow.DB.History.Add(client);
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