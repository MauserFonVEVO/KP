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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplicationEntity.MyClasses;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Threading;
namespace WpfApplicationEntity
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext = new WpfApplicationEntity.MyClasses.MyDBContext())
                {
                    try
                    {
                        if (objectMyDBContext.Database.Exists() == false)
                        {
                            objectMyDBContext.Database.Create();
                            WpfApplicationEntity.MyClasses.Employees objectUser = new WpfApplicationEntity.MyClasses.Employees();
                            objectUser.Name = "Jason";
                            objectMyDBContext.Employees.Add(objectUser);
                            objectMyDBContext.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Создание базы данных");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.ShowAll();
        }
        #region Группа
        private void addGroupButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.AddForms.AddTransport g = new Forms.AddForms.AddTransport(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        #endregion
        private void ShowAll()
        {
            try
            {
                using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext = new WpfApplicationEntity.MyClasses.MyDBContext())
                {
                    transportGrid.ItemsSource = WpfApplicationEntity.MyClasses.DatabaseRequest.GetTransport(objectMyDBContext);
                    OrderGrid.ItemsSource = WpfApplicationEntity.MyClasses.DatabaseRequest.GetOrder(objectMyDBContext);
                    packGrid.ItemsSource = WpfApplicationEntity.MyClasses.DatabaseRequest.GetPackage(objectMyDBContext);
                    EmploGrid.ItemsSource = WpfApplicationEntity.MyClasses.DatabaseRequest.GetEmployees(objectMyDBContext);
                    delivGrid.ItemsSource = WpfApplicationEntity.MyClasses.DatabaseRequest.GetEmployeesWithSchedule(objectMyDBContext);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void addorderButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.AddForms.AddOrder g = new Forms.AddForms.AddOrder(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }

        private void addTransportButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.AddForms.AddEmployees g = new Forms.AddForms.AddEmployees(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        private void addpackButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.AddForms.AddPackage g = new Forms.AddForms.AddPackage(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }
        private void adddelivButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.AddForms.AddDelivery g = new Forms.AddForms.AddDelivery(true);
            if (g.ShowDialog() == true)
                this.ShowAll();
        }

        private void editGroupButton_Click(object sender, RoutedEventArgs e)
        {
            if (transportGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < transportGrid.SelectedItems.Count; i++)
                {
                    WpfApplicationEntity.MyClasses.Transport objectGroup = transportGrid.SelectedItems[i] as WpfApplicationEntity.MyClasses.Transport;
                    if (objectGroup != null)
                    {
                        Forms.AddForms.AddTransport g = new Forms.AddForms.AddTransport(false, objectGroup.IDTransport);
                        if (g.ShowDialog() == true)
                            this.ShowAll();
                    }
                }
            }
            else
                MessageBox.Show("Выберите строку");
        }
        private void deleteorderButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrderGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < OrderGrid.SelectedItems.Count; i++)
                {
                    WpfApplicationEntity.MyClasses.Order objectOrder = OrderGrid.SelectedItems[i] as WpfApplicationEntity.MyClasses.Order;
                    if (objectOrder != null)
                    {
                        Forms.AddForms.AddOrder g = new Forms.AddForms.AddOrder(false, objectOrder.IDOrder);
                        if (g.ShowDialog() == true)
                            this.ShowAll();
                    }
                }
            }
            else
                MessageBox.Show("Выберите строку");
        }
        private void deletedelivButton_Click(object sender, RoutedEventArgs e)
        {
            if (delivGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < delivGrid.SelectedItems.Count; i++)
                {
                    WpfApplicationEntity.MyClasses.Delivery objectDelivery = delivGrid.SelectedItems[i] as WpfApplicationEntity.MyClasses.Delivery;
                    if (objectDelivery != null)
                    {
                        Forms.AddForms.AddDelivery g = new Forms.AddForms.AddDelivery(false, objectDelivery.IDAppointment);
                        if (g.ShowDialog() == true)
                            this.ShowAll();
                    }
                }
            }
            else
                MessageBox.Show("Выберите строку");
        }
        private void deleteTransportButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmploGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < EmploGrid.SelectedItems.Count; i++)
                {
                    WpfApplicationEntity.MyClasses.Employees objectEmployees = EmploGrid.SelectedItems[i] as WpfApplicationEntity.MyClasses.Employees;
                    if (objectEmployees != null)
                    {
                        Forms.AddForms.AddEmployees g = new Forms.AddForms.AddEmployees(false, objectEmployees.IDEmpoyes);
                        if (g.ShowDialog() == true)
                            this.ShowAll();
                    }
                }
            }
            else
                MessageBox.Show("Выберите строку");
        }
        private void deletepackButton_Click(object sender, RoutedEventArgs e)
        {
            if (packGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < packGrid.SelectedItems.Count; i++)
                {
                    WpfApplicationEntity.MyClasses.Package objectPackage = packGrid.SelectedItems[i] as WpfApplicationEntity.MyClasses.Package;
                    if (objectPackage != null)
                    {
                        Forms.AddForms.AddPackage g = new Forms.AddForms.AddPackage(false, objectPackage.IDPackage);
                        if (g.ShowDialog() == true)
                            this.ShowAll();
                    }
                }
            }
            else
                MessageBox.Show("Выберите строку");
        }

        private void editpackButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var itm = (Package)packGrid.SelectedItem;
                var tmp = (
                from tmpPack in WpfApplicationEntity.MyClasses.MyDBContext.DBContext.Package.ToList<Package>()
                where tmpPack.IDPackage.CompareTo(itm.IDPackage) == 0
                select tmpPack
                    ).ToList();
                WpfApplicationEntity.MyClasses.MyDBContext.DBContext.Package.Remove(tmp[0]);
                WpfApplicationEntity.MyClasses.MyDBContext.DBContext.SaveChanges();
                ShowAll();
            }
            catch
            {
                MessageBox.Show("Выберите строку!", "Ошибка!");
            }
        }
        private void deleteGroupButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var itm = (Transport)transportGrid.SelectedItem;
                var tmp = (
                from tmpTransport in WpfApplicationEntity.MyClasses.MyDBContext.DBContext.Transport.ToList<Transport>()
                where tmpTransport.IDTransport.CompareTo(itm.IDTransport) == 0
                select tmpTransport
                    ).ToList();
                WpfApplicationEntity.MyClasses.MyDBContext.DBContext.Transport.Remove(tmp[0]);
                WpfApplicationEntity.MyClasses.MyDBContext.DBContext.SaveChanges();
                ShowAll();
            }
            catch
            {
                MessageBox.Show("Выберите строку!", "Ошибка!");
            }
        }
        private void editorderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var itm = (Order)OrderGrid.SelectedItem;
                var tmp = (
                from tmpOrder in WpfApplicationEntity.MyClasses.MyDBContext.DBContext.Order.ToList<Order>()
                where tmpOrder.IDOrder.CompareTo(itm.IDOrder) == 0
                select tmpOrder
                    ).ToList();
                WpfApplicationEntity.MyClasses.MyDBContext.DBContext.Order.Remove(tmp[0]);
                WpfApplicationEntity.MyClasses.MyDBContext.DBContext.SaveChanges();
                ShowAll();
            }
            catch
            {
                MessageBox.Show("Выберите строку!", "Ошибка!");
            }
        }
        private void editTransportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var itm = (Employees)EmploGrid.SelectedItem;
                var tmp = (
                from tmpEmployees in WpfApplicationEntity.MyClasses.MyDBContext.DBContext.Employees.ToList<Employees>()
                where tmpEmployees.IDEmpoyes.CompareTo(itm.IDEmpoyes) == 0
                select tmpEmployees
                    ).ToList();
                WpfApplicationEntity.MyClasses.MyDBContext.DBContext.Employees.Remove(tmp[0]);
                WpfApplicationEntity.MyClasses.MyDBContext.DBContext.SaveChanges();
                ShowAll();
            }
            catch
            {
                MessageBox.Show("Выберите строку!", "Ошибка!");
            }
        }

        private void editdelivButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var itm = (Delivery)delivGrid.SelectedItem;
                var tmp = (
                from tmpDelivery in WpfApplicationEntity.MyClasses.MyDBContext.DBContext.Delivery.ToList<Delivery>()
                where tmpDelivery.IDAppointment.CompareTo(itm.IDAppointment) == 0
                select tmpDelivery
                    ).ToList();
                WpfApplicationEntity.MyClasses.MyDBContext.DBContext.Delivery.Remove(tmp[0]);
                WpfApplicationEntity.MyClasses.MyDBContext.DBContext.SaveChanges();
                ShowAll();
            }
            catch
            {
                MessageBox.Show("Выберите строку!", "Ошибка!");
            }
        }
        private void reportButton_Click(object sender, RoutedEventArgs e)
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "TimesNewRoman";
            workSheet.Cells[1, 1].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 1].Interior.ColorIndex = 17;
            workSheet.Cells[1, 1] = "Фамилия";
            workSheet.Cells[1, 2].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 2].Interior.ColorIndex = 17;
            workSheet.Cells[1, 2] = "Имя";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "Отчество";
            workSheet.Cells[1, 4].EntireColumn.ColumnWidth = 25;
            workSheet.Cells[1, 4].Interior.ColorIndex = 17;
            workSheet.Cells[1, 4] = "АдресПроживания";
            workSheet.Cells[1, 5].EntireColumn.ColumnWidth = 15;
            workSheet.Cells[1, 5].Interior.ColorIndex = 17;
            workSheet.Cells[1, 5] = "Должность";
            workSheet.Cells[1, 6].EntireColumn.ColumnWidth = 15;
            workSheet.Cells[1, 6].Interior.ColorIndex = 17;
            workSheet.Cells[1, 6] = "Логин";
            workSheet.Cells[1, 7].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 7].Interior.ColorIndex = 17;
            workSheet.Cells[1, 7] = "Пароль";
            workSheet.Cells[1, 8].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 8].Interior.ColorIndex = 17;
            workSheet.Cells[1, 8] = "КатегориПрав";
            workSheet.Cells[1, 9].EntireColumn.ColumnWidth = 15;
            workSheet.Cells[1, 9].Interior.ColorIndex = 17;
            workSheet.Cells[1, 9] = "НомерТелефона";
            int i = 2;
            try
            {
                using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext = new WpfApplicationEntity.MyClasses.MyDBContext())
                {
                    List<Employees> employes = WpfApplicationEntity.MyClasses.DatabaseRequest.GetEmployees(objectMyDBContext).ToList();
                    foreach (Employees employee in employes)
                    {
                        workSheet.Cells[i, 1].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 1] = employee.Surname;
                        workSheet.Cells[i, 2].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 2] = employee.Name;
                        workSheet.Cells[i, 3].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = employee.MiddelName;
                        workSheet.Cells[i, 4].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 4] = employee.adress;
                        workSheet.Cells[i, 5].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 5] = employee.Birthday;
                        workSheet.Cells[i, 6].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 6] = employee.Position;
                        workSheet.Cells[i, 7].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 7] = employee.Login;
                        workSheet.Cells[i, 8].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 8] = employee.Password;
                        workSheet.Cells[i, 9].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 9] = employee.Pass;
                        workSheet.Cells[i, 10].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 10] = employee.NumberTelephone;
                        i++;
                    }
                    string pathToXlsFile = Environment.CurrentDirectory + "\\Сотрудники.xls";
                    workSheet.SaveAs(pathToXlsFile);
                    exApp.Quit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void rapoGroupButton_Click(object sender, RoutedEventArgs e)
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "TimesNewRoman";
            workSheet.Cells[1, 1].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 1].Interior.ColorIndex = 17;
            workSheet.Cells[1, 1] = "Название";
            workSheet.Cells[1, 2].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 2].Interior.ColorIndex = 17;
            workSheet.Cells[1, 2] = "Тип";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "Цвет";
            workSheet.Cells[1, 4].EntireColumn.ColumnWidth = 25;
            workSheet.Cells[1, 4].Interior.ColorIndex = 17;
            workSheet.Cells[1, 4] = "Номер";
            int i = 2;
            try
            {
                using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext = new WpfApplicationEntity.MyClasses.MyDBContext())
                {
                    List<Transport> employes = WpfApplicationEntity.MyClasses.DatabaseRequest.GetTransport(objectMyDBContext).ToList();
                    foreach (Transport employee in employes)
                    {
                        workSheet.Cells[i, 1].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 1] = employee.Name;
                        workSheet.Cells[i, 2].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 2] = employee.type;
                        workSheet.Cells[i, 3].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = employee.Color;
                        workSheet.Cells[i, 4].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 4] = employee.Number;
                        i++;
                    }
                    string pathToXlsFile = Environment.CurrentDirectory + "\\Транспорт.xls";
                    workSheet.SaveAs(pathToXlsFile);
                    exApp.Quit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void raportorderButton_Click(object sender, RoutedEventArgs e)
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "TimesNewRoman";
            workSheet.Cells[1, 1].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 1].Interior.ColorIndex = 17;
            workSheet.Cells[1, 1] = "ДатаСтарта";
            workSheet.Cells[1, 2].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 2].Interior.ColorIndex = 17;
            workSheet.Cells[1, 2] = "Сумма";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "МестоДоставки";
            workSheet.Cells[1, 4].EntireColumn.ColumnWidth = 25;
            workSheet.Cells[1, 4].Interior.ColorIndex = 17;
            workSheet.Cells[1, 4] = "ОплатаДоставки";
            workSheet.Cells[1, 4].EntireColumn.ColumnWidth = 25;
            workSheet.Cells[1, 4].Interior.ColorIndex = 17;
            workSheet.Cells[1, 4] = "ФИО";
            int i = 2;
            try
            {
                using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext = new WpfApplicationEntity.MyClasses.MyDBContext())
                {
                    List<Order> employes = WpfApplicationEntity.MyClasses.DatabaseRequest.GetOrder(objectMyDBContext).ToList();
                    foreach (Order employee in employes)
                    {
                        workSheet.Cells[i, 1].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 1] = employee.DataStartOrder;
                        workSheet.Cells[i, 2].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 2] = employee.Sum;
                        workSheet.Cells[i, 3].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = employee.PlaceEndDelivery;
                        workSheet.Cells[i, 4].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 4] = employee.PaymentForDelivery;
                        workSheet.Cells[i, 4].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 4] = employee.FIOClient;
                        i++;
                    }
                    string pathToXlsFile = Environment.CurrentDirectory + "\\Заказ.xls";
                    workSheet.SaveAs(pathToXlsFile);
                    exApp.Quit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void reporpackButton_Click(object sender, RoutedEventArgs e)
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "TimesNewRoman";
            workSheet.Cells[1, 1].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 1].Interior.ColorIndex = 17;
            workSheet.Cells[1, 1] = "Масса";
            workSheet.Cells[1, 2].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 2].Interior.ColorIndex = 17;
            workSheet.Cells[1, 2] = "Наименование";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "Размер";
            int i = 2;
            try
            {
                using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext = new WpfApplicationEntity.MyClasses.MyDBContext())
                {
                    List<Package> employes = WpfApplicationEntity.MyClasses.DatabaseRequest.GetPackage(objectMyDBContext).ToList();
                    foreach (Package employee in employes)
                    {
                        workSheet.Cells[i, 1].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 1] = employee.weight;
                        workSheet.Cells[i, 2].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 2] = employee.Name;
                        workSheet.Cells[i, 3].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = employee.Size;
                        workSheet.Cells[i, 4].Interior.ColorIndex = 24;
                        i++;
                    }
                    string pathToXlsFile = Environment.CurrentDirectory + "\\Посылка.xls";
                    workSheet.SaveAs(pathToXlsFile);
                    exApp.Quit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void reportedelivButton_Click(object sender, RoutedEventArgs e)
        {
            Excel._Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1].EntireRow.Font.Bold = true;
            workSheet.Cells.EntireRow.Font.Size = 14;
            workSheet.Cells.EntireRow.Font.Name = "TimesNewRoman";
            workSheet.Cells[1, 1].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 1].Interior.ColorIndex = 17;
            workSheet.Cells[1, 1] = "ДатаОтправки";
            workSheet.Cells[1, 2].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 2].Interior.ColorIndex = 17;
            workSheet.Cells[1, 2] = "Маршрут";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "ОжидаемоеВермяДоставки";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "ВремяДоставки";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "ИмяИПуть";
            workSheet.Cells[1, 3].EntireColumn.ColumnWidth = 20;
            workSheet.Cells[1, 3].Interior.ColorIndex = 17;
            workSheet.Cells[1, 3] = "Посылка";
            int i = 2;
            try
            {
                using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext = new WpfApplicationEntity.MyClasses.MyDBContext())
                {
                    List<WpfApplicationEntity.MyClasses.DatabaseRequest.NewDelivery> employes = WpfApplicationEntity.MyClasses.DatabaseRequest.GetEmployeesWithSchedule(objectMyDBContext).ToList();
                    foreach (WpfApplicationEntity.MyClasses.DatabaseRequest.NewDelivery employee in employes)
                    {
                        workSheet.Cells[i, 1].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 1] = employee.DataStartDelivery;
                        workSheet.Cells[i, 2].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 2] = employee.Route;
                        workSheet.Cells[i, 3].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = employee.EstimaedDeliveryTime;
                        workSheet.Cells[i, 4].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = employee.TimeDelivery;
                        workSheet.Cells[i, 4].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = employee.Transport;
                        workSheet.Cells[i, 4].Interior.ColorIndex = 24;
                        workSheet.Cells[i, 3] = employee.Package;
                        workSheet.Cells[i, 4].Interior.ColorIndex = 24;

                        i++;
                    }
                    string pathToXlsFile = Environment.CurrentDirectory + "\\Доставка.xls";
                    workSheet.SaveAs(pathToXlsFile);
                    exApp.Quit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateGridAfterSearch(List<Employees> Source)
        {
            EmploGrid.ItemsSource = Source;
            EmploGrid.Columns[0].Header = "ID_Cотрудника";
            EmploGrid.Columns[1].Header = "Фамилия";
            EmploGrid.Columns[2].Header = "Имя";
            EmploGrid.Columns[3].Header = "Отчество";
            EmploGrid.Columns[4].Header = "Адрес";
            EmploGrid.Columns[5].Header = "ДатаРождения";
            EmploGrid.Columns[6].Header = "Должность";
            EmploGrid.Columns[7].Header = "Логин";
            EmploGrid.Columns[8].Visibility = Visibility.Hidden;
            EmploGrid.Columns[9].Visibility = Visibility.Hidden;
        }

        public struct WorkerSearchInfo
        {
            public int ID_employees { get; set; }
            public string NamesInfo { get; set; }
            public WorkerSearchInfo(int ID_employees, string NamesInfo)
                : this()
            {
                this.ID_employees = ID_employees;
                this.NamesInfo = NamesInfo;
            }
        }
        public List<Employees> SearchWorkers(string SearchValue)
        {
            var DefaultWorkersList = MyDBContext.DBContext.Employees.ToList();
            List<WorkerSearchInfo> workerSearchInfos = new List<WorkerSearchInfo>();
            List<Employees> SearchedWorkers = new List<Employees>();
            for (int i = 0; i < DefaultWorkersList.Count; i++)
            {
                workerSearchInfos.Add(new WorkerSearchInfo(DefaultWorkersList[i].IDEmpoyes, DefaultWorkersList[i].Surname + " " + DefaultWorkersList[i].Name + " " + DefaultWorkersList[i].MiddelName));
            }
            for (int i = 0; i < workerSearchInfos.Count; i++)
            {
                if (workerSearchInfos[i].NamesInfo.Contains(SearchValue))
                {
                    SearchedWorkers.Add(MyDBContext.DBContext.Employees.Find(workerSearchInfos[i].ID_employees));
                }
            }
            return SearchedWorkers;
        }
        private void SearchBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = String.Empty;
        }

        private void SearchBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "Поиск по ФИО";
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text.Length >= 3 && SearchBox.Text != "Поиск по ФИО")
            {
                List<Employees> SearchedWorkers = new List<Employees>();
                string SearchValue = SearchBox.Text;
                Thread SearchThread = new Thread(() => SearchedWorkers = SearchWorkers(SearchValue));
                SearchThread.Start();
                SearchThread.Join();
                UpdateGridAfterSearch(SearchedWorkers);
            }
            else
            {
                ShowAll();
            }
        }
        private void EmploButton_TextChanged(object sender, RoutedEventArgs e)
        {
            ExportImport.ExportDataBase();
        }
        private void ImpoButton_TextChanged(object sender, RoutedEventArgs e)
        {
            ExportImport.ImportDataBase();
        }
    }
}