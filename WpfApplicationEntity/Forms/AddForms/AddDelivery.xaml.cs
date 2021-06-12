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
    /// Логика взаимодействия для AddDelivery.xaml
    /// </summary>
    public partial class AddDelivery : Window
    {
        private bool add_edit;
        private int id;
        string Route;
        public AddDelivery()
        {
            InitializeComponent();
        }
        public AddDelivery(bool add_edit, int id = 0)
        {
            InitializeComponent();
            this.add_edit = add_edit;
            this.id = id;
            using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext = new WpfApplicationEntity.MyClasses.MyDBContext())
            {
                if (this.add_edit == false)
                {
                    WpfApplicationEntity.MyClasses.Delivery Delivery = WpfApplicationEntity.MyClasses.DatabaseRequest.GetDeliveryID(objectMyDBContext, this.id);
                    textAddDataStartDelivery.Text = Convert.ToString(Delivery.DataStartDelivery);
                    textAddRoute.Text = Delivery.Route;
                    textAddEstimaedDeliveryTime.Text = Convert.ToString(Delivery.EstimaedDeliveryTime);
                    textAddTimeDelivery.Text = Delivery.TimeDelivery;
                    
                    ButtonAddEditDelivery.Content = "Изменить";
                }
                comboBoxAddEditDelviTransport.ItemsSource = WpfApplicationEntity.MyClasses.DatabaseRequest.GetTransport(objectMyDBContext);
                comboBoxAddEditDelviTransport.Text = "{Binging GetNameType}";
                comboBoxAddEditDelivAppointment.ItemsSource = WpfApplicationEntity.MyClasses.DatabaseRequest.GetPackage(objectMyDBContext);
                comboBoxAddEditDelivAppointment.Text = "{Binging GetPackageName}";
            }
        }
        private void ButtonAddEditDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.IsDataCorrcet() == true)
                {
                    using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext =
                            new WpfApplicationEntity.MyClasses.MyDBContext())
                    {
                        WpfApplicationEntity.MyClasses.Delivery objectDeliv = new WpfApplicationEntity.MyClasses.Delivery(
                        Convert.ToString(textAddDataStartDelivery.Text),
                        textAddRoute.Text,
                        Convert.ToInt32(textAddEstimaedDeliveryTime.Text),
                        textAddTimeDelivery.Text,
                       (WpfApplicationEntity.MyClasses.Transport)comboBoxAddEditDelviTransport.SelectedItem,
                       (WpfApplicationEntity.MyClasses.Package)comboBoxAddEditDelivAppointment.SelectedItem
                        );
                        if (this.add_edit == true)
                        {
                            objectMyDBContext.Delivery.Add(objectDeliv);
                        }
                        else
                        {
                            WpfApplicationEntity.MyClasses.Delivery objectTransportFromDataBase = new WpfApplicationEntity.MyClasses.Delivery();
                            objectTransportFromDataBase = WpfApplicationEntity.MyClasses.DatabaseRequest.GetDeliveryID(objectMyDBContext, this.id);
                            objectMyDBContext.Entry(objectTransportFromDataBase).CurrentValues.SetValues(objectDeliv);
                        }
                        objectMyDBContext.SaveChanges();
                        this.DialogResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА (Доставка)", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool IsDataCorrcet()
        {
            return textAddDataStartDelivery.Text != string.Empty
                    && textAddRoute.Text != string.Empty
                    && textAddEstimaedDeliveryTime.Text != string.Empty
                    && textAddTimeDelivery.Text != string.Empty;
        }
    }
}



/*if (textAddDataStartDelivery.Text != string.Empty)
            {
                WpfApplicationEntity.MyClasses.Delivery objectDeliv = new WpfApplicationEntity.MyClasses.Delivery();
                objectDeliv.DataStartDelivery = Convert.ToString(textAddDataStartDelivery.Text);
                objectDeliv.Route = textAddRoute.Text;
                objectDeliv.EstimaedDeliveryTime = Convert.ToInt32(textAddEstimaedDeliveryTime.Text);
                objectDeliv.TimeDelivery = textAddTimeDelivery.Text;
                //objectDeliv.IDEmployees = Convert.ToInt32(textAddIDEmployees.Text);
                try
                {
                    using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext = new WpfApplicationEntity.MyClasses.MyDBContext())
                    {
                        objectMyDBContext.Delivery.Add(objectDeliv);
                        objectMyDBContext.SaveChanges();
                    }
                    MessageBox.Show("Доставка добавлен");
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