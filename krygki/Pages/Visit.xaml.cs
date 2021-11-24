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
    /// Логика взаимодействия для Visit.xaml
    /// </summary>
    public partial class Visit : Page
    {
        List<Visiting> Clients;
        public Visit()
        {
            InitializeComponent();
            TableL.ItemsSource = MainWindow.DB.Visiting.OrderBy(i => i.Id).ToList();
            if (MainWindow.userValue.Id_role != 1)
            {
                add1.Visibility = Visibility.Hidden;
                ext1.Visibility = Visibility.Hidden;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Clients = MainWindow.DB.Visiting.OrderBy(p => p.Id).ToList();
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
            //NavigationService.Navigate(new AddVisiting(null, false));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var deleteProduct = TableL.SelectedItems.Cast<Visiting>().ToList();
            if (TableL.SelectedItem as Visiting != null)
            {
                if (MessageBox.Show("Вы точно хотите удалить запись?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    object p1 = MainWindow.DB.Visiting.RemoveRange(deleteProduct);
                    try
                    {
                        MainWindow.DB.SaveChanges();
                        MainWindow.DB.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                        Clients = MainWindow.DB.Visiting.OrderBy(p => p.Id).ToList();
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
            //NavigationService.Navigate(new AddVisiting((sender as ListViewItem).DataContext as Visiting, true));
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

            //if (Search.Text != "Название, комментарий..." && Search.Text != "")
            //{
            //    itemsInfo = itemsInfo.Where(i => i.Club.Name.ToLower().IndexOf(Search.Text.ToLower()) != -1 | i.User.Firstname.ToLower().IndexOf(Search.Text.ToLower()) != -1).ToList();
            //}
            //if (Check.IsChecked == true)
            //{
            //    itemsInfo = itemsInfo.Where(u => u.Id_user == MainWindow.userValue.Id).ToList();
            //}
            if (itemsInfo != null)
            {
                TableL.ItemsSource = itemsInfo;
            }

        }

        private void Refresh()
        {
            var filtered = MainWindow.DB.Visiting.ToList();
            var text = (Search as TextBox).Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                //filtered = filtered.Where(i => i.Club.Name.ToLower().IndexOf(Search.Text.ToLower()) != -1 | i.User.Firstname.ToLower().IndexOf(Search.Text.ToLower()) != -1).ToList();
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
                TableL.ItemsSource = ((List<Visiting>)TableL.ItemsSource).OrderBy(x => x.Id).ToList();
            else TableL.ItemsSource = ((List<Visiting>)TableL.ItemsSource).OrderByDescending(x => x.Id).ToList();
            sortId = !sortId;
        }
        private void Firstname_Sort(object sender, MouseButtonEventArgs e)
        {
            //if (sortFirstname)
            //    TableL.ItemsSource = ((List<Visiting>)TableL.ItemsSource).OrderBy(x => x.Week).ToList();
            //else TableL.ItemsSource = ((List<Visiting>)TableL.ItemsSource).OrderByDescending(x => x.Week).ToList();
            sortFirstname = !sortFirstname;
        }
        private void Category_Sort(object sender, MouseButtonEventArgs e)
        {
            //if (sortCategory)
            //    TableL.ItemsSource = ((List<Visiting>)TableL.ItemsSource).OrderBy(x => x.cabinet).ToList();
            //else TableL.ItemsSource = ((List<Visiting>)TableL.ItemsSource).OrderByDescending(x => x.cabinet).ToList();
            //sortCategory = !sortCategory;
        }
        private void Status_Sort(object sender, MouseButtonEventArgs e)
        {
            if (sortCategory1)
                TableL.ItemsSource = ((List<Visiting>)TableL.ItemsSource).OrderBy(x => x.Status).ToList();
            else TableL.ItemsSource = ((List<Visiting>)TableL.ItemsSource).OrderByDescending(x => x.Status).ToList();
            sortCategory1 = !sortCategory1;
        }
        private void User_Sort(object sender, MouseButtonEventArgs e)
        {
            if (sortBirthday)
            //    TableL.ItemsSource = ((List<Visiting>)TableL.ItemsSource).OrderBy(x => x.User.Firstname).ToList();
            //else TableL.ItemsSource = ((List<Visiting>)TableL.ItemsSource).OrderByDescending(x => x.User.Firstname).ToList();
            sortBirthday = !sortBirthday;
        }
        private void Club_Sort(object sender, MouseButtonEventArgs e)
        {
            if (sortPatronymic)
            //    TableL.ItemsSource = ((List<Visiting>)TableL.ItemsSource).OrderBy(x => x.Club.Name).ToList();
            //else TableL.ItemsSource = ((List<Visiting>)TableL.ItemsSource).OrderByDescending(x => x.Club.Name).ToList();
            sortPatronymic = !sortPatronymic;
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            FilterSearch();
        }
    }
}
