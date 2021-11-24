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
    /// Логика взаимодействия для TableHistory.xaml
    /// </summary>
    public partial class TableHistory : Page
    {
        List<History> Clients;
        public TableHistory()
        {
            InitializeComponent();
            TableL.ItemsSource = MainWindow.DB.History.OrderBy(i => i.Id).ToList();
            if (MainWindow.userValue.Id_role != 1)
            {
                ext1.Visibility = Visibility.Hidden;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Clients = MainWindow.DB.History.OrderBy(p => p.Id).ToList();
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
            NavigationService.Navigate(new AddHistory(null, false));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var deleteProduct = TableL.SelectedItems.Cast<History>().ToList();
            if (TableL.SelectedItem as History != null)
            {
                if (MessageBox.Show("Вы точно хотите удалить запись?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    object p1 = MainWindow.DB.History.RemoveRange(deleteProduct);
                    try
                    {
                        MainWindow.DB.SaveChanges();
                        MainWindow.DB.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                        Clients = MainWindow.DB.History.OrderBy(p => p.Id).ToList();
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
            NavigationService.Navigate(new AddHistory((sender as ListViewItem).DataContext as History, true));
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
                FilterSearch();
            }
        }

        private void FilterSearch()
        {
            var itemsInfo = Clients;

            if (Search.Text != "Название, комментарий..." && Search.Text != "")
            {
                itemsInfo = itemsInfo.Where(i => i.Timetable.Club.Name.ToLower().IndexOf(Search.Text.ToLower()) != -1 | i.Timetable.User.Firstname.ToLower().IndexOf(Search.Text.ToLower()) != -1).ToList();
            }
            if (itemsInfo != null)
            {
                TableL.ItemsSource = itemsInfo;
            }

        }

        private void Refresh()
        {
            var filtered = MainWindow.DB.History.ToList();
            var text = (Search as TextBox).Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                filtered = filtered.Where(i => i.Timetable.Club.Name.ToLower().IndexOf(Search.Text.ToLower()) != -1 | i.Timetable.User.Firstname.ToLower().IndexOf(Search.Text.ToLower()) != -1).ToList();
            }
            if (TableL != null)
            {
                TableL.ItemsSource = filtered;
            }
        }

        bool sortId = true;
        bool sortBirthday = true;
        bool sortFirstname = true;
        bool sortPatronymic = true;
        bool sortCategory = true;
        bool sortCategory1 = true;
        private void Id_Sort(object sender, MouseButtonEventArgs e)
        {
            if (sortId)
                TableL.ItemsSource = ((List<History>)TableL.ItemsSource).OrderBy(x => x.Id).ToList();
            else TableL.ItemsSource = ((List<History>)TableL.ItemsSource).OrderByDescending(x => x.Id).ToList();
            sortId = !sortId;
        }
        private void Firstname_Sort(object sender, MouseButtonEventArgs e)
        {
            if (sortFirstname)
                TableL.ItemsSource = ((List<History>)TableL.ItemsSource).OrderBy(x => x.Timetable.Week).ToList();
            else TableL.ItemsSource = ((List<History>)TableL.ItemsSource).OrderByDescending(x => x.Timetable.Week).ToList();
            sortFirstname = !sortFirstname;
        }
        private void Category_Sort(object sender, MouseButtonEventArgs e)
        {
            if (sortCategory)
                TableL.ItemsSource = ((List<History>)TableL.ItemsSource).OrderBy(x => x.Timetable.cabinet).ToList();
            else TableL.ItemsSource = ((List<History>)TableL.ItemsSource).OrderByDescending(x => x.Timetable.cabinet).ToList();
            sortCategory = !sortCategory;
        }
        private void Status_Sort(object sender, MouseButtonEventArgs e)
        {
            if (sortCategory1)
                TableL.ItemsSource = ((List<History>)TableL.ItemsSource).OrderBy(x => x.Statusof).ToList();
            else TableL.ItemsSource = ((List<History>)TableL.ItemsSource).OrderByDescending(x => x.Statusof).ToList();
            sortCategory1 = !sortCategory1;
        }
        private void User_Sort(object sender, MouseButtonEventArgs e)
        {
            if (sortBirthday)
                TableL.ItemsSource = ((List<History>)TableL.ItemsSource).OrderBy(x => x.Timetable.User.Firstname).ToList();
            else TableL.ItemsSource = ((List<History>)TableL.ItemsSource).OrderByDescending(x => x.Timetable.User.Firstname).ToList();
            sortBirthday = !sortBirthday;
        }
        private void Club_Sort(object sender, MouseButtonEventArgs e)
        {
            if (sortPatronymic)
                TableL.ItemsSource = ((List<History>)TableL.ItemsSource).OrderBy(x => x.Timetable.Club.Name).ToList();
            else TableL.ItemsSource = ((List<History>)TableL.ItemsSource).OrderByDescending(x => x.Timetable.Club.Name).ToList();
            sortPatronymic = !sortPatronymic;
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            FilterSearch();
        }

    }
}

