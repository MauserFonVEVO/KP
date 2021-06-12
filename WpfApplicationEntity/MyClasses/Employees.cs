using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace WpfApplicationEntity.MyClasses
{
    public class Employees
    {
        [Key]
        public int IDEmpoyes { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string MiddelName { get; set; }
        [Required]
        public string adress { get; set; }
        [Required]
        public string Birthday { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Pass { get; set; }
        [Required]
        public string NumberTelephone { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Delivery> Delivery { get; set; }
    }
}
