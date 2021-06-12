using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApplicationEntity.MyClasses;

namespace WpfApplicationEntity.Forms.LoginForm
{
    /// <summary>
    /// Логика взаимодействия для LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (WpfApplicationEntity.MyClasses.MyDBContext MyDBContext = new WpfApplicationEntity.MyClasses.MyDBContext())
            {
                var tmp = (
                    from tmpEmployees in MyDBContext.Employees.ToList<Employees>()
                    where tmpEmployees.Login.CompareTo(Log.Text) == 0 && tmpEmployees.Password.CompareTo(Pass.Text) == 0
                    select tmpEmployees
                    ).ToList();
                if (tmp.Count > 0)
                {
                    if (tmp[0].Position == "Администратор")
                    {
                        MainWindow g = new MainWindow();
                        if (g.ShowDialog() == true)
                            this.ShowAll();
                    }
                    else if (tmp[0].Position == "Приёмщик заказов")
                    {
                        MainWindow g = new MainWindow();
                        if (g.ShowDialog() == true)
                            this.ShowAll();
                    }
                    else if (tmp[0].Position == "Курьер")
                    {
                        MainWindow g = new MainWindow();
                        if (g.ShowDialog() == true)
                            this.ShowAll();
                    }
                }
                else
                {
                    MessageBox.Show("Wrong Data!", "Fatal FE", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void ShowAll()
        {
            try
            {
                using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext = new WpfApplicationEntity.MyClasses.MyDBContext())
                {
                    // var list = WFAEntity.API.DatabaseRequest.GetMatType(objectMyDBContext);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
