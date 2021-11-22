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
            if (Edit)
            {
                WeeK_Combo
                FirstName.Text = timetable.Week;
                Name1.Text = timetable.LastName;
                Patronymic.Text = timetable.Patronymic;
                Address.Text = timetable.Email;
                Telephone.Text = timetable.Phone;
                Birthday.SelectedDate = timetable.Birthday;
                CategoryComboBox.SelectedIndex = timetable.Status;
            }
            else
            {
                FirstName.Text = Name1.Text = Patronymic.Text = Address.Text = Telephone.Text = Login.Text = Password.Text = Photo1.Text = "";
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

            if (Login.Text != "" && Password.Text != "")
            {
                Regex myReg = new Regex(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&]).{6,16}$");
                if (myReg.IsMatch(Password.Text) | edit)
                {

                    if (MainWindow.DB.User.Where(x => x.Login == Login.Text).ToList().Count == 0 | edit)
                    {
                        if (edit)
                        {
                            newUser.Firstname = FirstName.Text;
                            newUser.LastName = Name1.Text;
                            newUser.Patronymic = Patronymic.Text;
                            newUser.Birthday = Convert.ToDateTime(Birthday.Text);
                            newUser.Email = Address.Text;
                            newUser.Phone = Telephone.Text;
                            newUser.Login = Login.Text;
                            newUser.Password = Password.Text;
                            newUser.Photo = Photo1.Text;
                            newUser.Status = CategoryComboBox.SelectedIndex;
                        }
                        else
                        {
                            User client = new User();
                            client.Firstname = FirstName.Text;
                            client.LastName = Name1.Text;
                            client.Patronymic = Patronymic.Text;
                            client.Birthday = Convert.ToDateTime(Birthday.Text);
                            client.Email = Address.Text;
                            client.Phone = Telephone.Text;
                            client.RegistrationDate = Convert.ToDateTime(DateTime.Today);
                            client.Login = Login.Text;
                            client.Password = Password.Text;
                            client.Status = CategoryComboBox.SelectedIndex;
                            client.Id_role = 2;
                            client.Photo = Photo1.Text;
                            MainWindow.DB.User.Add(client);
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
                    else
                    {

                        MessageBox.Show("Такой Логин уже существует");
                    }
                }
                else MessageBox.Show("Неверный формат");
            }
        }

        private void Telephone_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
