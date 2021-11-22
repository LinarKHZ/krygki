using System.Windows;
using System.Windows.Navigation;

namespace krygki
{
    public partial class MainWindow : NavigationWindow
    {
        public static krygkiDBEntities DB = new krygkiDBEntities();
        public static User userValue;
        public MainWindow()
        {
            InitializeComponent();
            MnWindow.NavigationService.Navigate(new Pages.Authorization(true));
        }
    }
}
