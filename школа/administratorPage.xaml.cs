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
    /// Логика взаимодействия для administratorPage.xaml
    /// </summary>
    public partial class administratorPage : Page
    {
        public administratorPage()
        {
            InitializeComponent();

            TxbSurname.Text = WorkAccount.account.surname;
            TxbName.Text = WorkAccount.account.name;
            TxbLastname.Text = WorkAccount.account.lastname;
            TxbPassword.Text = WorkAccount.account.password;
            TxbLogin.Text = WorkAccount.account.login;

            if(WorkAccount.account.avatarka != null)
            {
                Avatarka.Source = new BitmapImage(new Uri($@"{Directory.GetCurrentDirectory()}\Images\{WorkAccount.account.avatarka}"));
            }
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
            if(TxbLogin.Text != "" && TxbPassword.Text != "" && TxbName.Text != "" && TxbSurname.Text != "" && TxbLastname.Text != "" && TxbPin.Text != "")
            {
                administrator administrator = Database.db.administrator.Where(a => a.account1.id == WorkAccount.account.id).First();
                if(administrator.pincode != TxbOldPin.Text)
                {
                    MessageBox.Show("Пин код не правильный", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (TxbLogin.Text != WorkAccount.account.login && Database.db.account.Where(a => a.login == TxbLogin.Text).Count() > 0)
                {
                    MessageBox.Show("Аккаунт с таким логином уже существует", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                WorkAccount.account.login = TxbLogin.Text;
                WorkAccount.account.password = TxbPassword.Text;
                WorkAccount.account.name = TxbName.Text;
                WorkAccount.account.surname = TxbSurname.Text;
                WorkAccount.account.lastname = TxbLastname.Text;
                administrator.pincode = TxbPin.Text;
                Database.db.SaveChanges();
                MessageBox.Show("Сохранение завершено", "Информация о сохранении", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if(button.Content == "Добавить новый класс")
            {
                administrator administrator = Database.db.administrator.Where(a => a.account1.id == WorkAccount.account.id).First();
                if (administrator.pincode != Txb_Pin.Text)
                {
                    MessageBox.Show("Пин код не правильный", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (TxbClasse_name.Text == "")
                {
                    MessageBox.Show("Все поля должны быть заполнены", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                @class @class = new @class()
                {
                    name = TxbClasse_name.Text
                };
                Database.db.@class.Add(@class);
                Database.db.SaveChanges();
                MessageBox.Show("Сохранение завершено", "Информация о сохранении", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (button.Content == "Добавить нового учителя")
            {
                administrator administrator = Database.db.administrator.Where(a => a.account1.id == WorkAccount.account.id).First();
                if (administrator.pincode != Txb__Pin.Text)
                {
                    MessageBox.Show("Пин код не правильный", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (Txb_Login.Text == "" || Txb_Password.Text == "" || Txb_Surname.Text == "" || Txb_Name.Text == "" || Txb_Lastname.Text == "" || TxbItem.Text == "" || TxbExperience.Text == "")
                {
                    MessageBox.Show("Все поля должны быть заполнены", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (Database.db.account.Where(a => a.login == Txb_Login.Text).Count() > 0)
                {
                    MessageBox.Show("Аккаунт с таким логином уже существует", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                account account = new account()
                {
                    login = Txb_Login.Text,
                    password = Txb_Password.Text,
                    name = Txb_Name.Text,
                    surname = Txb_Surname.Text,
                    lastname = Txb_Lastname.Text
                };
                teacher teacher = new teacher()
                {
                    account1 = account,
                    subject = TxbItem.Text,
                    experience = Convert.ToInt32(TxbExperience.Text)
                };
                Database.db.teacher.Add(teacher);
                Database.db.SaveChanges();
                MessageBox.Show("Сохранение завершено", "Информация о сохранении", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                administrator administrator = Database.db.administrator.Where(a => a.account1.id == WorkAccount.account.id).First();
                if (administrator.pincode != Txb___Pin.Text)
                {
                    MessageBox.Show("Пин код не правильный", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (className.Text == "" || TxbLesson1.Text == "" || TxbLesson2.Text == "" || TxbLesson3.Text == "" || TxbLesson4.Text == "" || TxbLesson5.Text == "" || TxbLesson6.Text == "" || date.SelectedDate == null)
                {
                    MessageBox.Show("Все поля должны быть заполнены", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                @class @class = Database.db.@class.Where(a => a.name == className.Text).FirstOrDefault();
                if(@class == null)
                {
                    MessageBox.Show("Класса с таким названием не существует", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                timetable timetable = new timetable()
                {
                    date = (DateTime)date.SelectedDate,
                    @class1 = @class,
                    lesson1 = TxbLesson1.Text,
                    lesson2 = TxbLesson2.Text,
                    lesson3 = TxbLesson3.Text,
                    lesson4 = TxbLesson4.Text,
                    lesson5 = TxbLesson5.Text,
                    lesson6 = TxbLesson6.Text
                };
                Database.db.timetable.Add(timetable);
                Database.db.SaveChanges();
            }
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            WorkFrame.frame.Navigate(new home());
        }
    }
}
