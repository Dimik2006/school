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
    /// Логика взаимодействия для vkhod.xaml
    /// </summary>
    public partial class vkhod : Page
    {
        public vkhod()
        {
            InitializeComponent();
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            account account = Database.db.account.Where(a => a.login == TxbLogin.Text).FirstOrDefault();
            if(account != null && account.password == TxbPassword.Text)
            {
                WorkAccount.account = account;
                if(Database.db.administrator.Where(a => a.account == account.id).Count() > 0)
                {
                    WorkFrame.frame.Navigate(new administratorPage());
                }
                else if(Database.db.director.Where(a => a.account == account.id).Count() > 0)
                {
                    WorkFrame.frame.Navigate(new directorPage());
                }
                else if(Database.db.teacher.Where(a => a.account == account.id).Count() > 0)
                {
                    WorkFrame.frame.Navigate (new teacherPage());
                }
                else if(Database.db.student.Where(a => a.account == account.id).Count() > 0)
                {
                    WorkFrame.frame.Navigate(new studentPage());
                }
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
