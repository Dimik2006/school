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
    /// Логика взаимодействия для registratsiya.xaml
    /// </summary>
    public partial class registratsiya : Page
    {
        public registratsiya()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if(TxbLogin.Text != "" && PwbPassword.Text != "" && TxbName.Text != "" && TxbSurname.Text != "" && TxbLastName.Text != "" && TxbClasse.Text != "")
            {
                @class @class = Database.db.@class.Where(a => a.name == TxbClasse.Name).FirstOrDefault();
                if(@class == null)
                {
                    MessageBox.Show("Класса с таким названием не существует", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (Database.db.account.Where(a => a.login == TxbLogin.Text).Count() > 0)
                {
                    MessageBox.Show("Аккаунт с таким логином уже существует", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                account account = new account()
                {
                    name = TxbName.Text,
                    surname = TxbSurname.Text,
                    lastname = TxbLastName.Text,
                    login = TxbLogin.Text,
                    password = PwbPassword.Text
                };
                student student = new student()
                {
                    account1 = account,
                    class1 = @class
                };
                Database.db.student.Add(student);
                Database.db.SaveChanges();
                WorkFrame.frame.Navigate(new home());
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
