using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace  WpfApplicationEntity.MyClasses
{
    class MyDBContext : DbContext
    {
        public MyDBContext() : base("DbConnectString") //сторка подключения
        {
        }
        public DbSet<Delivery> Delivery { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Package> Package { get; set; }
        public DbSet<Transport> Transport { get; set; }
        public static MyDBContext DBContext = new WpfApplicationEntity.MyClasses.MyDBContext();

    }
}