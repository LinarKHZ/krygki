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
    /// Логика взаимодействия для AddTimetable.xaml
    /// </summary>
    public partial class AddTimetable : Page
    {
        bool edit;
        Timetable newTimetable;
        public AddTimetable(Timetable timetable, bool Edit)
        {
            edit = Edit;
            newTimetable = null;
            newTimetable = timetable;
            InitializeComponent();
            WeeK_Combo.ItemsSource = MainWindow.DB.Week.ToList();
            Club_Combo.ItemsSource = MainWindow.DB.Club.ToList();
            User_Combo.ItemsSource = MainWindow.DB.User.ToList();
            if (Edit)
            {
                WeeK_Combo.SelectedIndex = timetable.Week-1;
                Club_Combo.SelectedIndex = timetable.Id_club-1;
                User_Combo.SelectedIndex = timetable.Id_user-1;
                Time_Start.Text = timetable.Time_s.ToString();
                Time_End.Text = timetable.Time_e.ToString();
                cabinet.Text = timetable.cabinet;
                CategoryComboBox.SelectedIndex = timetable.Status;
            }
            else
            {
                cabinet.Text = Time_Start.Text = Time_End.Text = "";
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
                newTimetable.Week = WeeK_Combo.SelectedIndex+1;
                newTimetable.Time_s = TimeSpan.Parse(Time_Start.Text);
                newTimetable.Time_e = TimeSpan.Parse(Time_End.Text);
                newTimetable.Id_club = Club_Combo.SelectedIndex + 1;
                newTimetable.Id_user = User_Combo.SelectedIndex + 1;
                newTimetable.cabinet = cabinet.Text;
                newTimetable.Status = CategoryComboBox.SelectedIndex;
            }
            else
            {
                Timetable timetable = new Timetable();
                timetable.Week = WeeK_Combo.SelectedIndex + 1;
                timetable.Time_s = TimeSpan.Parse(Time_Start.Text);
                timetable.Time_e = TimeSpan.Parse(Time_End.Text);
                timetable.Id_club = Club_Combo.SelectedIndex + 1;
                timetable.Id_user = User_Combo.SelectedIndex + 1;
                timetable.cabinet = cabinet.Text;
                timetable.Status = CategoryComboBox.SelectedIndex;
                MainWindow.DB.Timetable.Add(timetable);
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
