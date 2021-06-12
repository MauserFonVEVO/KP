using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace WpfApplicationEntity.MyClasses
{
    public class Package
    {
        [Key]
        public int IDPackage { get; set; }
        [Required]
        public int weight { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Size { get; set; }
        public string GetPackageName
        {
            get
            {
                return Name + ' ' + Size;
            }            
        }
    }
}
