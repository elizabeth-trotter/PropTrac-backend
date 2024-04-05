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
        public DbSet<TenantPaymentInfoModel> TenantPaymentInfo { get; set; }
        public DbSet<RoomInfoModel> RoomInfo { get; set; }
        public DbSet<PropertyInfoModel> PropertyInfo { get; set; }
        public DbSet<DocumentsModel> Documents { get; set; }
        public DbSet<SecurityQuestionModel> SecurityQuestion { get; set; }
        // add additional models here once known

        // creating constructor to inject models into our database
        public DataContext(DbContextOptions options) : base(options) { }

        // this function will build out our table in the database
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed predefined security questions
            builder.Entity<SecurityQuestionModel>().HasData(
                new SecurityQuestionModel { ID = 1, Question = "What is the name of your first pet?" },
                new SecurityQuestionModel { ID = 2, Question = "What was your favorite teacher's name?" },
                new SecurityQuestionModel { ID = 3, Question = "What was the name of your first stuffed animal?" }
                // Add more questions as needed
            );

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