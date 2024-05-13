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

namespace школа
{
    /// <summary>
    /// Логика взаимодействия для home.xaml
    /// </summary>
    public partial class home : Page
    {
        public home()
        {
            InitializeComponent();

            List<timetable> timetableList = Database.db.timetable.ToList();
            MatchesGrid.ItemsSource = timetableList;

            List<teacher> teachers = Database.db.teacher.ToList();
            coaching_staff.ItemsSource = teachers;
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Есть ли у вас аккаунт?", "Вход или Регистрация", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                WorkFrame.frame.Navigate(new vkhod ());

            }
            else
            {
                WorkFrame.frame.Navigate(new registratsiya ());

            }
        }
    }
}
