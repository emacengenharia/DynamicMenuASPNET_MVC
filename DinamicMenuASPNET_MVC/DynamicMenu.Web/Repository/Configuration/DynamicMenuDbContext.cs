using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DynamicMenu.Web.Models;

namespace DynamicMenu.Web.Repository.Configuration
{
    public class DynamicMenuDbContext : DbContext
    {
        public DynamicMenuDbContext() : base("DynamicMenuStrConn")
        {
            
        }
        public DbSet<Menu> Menus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);


        }

    }
}