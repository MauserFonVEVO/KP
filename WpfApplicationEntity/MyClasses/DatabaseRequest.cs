using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplicationEntity.MyClasses
{
    class DatabaseRequest
    {
        public class NewDelivery
        {
            public int IDAppointment { get; set; }
            public string DataStartDelivery { get; set; }
            public string Route { get; set; }
            public int EstimaedDeliveryTime{get; set;}
            public string TimeDelivery { get; set; }
            public string Transport { get; set; }
            public string Package { get; set; }
            public NewDelivery(int IDAppointment, string DataStartDelivery, string Route, int EstimaedDeliveryTime, string TimeDelivery, string Transport, string Package)
            {
                this.IDAppointment = IDAppointment;
                this.DataStartDelivery = DataStartDelivery;
                this.Route = Route;
                this.EstimaedDeliveryTime = EstimaedDeliveryTime;
                this.TimeDelivery = TimeDelivery;
                this.Transport = Transport;
                this.Package = Package;
            }
        }
        #region NewEmployees
        public class NewEmployees
        {
            public int IDEmpoyes { get; set; }
            public string Surname { get; set; }
            public string Name { get; set; }
            public string MiddelName { get; set; }
            public string adress { get; set; }
            public string Birthday { get; set; }
            public string Position { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public string Pass { get; set; }
            public string NumberTelephone { get; set; }
            public string CompanyName { get; set; }
            /// <summary>
            /// Сотрудник
            /// </summary>
            /// <param name="Id">id сотрудника</param>
            /// <param name="Name">имя сорудника</param>
            /// <param name="Surname">Фамилия сотрудника</param>
            /// <param name="MiddelName">Отчество сотрудника</param>
            /// <param name="adress">Адрес прживания</param>
            /// <param name="Birthday">Дата рождения</param>
            /// <param name="Position">Должность</param>
            /// <param name="Login">Логин</param>
            /// <param name="Password">Пароль</param>
            /// <param name="Pass">Категория прав</param>
            /// <param name="NumberTelephone">Номер телефона</param>
            /// <param name="CompanyName">название компании</param>
            public NewEmployees(int Id, string Name, string Surname, string MiddelName, string adress, string Birthday,
                 string Position, string Login, string Password, string Pass, string NumberTelephone, string CompanyName)
            {
                this.IDEmpoyes = Id;
                this.Name = Name;
                this.Surname = Surname;
                this.MiddelName = MiddelName;
                this.adress = adress;
                this.Birthday = Birthday;
                this.Position = Position;
                this.Login = Login;
                this.Password = Password;
                this.Pass = Pass;
                this.NumberTelephone = NumberTelephone;
                this.CompanyName = CompanyName;
            }
        }
        #endregion
        #region Database
        static DatabaseRequest()
        {
        }
        public static bool IsUser(MyDBContext objectMyDBContext, string login, string password)
        {
            var tmp = (
                from tmpUser in objectMyDBContext.Employees.ToList<Employees>()
                where tmpUser.Login.CompareTo(login) == 0 && tmpUser.Password.CompareTo(password) == 0
                select tmpUser
                      ).ToList();
            if (tmp.Count == 1)
                return true;
            return false;
        }
        public static IEnumerable<NewEmployees> GetEmployeess(MyDBContext objectMyDBContext)
        {
            return (
                from tmpEmployees in objectMyDBContext.Employees.ToList<Employees>()
                from tmpGroup in objectMyDBContext.Order.ToList<Order>()
                where tmpEmployees.IDEmpoyes == tmpGroup.IDOrder
                select (
                new NewEmployees//(tmpEmployees.IDEmpoyes, tmpEmployees.Name, tmpEmployees.Surname, tmpEmployees.MiddelName, tmpEmployees.adress, tmpEmployees.Birthday, tmpEmployees.Login, tmpEmployees.Password, tmpEmployees.Pass, tmpEmployees.NumberTelephone, tmpEmployees.Position, tmpEmployees.IDCompany)
                (tmpEmployees.IDEmpoyes, tmpEmployees.Name, tmpEmployees.Surname, tmpEmployees.MiddelName, tmpEmployees.adress, tmpEmployees.Birthday,
                tmpEmployees.Position, tmpEmployees.Login, tmpEmployees.Password, tmpEmployees.Pass, tmpEmployees.NumberTelephone, "Название компании")
                )
                       ).ToList();
        }
        #endregion
        #region TheNewOrder
        public static IEnumerable<Order> GetOrder(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Order.ToList();
        }
        public static Order GetOrderID(MyDBContext objectMyDBContext, int ID)
        {
            return (from tempGroup in objectMyDBContext.Order.ToList<Order>()
                    where tempGroup.IDOrder == ID
                    select tempGroup).SingleOrDefault();
        }
        #endregion 
        #region Employess
        public static IEnumerable<Employees> GetEmployees(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Employees.ToList();
        }
        public static Employees GetEmployeesID(MyDBContext objectMyDBContext, int ID)
        {
            return (from tempGroup in objectMyDBContext.Employees.ToList<Employees>()
                    where tempGroup.IDEmpoyes == ID
                    select tempGroup).SingleOrDefault();
        }
        #endregion
        #region Delivery
        public static IEnumerable<Delivery> GetDelivery(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Delivery.ToList();
        }
        public static Delivery GetDeliveryID(MyDBContext objectMyDBContext, int ID)
        {
            return (from tempGroup in objectMyDBContext.Delivery.ToList<Delivery>()
                    where tempGroup.IDAppointment == ID
                    select tempGroup).SingleOrDefault();
        }
          public static IEnumerable<NewDelivery> GetEmployeesWithSchedule(MyDBContext objectMyDBContext)
        {
            return (
                from tmpDelivery in objectMyDBContext.Delivery.ToList<Delivery>()
                from tmpTransport in objectMyDBContext.Transport.ToList<Transport>()
                from tmpPackage in objectMyDBContext.Package.ToList<Package>()
                where tmpDelivery.IDTransport == tmpTransport.IDTransport
                where tmpDelivery.IDPackage == tmpPackage.IDPackage
                select (
                new NewDelivery(tmpDelivery.IDAppointment, tmpDelivery.DataStartDelivery, tmpDelivery.Route, tmpDelivery.EstimaedDeliveryTime, tmpDelivery.TimeDelivery, tmpTransport.Name, tmpPackage.Name)
                )
                       ).ToList();
        }
        #endregion Delivery
        #region Transport
        public static IEnumerable<Transport> GetTransport(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Transport.ToList();
        }
        public static Transport GetTransportByID(MyDBContext objectMyDBContext, int ID)
        {
            return (from tempGroup in objectMyDBContext.Transport.ToList<Transport>()
                    where tempGroup.IDTransport == ID
                    select tempGroup).SingleOrDefault();
        }
        #endregion Transport
        #region Package
        public static IEnumerable<Package> GetPackage(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Package.ToList();
        }
        public static Package GetPackageID(MyDBContext objectMyDBContext, int ID)
        {
            return (from tempGroup in objectMyDBContext.Package.ToList<Package>()
                    where tempGroup.IDPackage == ID
                    select tempGroup).SingleOrDefault();
        }
      
        #endregion

    }
}
