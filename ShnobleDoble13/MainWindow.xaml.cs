using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ShnobleDoble13
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            OnStart();
        }

        private void OnStart()
        {
            DataGridUsers.ItemsSource = Models.StudentsEntities.GetContext().Students.ToList();
            GroupComboBox.ItemsSource = Models.StudentsEntities.GetContext().Groups.ToList();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            string FioText = NameFioTB.Text.ToString().ToLower();
            var selectedGroup = GroupComboBox.SelectedIndex + 1;
            if (string.IsNullOrEmpty(FioText))
            {
                errors.AppendLine("Введите фио");
            }
            if (selectedGroup <= 0)
            {
                errors.AppendLine("Выберите группу");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Models.StudentsEntities.GetContext().Students.Add(new Models.Students()
            {
                Fio = FioText,
                Group_Id = selectedGroup
            });
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.StudentsEntities.GetContext().SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Models.StudentsEntities.GetContext().SaveChanges();
            OnStart();
        }
    }
}
