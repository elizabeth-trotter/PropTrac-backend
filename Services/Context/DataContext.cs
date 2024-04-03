using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropTrac_backend.Models;

namespace PropTrac_backend.Services.Context
{
    public class DataContext : DbContext
    {
        public DbSet<UserModel> UserInfo { get; set; }
        public DbSet<TenantModel> Tenants { get; set; }
        public DbSet<ManagerModel> Managers { get; set; }
        // add additional models here once known

        // creating constructor to inject models into our database
        public DataContext(DbContextOptions options) : base(options) { }

        // this function will build out our table in the database
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure relationships if necessary
            // builder.Entity<TenantModel>()
            //     .HasOne(t => t.User)
            //     .WithOne(u => u.Tenant)
            //     .HasForeignKey<TenantModel>(t => t.UserID);

            // builder.Entity<ManagerModel>()
            //     .HasOne(m => m.User)
            //     .WithOne(u => u.Manager)
            //     .HasForeignKey<ManagerModel>(m => m.UserID);
        }
    }
}