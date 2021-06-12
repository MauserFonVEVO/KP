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
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        private bool add_edit;
        private int id;
        string FIOClient;
        public AddOrder()
        {
            InitializeComponent();
        }
        public AddOrder(bool add_edit, int id = 0)
        {
            InitializeComponent();
            this.add_edit = add_edit;
            this.id = id;
            if (this.add_edit == false)
            {
                using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext = new WpfApplicationEntity.MyClasses.MyDBContext())
                {
                    if (this.add_edit == false)
                    {
                        WpfApplicationEntity.MyClasses.Order Order = WpfApplicationEntity.MyClasses.DatabaseRequest.GetOrderID(objectMyDBContext, this.id);
                        textAddIDOrder.Text = Convert.ToString(Order.IDOrder);
                        textAddDataStartOrder.Text = Order.DataStartOrder;
                        textAddSum.Text = Convert.ToString(Order.Sum);
                        textAddPlaceEndDelivery.Text = Order.PlaceEndDelivery;
                        textAddPaymentForDelivery.Text = Order.PaymentForDelivery;
                        textAddFIOClient.Text = Order.FIOClient;
                    }
                    ButtonAddEditOrder.Content = "Изменить";
                }
            }
        }
        private void ButtonAddEditOrder_Click(object sender, RoutedEventArgs e)
        {
             try
            {
                if (this.IsDataCorrcet() == true)
                {
                    using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext =
                            new WpfApplicationEntity.MyClasses.MyDBContext())
                    {
                        WpfApplicationEntity.MyClasses.Order objectOrder = new WpfApplicationEntity.MyClasses.Order();
                        objectOrder.DataStartOrder = Convert.ToString(textAddDataStartOrder.Text);
                        objectOrder.Sum =  Convert.ToInt32(textAddSum.Text);
                        objectOrder.PlaceEndDelivery = textAddPlaceEndDelivery.Text;
                        objectOrder.PaymentForDelivery = textAddPaymentForDelivery.Text;
                        objectOrder.FIOClient = textAddFIOClient.Text;
                        if (this.add_edit == true)
                        {
                            objectMyDBContext.Order.Add(objectOrder);
                        }
                        else
                        {
                            WpfApplicationEntity.MyClasses.Order objectTransportFromDataBase = new WpfApplicationEntity.MyClasses.Order();
                            objectTransportFromDataBase = WpfApplicationEntity.MyClasses.DatabaseRequest.GetOrderID(objectMyDBContext, this.id);
                            objectMyDBContext.Entry(objectTransportFromDataBase).CurrentValues.SetValues(objectOrder);
                        }
                        objectMyDBContext.SaveChanges();
                        this.DialogResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА (Заказ)", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool IsDataCorrcet()
        {
            return  textAddDataStartOrder.Text != string.Empty
                    && textAddSum.Text != string.Empty
                    && textAddPlaceEndDelivery.Text != string.Empty
                    && textAddPaymentForDelivery.Text != string.Empty
                    && textAddFIOClient.Text != string.Empty;
        }
            /*if (this.add_edit == true)
                if (textAddIDOrder.Text != string.Empty
                    && textAddDataStartOrder.Text != string.Empty
                    && textAddSum.Text != string.Empty
                    && textAddPlaceEndDelivery.Text != string.Empty
                    && textAddPaymentForDelivery.Text != string.Empty
                    && textAddFIOClient.Text != string.Empty)
                {
                    WpfApplicationEntity.MyClasses.Order objectOrder = new WpfApplicationEntity.MyClasses.Order();
                    objectOrder.IDOrder = Convert.ToInt32(textAddIDOrder.Text);
                    objectOrder.DataStartOrder = textAddDataStartOrder.Text;
                    objectOrder.Sum = Convert.ToInt32(textAddSum.Text);
                    objectOrder.PlaceEndDelivery = textAddPlaceEndDelivery.Text;
                    objectOrder.PaymentForDelivery = textAddPaymentForDelivery.Text;
                    objectOrder.FIOClient = textAddFIOClient.Text;
                    try
                    {
                        using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext = new WpfApplicationEntity.MyClasses.MyDBContext())
                        {
                            objectMyDBContext.Order.Add(objectOrder);
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("Заказ добавлен");
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
        }
    }


