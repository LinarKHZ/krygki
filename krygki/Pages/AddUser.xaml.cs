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
    public partial class AddUser : Page
    {
        bool edit;
        User newUser;
        string Userphoto;
        public AddUser(User user, bool Edit)
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
                Address.Text = user.Email;
                Telephone.Text = user.Phone;
                Birthday.SelectedDate = user.Birthday;
                Login.Text = user.Login;
                Password.Text = user.Password;
                CategoryComboBox.SelectedIndex = user.Status;
                Photo1.Text = user.Photo;
                UserPhoto.Source = new BitmapImage(new Uri(user.Photo));
            }
            else
            {
                FirstName.Text = Name1.Text = Patronymic.Text = Address.Text = Telephone.Text = Login.Text = Password.Text = Photo1.Text = "";
                Birthday.SelectedDate = DateTime.ParseExact("1995-01-01", "yyyy-MM-dd",
                                           System.Globalization.CultureInfo.InvariantCulture);
            }
        }


        private void photoclick_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите картинку";
            openFileDialog.Filter = "Image file (*.png;*.jpg)|*.png;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                UserPhoto.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
            Userphoto = openFileDialog.FileName;
            Photo1.Text = Userphoto;
            Photo1.Opacity = 1;
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
    }
}
