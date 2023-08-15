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
        public DbSet<Jobrespons> Jobresponses { get; set; }
        public DbSet<orderdetail> Orderdetails { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Outlet> Outlets { get; set; }
        public DbSet<Dinetable> Dinetables { get; set; }
        public DbSet<contact> contacts { get; set; }
        public DbSet<Carrer> Carrers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }


    }
}