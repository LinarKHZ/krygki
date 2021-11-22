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
    /// Логика взаимодействия для TableStudent.xaml
    /// </summary>
    public partial class TableStudent : Page
    {
        List<Student> Clients;
        public TableStudent()
        {
            InitializeComponent();
            TableL.ItemsSource = MainWindow.DB.Student.OrderBy(i => i.Id).ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Clients = MainWindow.DB.Student.OrderBy(p => p.LastName).OrderBy(p => p.Firstname).ToList();
                MainWindow.DB.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                TableL.ItemsSource = Clients;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenu());
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddStudent(null, false));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var deleteProduct = TableL.SelectedItems.Cast<Student>().ToList();
            if (TableL.SelectedItem as Student != null)
            {
                if (MessageBox.Show("Вы точно хотите удалить запись?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    object p1 = MainWindow.DB.Student.RemoveRange(deleteProduct);
                    try
                    {
                        MainWindow.DB.SaveChanges();
                        MainWindow.DB.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                        Clients = MainWindow.DB.Student.OrderBy(p => p.Firstname).OrderBy(p => p.LastName).ToList();
                        TableL.ItemsSource = Clients;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            else MessageBox.Show("Выберите элемент для удаления!");
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            FilterSearch();
        }

        private void Search_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (TableL != null)
            {
                CollectionViewSource.GetDefaultView(TableL.ItemsSource).Refresh();
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e) //редакт элемента
        {
            NavigationService.Navigate(new AddStudent((sender as ListViewItem).DataContext as Student, true));
        }
        private void Search_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Search.Text == "Название, комментарий...")
            {
                Search.Text = "";
            }
        }

        private void Search_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Search.Text == "")
                Search.Text = "Название, комментарий...";
            else
            {
                Refresh();
            }
        }

        private void FilterSearch()
        {
            var itemsInfo = Clients;

            if (Search.Text != "Название, комментарий..." && Search.Text != "")
            {
                itemsInfo = itemsInfo.Where(i => i.Firstname.ToLower().IndexOf(Search.Text.ToLower()) != -1 | i.LastName.ToLower().IndexOf(Search.Text.ToLower()) != -1).ToList();
            }
            TableL.ItemsSource = itemsInfo;
            if (itemsInfo != null)
                TableL.ItemsSource = itemsInfo;

        }

        private void Refresh()
        {
            var filtered = MainWindow.DB.Student.ToList();
            var text = (Search as TextBox).Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                filtered = filtered.Where(i => i.Firstname.ToLower().IndexOf(Search.Text.ToLower()) != -1 | i.LastName.ToLower().IndexOf(Search.Text.ToLower()) != -1).ToList();
            }
            if (TableL != null)
            {
                TableL.ItemsSource = filtered;
            }
        }

        bool sortId = true;
        bool sortName = true;
        bool sortBirthday = true;
        bool sortFirstname = true;
        bool sortPatronymic = true;
        bool sortCategory = true;
        private void Id_Sort(object sender, MouseButtonEventArgs e)
        {
            if (sortId)
                TableL.ItemsSource = ((List<Student>)TableL.ItemsSource).OrderBy(x => x.Id).ToList();
            else TableL.ItemsSource = ((List<Student>)TableL.ItemsSource).OrderByDescending(x => x.Id).ToList();
            sortId = !sortId;
        }
        private void Firstname_Sort(object sender, MouseButtonEventArgs e)
        {
            if (sortFirstname)
                TableL.ItemsSource = ((List<Student>)TableL.ItemsSource).OrderBy(x => x.Firstname).ToList();
            else TableL.ItemsSource = ((List<Student>)TableL.ItemsSource).OrderByDescending(x => x.Firstname).ToList();
            sortFirstname = !sortFirstname;
        }
        private void Name_Sort(object sender, MouseButtonEventArgs e)
        {
            if (sortName)
                TableL.ItemsSource = ((List<Student>)TableL.ItemsSource).OrderBy(x => x.LastName).ToList();
            else TableL.ItemsSource = ((List<Student>)TableL.ItemsSource).OrderByDescending(x => x.LastName).ToList();
            sortName = !sortName;
        }
        private void Patronymic_Sort(object sender, MouseButtonEventArgs e)
        {
            if (sortPatronymic)
                TableL.ItemsSource = ((List<Student>)TableL.ItemsSource).OrderBy(x => x.Patronymic).ToList();
            else TableL.ItemsSource = ((List<Student>)TableL.ItemsSource).OrderByDescending(x => x.Patronymic).ToList();
            sortPatronymic = !sortPatronymic;
        }
        private void Birthday_Sort(object sender, MouseButtonEventArgs e)
        {
            if (sortBirthday)
                TableL.ItemsSource = ((List<Student>)TableL.ItemsSource).OrderBy(x => x.Birthday).ToList();
            else TableL.ItemsSource = ((List<Student>)TableL.ItemsSource).OrderByDescending(x => x.Birthday).ToList();
            sortBirthday = !sortBirthday;
        }
        private void Status_Sort(object sender, MouseButtonEventArgs e)
        {
            if (sortCategory)
                TableL.ItemsSource = ((List<Student>)TableL.ItemsSource).OrderBy(x => x.Status).ToList();
            else TableL.ItemsSource = ((List<Student>)TableL.ItemsSource).OrderByDescending(x => x.Status).ToList();
            sortCategory = !sortCategory;
        }
    }
}
