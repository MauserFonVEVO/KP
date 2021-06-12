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
    /// Логика взаимодействия для AddTransport.xaml
    /// </summary>
    public partial class AddTransport : Window
    {
        private bool add_edit;
        private int id;
        string Type;
        public AddTransport()
        {
            InitializeComponent();
        }
        public AddTransport(bool add_edit, int id = 0)
        {
            InitializeComponent();
            this.add_edit = add_edit;
            this.id = id;
            if (this.add_edit == false)
            {
                using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext = new WpfApplicationEntity.MyClasses.MyDBContext())
                {
                    WpfApplicationEntity.MyClasses.Transport transport = WpfApplicationEntity.MyClasses.DatabaseRequest.GetTransportByID(objectMyDBContext, this.id);
                    textAddNaame.Text = transport.Name;
                    textAddtype.Text = transport.type;
                    textAddColor.Text = transport.Color;
                    textAddNumber.Text = Convert.ToString(transport.Number);
                }
                ButtonAddEditTransport.Content = "Изменить";
            }
        }
        private void ButtonAddEditTransport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.IsDataCorrcet() == true)
                {
                    using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext =
                            new WpfApplicationEntity.MyClasses.MyDBContext())
                    {
                        WpfApplicationEntity.MyClasses.Transport objectTrans = new WpfApplicationEntity.MyClasses.Transport();
                        objectTrans.IDTransport = Convert.ToInt32(textAddIDTransport.Text);
                        objectTrans.Name = textAddNaame.Text;
                        objectTrans.type = textAddtype.Text;
                        objectTrans.Color = textAddColor.Text;
                        objectTrans.Number = Convert.ToInt32(textAddNumber.Text);
                        if (this.add_edit == true)
                        {
                            objectMyDBContext.Transport.Add(objectTrans);
                        }
                        else
                        {
                            WpfApplicationEntity.MyClasses.Transport objectTransportFromDataBase = new WpfApplicationEntity.MyClasses.Transport();
                            objectTransportFromDataBase = WpfApplicationEntity.MyClasses.DatabaseRequest.GetTransportByID(objectMyDBContext, this.id);
                            objectMyDBContext.Entry(objectTransportFromDataBase).CurrentValues.SetValues(objectTrans);
                        }
                        objectMyDBContext.SaveChanges();
                        this.DialogResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА (Группа)", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool IsDataCorrcet()
        {
            return textAddIDTransport.Text != string.Empty    
                    && textAddNaame.Text != string.Empty
                    && textAddtype.Text != string.Empty
                    && textAddColor.Text != string.Empty
                    && textAddNumber.Text != string.Empty;
        }
    }
}
