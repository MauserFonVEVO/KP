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
    /// Логика взаимодействия для AddPackage.xaml
    /// </summary>
    public partial class AddPackage : Window
    {
        private bool add_edit;
        private int id;
        string NameANDRoute;
        public AddPackage()
        {
            InitializeComponent();
        }
        public AddPackage(bool add_edit, int id = 0)
        {
            InitializeComponent();
            this.add_edit = add_edit;
            this.id = id;
            if (this.add_edit == false)
            {
                using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext = new WpfApplicationEntity.MyClasses.MyDBContext())
                {
                    WpfApplicationEntity.MyClasses.Package Package = WpfApplicationEntity.MyClasses.DatabaseRequest.GetPackageID(objectMyDBContext, this.id);
                    textAddIDPackage.Text = Convert.ToString(Package.IDPackage);
                    textAddweight.Text = Convert.ToString(Package.weight);
                    textAddNamePackage.Text = Package.Name;
                    textAddSize.Text = Package.Size;
                }
                ButtonAddEditPackage.Content = "Изменить";
            }
        }
        private void ButtonAddEditPackage_Click(object sender, RoutedEventArgs e)
        {
             try
            {
                if (this.IsDataCorrcet() == true)
                {
                    using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext =
                            new WpfApplicationEntity.MyClasses.MyDBContext())
                    {
                        WpfApplicationEntity.MyClasses.Package objectLootBoxes = new WpfApplicationEntity.MyClasses.Package();
                        objectLootBoxes.IDPackage = Convert.ToInt32(textAddIDPackage.Text);
                        objectLootBoxes.weight = Convert.ToInt32(textAddweight.Text);
                        objectLootBoxes.Name = textAddNamePackage.Text;
                        objectLootBoxes.Size = textAddSize.Text;
                        if (this.add_edit == true)
                        {
                            objectMyDBContext.Package.Add(objectLootBoxes);
                        }
                        else
                        {
                            WpfApplicationEntity.MyClasses.Package objectTransportFromDataBase = new WpfApplicationEntity.MyClasses.Package();
                            objectTransportFromDataBase = WpfApplicationEntity.MyClasses.DatabaseRequest.GetPackageID(objectMyDBContext, this.id);
                            objectMyDBContext.Entry(objectTransportFromDataBase).CurrentValues.SetValues(objectLootBoxes);
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
            return textAddIDPackage.Text != string.Empty
                    && textAddweight.Text != string.Empty
                    && textAddNamePackage.Text != string.Empty
                    && textAddSize.Text != string.Empty;
        }
        }
    }






/*if (this.add_edit == true)
                if (textAddIDPackage.Text != string.Empty
                    && textAddweight.Text != string.Empty
                    && textAddNamePackage.Text != string.Empty
                    && textAddSize.Text != string.Empty)
                {
                    WpfApplicationEntity.MyClasses.Package objectLootBoxes = new WpfApplicationEntity.MyClasses.Package();
                    objectLootBoxes.IDPackage = Convert.ToInt32(textAddIDPackage.Text);
                    objectLootBoxes.weight = Convert.ToInt32(textAddweight.Text);
                    objectLootBoxes.Name = textAddNamePackage.Text;
                    objectLootBoxes.Size = textAddSize.Text;
                    try
                    {
                        using (WpfApplicationEntity.MyClasses.MyDBContext objectMyDBContext = new WpfApplicationEntity.MyClasses.MyDBContext())
                        {
                            objectMyDBContext.Package.Add(objectLootBoxes);
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("Посылка добавлена");
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
