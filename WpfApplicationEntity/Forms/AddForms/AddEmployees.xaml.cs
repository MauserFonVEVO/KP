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

namespace WpfApplicationEntity.Forms.AddForms
{
    /// <summary>
    /// Логика взаимодействия для AddEmployees.xaml
    /// </summary>
    public partial class AddEmployees : Window
    {
        private bool add_edit;
        private int id;
        //string Name;
        public AddEmployees()
        {
            InitializeComponent();
        }
        public AddEmployees(bool add_edit, int id = 0)
        {
            InitializeComponent();
            this.add_edit = add_edit;
            this.id = id;
            if (this.add_edit == false)
            {
                using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext = new WpfApplicationEntity.MyClasses.MyDBContext())
                {
                    WpfApplicationEntity.MyClasses.Employees Employees = WpfApplicationEntity.MyClasses.DatabaseRequest.GetEmployeesID(objectMyDBContext, this.id);
                    textAddID.Text = Convert.ToString(Employees.IDEmpoyes);
                    textAddSurname.Text = Employees.Surname;
                    textAddName.Text = Employees.Name;
                    textAddMiddleName.Text = Employees.MiddelName;
                    textAddAdress.Text = Employees.adress;
                    textAddBirthday.Text = Employees.Birthday;
                    textAddCategory.Text = Employees.Position;
                    textAddLogin.Text = Employees.Login;
                    textAddPassword.Text = Employees.Password;
                    textAddPass.Text = Employees.Pass;
                    textAddTelephone.Text = Employees.NumberTelephone;
                }
                ButtonAddEditEmployees.Content = "Изменить";
            }
        }
        private void ButtonAddEditEmployees_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.IsDataCorrcet() == true)
                {
                    using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext =
                            new WpfApplicationEntity.MyClasses.MyDBContext())
                    {
                        WpfApplicationEntity.MyClasses.Employees objectEmplo = new WpfApplicationEntity.MyClasses.Employees();
                        objectEmplo.IDEmpoyes = Convert.ToInt32(textAddID.Text);
                        objectEmplo.Surname = textAddSurname.Text;
                        objectEmplo.Name = textAddName.Text;
                        objectEmplo.MiddelName = textAddMiddleName.Text;
                        objectEmplo.adress = textAddAdress.Text;
                        objectEmplo.Birthday = textAddBirthday.Text;
                        objectEmplo.Position = textAddCategory.Text;
                        objectEmplo.Login = textAddLogin.Text;
                        objectEmplo.Password = textAddPassword.Text;
                        objectEmplo.Pass = textAddPass.Text;
                        objectEmplo.NumberTelephone = textAddTelephone.Text;
                        if (this.add_edit == true)
                        {
                            objectMyDBContext.Employees.Add(objectEmplo);
                        }
                        else
                        {
                            WpfApplicationEntity.MyClasses.Employees objectTransportFromDataBase = new WpfApplicationEntity.MyClasses.Employees();
                            objectTransportFromDataBase = WpfApplicationEntity.MyClasses.DatabaseRequest.GetEmployeesID(objectMyDBContext, this.id);
                            objectMyDBContext.Entry(objectTransportFromDataBase).CurrentValues.SetValues(objectEmplo);
                        }
                        objectMyDBContext.SaveChanges();
                        this.DialogResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА (Сотрудник)", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool IsDataCorrcet()
        {
            return textAddID.Text != string.Empty
                    && textAddSurname.Text != string.Empty
                    && textAddName.Text != string.Empty
                    && textAddMiddleName.Text != string.Empty
                    && textAddAdress.Text != string.Empty
                    && textAddBirthday.Text != string.Empty
                    && textAddCategory.Text != string.Empty
                    && textAddLogin.Text != string.Empty
                    && textAddPassword.Text != string.Empty
                    && textAddPass.Text != string.Empty
                    && textAddTelephone.Text != string.Empty;
        }
        }
    }




/*if (this.add_edit == true)
                if (textAddID.Text != string.Empty
                    && textAddSurname.Text != string.Empty
                    && textAddName.Text != string.Empty
                    && textAddMiddleName.Text != string.Empty
                    && textAddAdress.Text != string.Empty
                    && textAddBirthday.Text != string.Empty
                    && textAddCategory.Text != string.Empty
                    && textAddLogin.Text != string.Empty
                    && textAddPassword.Text != string.Empty
                    && textAddPass.Text != string.Empty
                    && textAddTelephone.Text != string.Empty)
                {
                    WpfApplicationEntity.MyClasses.Employees objectEmplo = new WpfApplicationEntity.MyClasses.Employees();
                    objectEmplo.IDEmpoyes = Convert.ToInt32(textAddID.Text);
                    objectEmplo.Surname = textAddSurname.Text;
                    objectEmplo.Name = textAddName.Text;
                    objectEmplo.MiddelName = textAddMiddleName.Text;
                    objectEmplo.adress = textAddAdress.Text;
                    objectEmplo.Birthday = textAddBirthday.Text;
                    objectEmplo.Position = textAddCategory.Text;
                    objectEmplo.Login = textAddLogin.Text;
                    objectEmplo.Password = textAddPassword.Text;
                    objectEmplo.Pass = textAddPass.Text;
                    objectEmplo.NumberTelephone = textAddTelephone.Text;
                    try
                    {
                        using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext = new WpfApplicationEntity.MyClasses.MyDBContext())
                        {
                            objectMyDBContext.Employees.Add(objectEmplo);
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("Сотрудник добавлен");
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка!");
                    this.DialogResult = false;
                }*/