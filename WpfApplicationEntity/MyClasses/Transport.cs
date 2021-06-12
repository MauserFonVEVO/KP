using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace WpfApplicationEntity.MyClasses
{
    public class Transport
    {
        [Key]
        public int IDTransport { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string type { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public int Number { get; set; }
        public virtual ICollection<Delivery> Delivery { get; set; }
        public string GetNameType
        {
            get
            {
                return Name + " " + type;
            }
        }
    }
}
