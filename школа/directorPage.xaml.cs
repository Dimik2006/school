using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для directorPage.xaml
    /// </summary>
    public partial class directorPage : Page
    {
        public directorPage()
        {
            InitializeComponent();

            TxbSurname.Text = WorkAccount.account.surname;
            TxbName.Text = WorkAccount.account.name;
            TxbLastname.Text = WorkAccount.account.lastname;
            TxbPassword.Text = WorkAccount.account.password;
            TxbLogin.Text = WorkAccount.account.login;

            if (WorkAccount.account.avatarka != null)
            {
                Avatarka.Source = new BitmapImage(new Uri($@"{Directory.GetCurrentDirectory()}\Images\{WorkAccount.account.avatarka}"));
            }

            List<student> studentsList = Database.db.student.ToList();
            students.ItemsSource = studentsList;

            List<classesForTeacher> classesForTeachers = Database.db.classesForTeacher.ToList();
            classe.ItemsSource = classesForTeachers;

            List<account> accounts = Database.db.account.ToList();
            users.ItemsSource = accounts;
        }

        private void Button_Click_Avatarka(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Title = "Выберите аватарку",
                Multiselect = false,
                Filter = "Фото |*.png;*.jpg"
            };

            if ((bool)fileDialog.ShowDialog())
            {
                string photo = $@"{Guid.NewGuid()}.png";
                string FullPath = $@"{Directory.GetCurrentDirectory()}\Images\{photo}";

                Directory.CreateDirectory(FullPath);

                using (FileStream fileStream = new FileStream(fileDialog.FileName, FileMode.Open))
                {
                    using (FileStream saveFileStream = new FileStream(FullPath, FileMode.Create))
                    {
                        fileStream.CopyTo(saveFileStream);
                    }
                }

                WorkAccount.account.avatarka = photo;
                Avatarka.Source = new BitmapImage(new Uri(FullPath));
                Database.db.SaveChanges();
            }
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            if (TxbLogin.Text != WorkAccount.account.login && Database.db.account.Where(a => a.login == TxbLogin.Text).Count() > 0)
            {
                MessageBox.Show("Аккаунт с таким логином уже существует", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (TxbLogin.Text != "" && TxbPassword.Text != "" && TxbName.Text != "" && TxbSurname.Text != "" && TxbLastname.Text != "")
            {
                WorkAccount.account.login = TxbLogin.Text;
                WorkAccount.account.password = TxbPassword.Text;
                WorkAccount.account.name = TxbName.Text;
                WorkAccount.account.surname = TxbSurname.Text;
                WorkAccount.account.lastname = TxbLastname.Text;
                Database.db.SaveChanges();
                MessageBox.Show("Сохранение завершено", "Информация о сохранении", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            WorkFrame.frame.Navigate(new home());
        }
    }
}
