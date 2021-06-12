using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace WpfApplicationEntity.MyClasses
{
    public class Delivery
    {
        [Key]
        public int IDAppointment { get; set; }
        [Required]
        public string DataStartDelivery { get; set; }
        [Required]
        public string Route { get; set; }
        [Required]
        public int EstimaedDeliveryTime { get; set; }
        [Required]
        public string TimeDelivery { get; set; }
        public int IDTransport { get; set; }
        public virtual Transport Transport { get; set; }
        public int IDPackage { get; set; }
        public virtual Package Package { get; set; }
        public Delivery()
        {
        }
        public Delivery(string DataStartDelivery, string Route, int EstimaedDeliveryTime, string TimeDelivery, Transport Transport, Package Package, int IDAppointment = 0)
        {
            this.DataStartDelivery = DataStartDelivery;
            this.Route = Route;
            this.EstimaedDeliveryTime = EstimaedDeliveryTime;
            this.TimeDelivery = TimeDelivery;
          //this.Employees = Employees;
            this.IDTransport = Transport.IDTransport;
            this.IDPackage = Package.IDPackage;
            this.IDAppointment = IDAppointment;
        }
    }
}
