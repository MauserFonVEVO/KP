using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace WpfApplicationEntity.MyClasses
{
    public class Order
    {
        [Key]
        public int IDOrder { get; set; }
        [Required]
        public string DataStartOrder { get; set; }
        [Required]
        public int Sum { get; set; }
        [Required]
        public string PlaceEndDelivery { get; set; }
        [Required]
        public string PaymentForDelivery { get; set; }
        [Required]
        public string FIOClient { get; set; }
        public virtual ICollection<Package> Package { get; set; }
        public virtual ICollection<Delivery> Delivery { get; set; }
    }
}
