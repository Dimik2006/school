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
    /// Логика взаимодействия для teacherPage.xaml
    /// </summary>
    public partial class teacherPage : Page
    {
        public teacherPage()
        {
            InitializeComponent();

            TxbSurname.Text = WorkAccount.account.surname;
            TxbName.Text = WorkAccount.account.name;
            TxbLastname.Text = WorkAccount.account.lastname;
            TxbPassword.Text = WorkAccount.account.password;
            TxbLogin.Text = WorkAccount.account.login;

            teacher teacher = Database.db.teacher.Where(a => a.account == WorkAccount.account.id).First();

            TxbItem.Text = teacher.subject;
            TxbExperience.Text = teacher.experience.ToString();

            if (WorkAccount.account.avatarka != null)
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
            if (TxbLogin.Text != WorkAccount.account.login && Database.db.account.Where(a => a.login == TxbLogin.Text).Count() > 0)
            {
                MessageBox.Show("Аккаунт с таким логином уже существует", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (TxbLogin.Text != "" && TxbPassword.Text != "" && TxbName.Text != "" && TxbSurname.Text != "" && TxbLastname.Text != "" && TxbExperience.Text != "" && TxbItem.Text != "")
            {
                
                WorkAccount.account.login = TxbLogin.Text;
                WorkAccount.account.password = TxbPassword.Text;
                WorkAccount.account.name = TxbName.Text;
                WorkAccount.account.surname = TxbSurname.Text;
                WorkAccount.account.lastname = TxbLastname.Text;
                teacher teacher = Database.db.teacher.Where(a => a.account == WorkAccount.account.id).First();
                teacher.experience = Convert.ToInt32(TxbExperience.Text);
                teacher.subject = TxbItem.Text;
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
            if(button.Content == "Добавить д\\з")
            {
                if(Date.SelectedDate == null || Txb_Item.Text == "" || TxbHome_Work.Text == "" || TxbClass.Text == "")
                {
                    MessageBox.Show("Все поля должны быть заполнены", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                @class @class = Database.db.@class.Where(a => a.name == TxbClass.Text).FirstOrDefault();
                if(@class == null)
                {
                    MessageBox.Show("Класса с таким названием не существует", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                homework homework = new homework()
                {
                    text = TxbHome_Work.Text,
                    subject = Txb_Item.Text,
                    class1 = @class,
                    date = (DateTime)Date.SelectedDate
                };
                Database.db.homework.Add(homework);
                Database.db.SaveChanges();
                MessageBox.Show("Сохранение завершено", "Информация о сохранении", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if(button.Content == "Поставить оценку")
            {
                if(Date_.SelectedDate == null || Txb__Item.Text == "" || TxbGrade.Text == "" || TxbFIO.Text == "" || Txb_class.Text == "")
                {
                    MessageBox.Show("Все поля должны быть заполнены", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                @class @class = Database.db.@class.Where(a => a.name == Txb_class.Text).FirstOrDefault();
                if (@class == null)
                {
                    MessageBox.Show("Класса с таким названием не существует", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                string[] fio = TxbFIO.Text.Split();
                account account = Database.db.account.Where(a => a.surname == fio[0] && a.name == fio[1] && a.lastname == fio[2]).FirstOrDefault();
                if(account == null)
                {
                    MessageBox.Show("Ученика с таким ФИО не существует", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                student student = Database.db.student.Where(a => a.@class == @class.id && a.account == account.id).FirstOrDefault();
                if(student == null)
                {
                    MessageBox.Show("Ученика с таким ФИО не существует", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                assessment assessment = new assessment()
                {
                    Mark = Convert.ToInt16(TxbGrade.Text),
                    subject = Txb__Item.Text,
                    date = (DateTime)Date_.SelectedDate
                };
                assessmentForStudent assessmentForStudent = new assessmentForStudent()
                {
                    student1 = student,
                    assessment1 = assessment
                };
                Database.db.assessmentForStudent.Add(assessmentForStudent);
                Database.db.SaveChanges();
                MessageBox.Show("Сохранение завершено", "Информация о сохранении", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if(Date__.SelectedDate == null || TxbCause.Text == "" || Txb_FIO.Text == "" || Txb__class.Text == "")
                {
                    MessageBox.Show("Все поля должны быть заполнены", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                @class @class = Database.db.@class.Where(a => a.name == Txb__class.Text).FirstOrDefault();
                if (@class == null)
                {
                    MessageBox.Show("Класса с таким названием не существует", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                string[] fio = Txb_FIO.Text.Split();
                account account = Database.db.account.Where(a => a.surname == fio[0] && a.name == fio[1] && a.lastname == fio[2]).FirstOrDefault();
                if (account == null)
                {
                    MessageBox.Show("Ученика с таким ФИО не существует", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                student student = Database.db.student.Where(a => a.@class == @class.id && a.account == account.id).FirstOrDefault();
                if (student == null)
                {
                    MessageBox.Show("Ученика с таким ФИО не существует", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                attendance attendance = new attendance()
                {
                    date = (DateTime)Date__.SelectedDate,
                    reason = TxbCause.Text
                };
                attendancesForStudent attendancesForStudent = new attendancesForStudent
                {
                    attendance1 = attendance,
                    student1 = student
                };

                Database.db.attendancesForStudent.Add(attendancesForStudent);
                Database.db.SaveChanges();
                MessageBox.Show("Сохранение завершено", "Информация о сохранении", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            WorkFrame.frame.Navigate(new home());
        }
    }
}
