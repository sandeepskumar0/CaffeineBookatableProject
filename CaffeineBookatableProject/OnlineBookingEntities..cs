using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CaffeineBookatableProject.Models;

namespace CaffeineBookatableProject
{
    public class OnlineBookingEntities : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<order> Order { get; set; }
        public DbSet<orderdetail> Orderdetails { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Outlet> Outlets { get; set; }
        public DbSet<reserve> Reserve { get; set; }


    }
}