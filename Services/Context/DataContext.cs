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
        public DbSet<ManagerPropertiesModel> ManagerProperties { get; set; }
        public DbSet<ManagerFinanceModel> ManagerFinance { get; set; }
        public DbSet<ManagerDocumentsModel> ManagerDocuments { get; set; }
        public DbSet<PropertyExpenseModel> PropertyExpense { get; set; }
        public DbSet<PropertyIncomeModel> PropertyIncome { get; set; }
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

            // Seed dummy data for UserModel
            builder.Entity<UserModel>().HasData(
                new UserModel { ID = 1, Username = "john_doe", Salt = "salt1", Hash = "hash1", Email = "john@example.com", IsManager = true, SecurityAnswerSalt = "security_salt1", SecurityAnswerHash = "security_hash1", SecurityQuestionID = 1 },
                new UserModel { ID = 2, Username = "jane_smith", Salt = "salt2", Hash = "hash2", Email = "jane@example.com", IsManager = true, SecurityAnswerSalt = "security_salt2", SecurityAnswerHash = "security_hash2", SecurityQuestionID = 2 },
                new UserModel { ID = 3, Username = "alice_johnson", Salt = "salt3", Hash = "hash3", Email = "alice@example.com", IsManager = false, SecurityAnswerSalt = "security_salt3", SecurityAnswerHash = "security_hash3", SecurityQuestionID = 3 },
                new UserModel { ID = 4, Username = "bob_williams", Salt = "salt4", Hash = "hash4", Email = "bob@example.com", IsManager = false, SecurityAnswerSalt = "security_salt4", SecurityAnswerHash = "security_hash4", SecurityQuestionID = 1 }
            );

            // Seed dummy data for ManagerModel
            builder.Entity<ManagerModel>().HasData(
                new ManagerModel { ID = 1, FirstName = "John", LastName = "Doe", Phone = "123-456-7890", Role = "Manager", Location = "New York", Language = "English", UserID = 1 },
                new ManagerModel { ID = 2, FirstName = "Jane", LastName = "Smith", Phone = "987-654-3210", Role = "Manager", Location = "Los Angeles", Language = "Spanish", UserID = 2 }
            );

            // Seed dummy data for TenantModel
            builder.Entity<TenantModel>().HasData(
                new TenantModel { ID = 1, FirstName = "Alice", LastName = "Johnson", Phone = "123-456-7890", LeaseType = "Annual", LeaseStart = DateTime.Now, LeaseEnd = DateTime.Now.AddYears(1), UserID = 3, RoomInfoID = null, PropertyInfoID = 1, DocumentsID = 1 },
                new TenantModel { ID = 2, FirstName = "Bob", LastName = "Williams", Phone = "987-654-3210", LeaseType = "Monthly", LeaseStart = DateTime.Now, LeaseEnd = DateTime.Now.AddMonths(1), UserID = 4, RoomInfoID = 1, PropertyInfoID = 2, DocumentsID = 2 }
            );

            // Seed dummy data for TenantPaymentInfoModel
            builder.Entity<TenantPaymentInfoModel>().HasData(
                new TenantPaymentInfoModel { ID = 1, Balance = 1000, DueDate = DateTime.Now.AddDays(7), PaymentRecieved = false, DaysRemaining = 7, TenantID = 1 },
                new TenantPaymentInfoModel { ID = 2, Balance = 1500, DueDate = DateTime.Now.AddDays(9), PaymentRecieved = false, DaysRemaining = 9, TenantID = 2 }
            );

            // Seed dummy data for RoomInfoModel
            builder.Entity<RoomInfoModel>().HasData(
                new RoomInfoModel { ID = 1, RoomRent = 500, PropertyInfoID = 2 },
                new RoomInfoModel { ID = 2, RoomRent = 600, PropertyInfoID = 2 }
            );

            // Seed dummy data for PropertyInfoModel
            builder.Entity<PropertyInfoModel>().HasData(
                new PropertyInfoModel { ID = 1, HouseNumber = "123", Street = "Main St", City = "Anytown", Zip = "12345", State = "CA", HouseOrRoomType = "House", HouseRent = 2000, Rooms = 3, Baths = 2, Sqft = 1800, AmenFeatList = "Swimming Pool, Gym", Description = "Spacious family house", Expenses = 1800, Income = 2500 },
                new PropertyInfoModel { ID = 2, HouseNumber = "456", Street = "Oak St", City = "Otherville", Zip = "54321", State = "NY", HouseOrRoomType = "Rooms", HouseRent = 1500, Rooms = 2, Baths = 1, Sqft = 1000, AmenFeatList = "Laundry, Parking, Backyard", Description = "Cozy condo with rooms for rent", Expenses = 1450, Income = 1800 },
                new PropertyInfoModel { ID = 3, HouseNumber = "789", Street = "Pine St", City = "Smalltown", Zip = "67890", State = "TX", HouseOrRoomType = "House", HouseRent = 1800, Rooms = 2, Baths = 1, Sqft = 1200, AmenFeatList = "Garage, Patio", Description = "Charming cottage", Expenses = 1500, Income = 2000 },
                new PropertyInfoModel { ID = 4, HouseNumber = "101", Street = "Maple St", City = "Villageton", Zip = "98765", State = "FL", HouseOrRoomType = "House", HouseRent = 1200, Rooms = 2, Baths = 1, Sqft = 800, AmenFeatList = "Utilities Included", Description = "Small home", Expenses = 1180, Income = 1400 }
            );

            // Seed dummy data for PropertyExpenseModel
            builder.Entity<PropertyExpenseModel>().HasData(
                new PropertyExpenseModel { ID = 1, Mortage = 1500, MaintenceCosts = 300, PropertyInfoID = 1 },
                new PropertyExpenseModel { ID = 2, Mortage = 1200, MaintenceCosts = 250, PropertyInfoID = 2 },
                new PropertyExpenseModel { ID = 3, Mortage = 1300, MaintenceCosts = 200, PropertyInfoID = 3 },
                new PropertyExpenseModel { ID = 4, Mortage = 1000, MaintenceCosts = 180, PropertyInfoID = 4 }
            );

            // Seed dummy data for PropertyIncomeModel
            builder.Entity<PropertyIncomeModel>().HasData(
                new PropertyIncomeModel { ID = 1, Rent = 2500, PropertyInfoID = 1 },
                new PropertyIncomeModel { ID = 2, Rent = 1800, PropertyInfoID = 2 },
                new PropertyIncomeModel { ID = 3, Rent = 2000, PropertyInfoID = 3 },
                new PropertyIncomeModel { ID = 4, Rent = 1400, PropertyInfoID = 4 }
            );

            // Seed dummy data for ManagerPropertiesModel
            builder.Entity<ManagerPropertiesModel>().HasData(
                new ManagerPropertiesModel { ID = 1, ManagerID = 1, PropertyInfoID = 1 },
                new ManagerPropertiesModel { ID = 2, ManagerID = 1, PropertyInfoID = 2 },
                new ManagerPropertiesModel { ID = 3, ManagerID = 2, PropertyInfoID = 3 },
                new ManagerPropertiesModel { ID = 4, ManagerID = 2, PropertyInfoID = 4 }
            );

            // Seed dummy data for ManagerFinanceModel
            builder.Entity<ManagerFinanceModel>().HasData(
                new ManagerFinanceModel { ID = 1, MonthlyRentRecieved = 3000, ManagerID = 1 },
                new ManagerFinanceModel { ID = 2, MonthlyRentRecieved = 2500, ManagerID = 2 },
                new ManagerFinanceModel { ID = 3, MonthlyRentRecieved = 2800, ManagerID = 3 },
                new ManagerFinanceModel { ID = 4, MonthlyRentRecieved = 2000, ManagerID = 4 }
            );

            // Seed dummy data for DocumentsModel
            builder.Entity<DocumentsModel>().HasData(
                new DocumentsModel { ID = 1, Name = "LeaseAgreement", Type = "Lease", Content = new byte[] { 0x01, 0x02, 0x03 }, UploadDate = DateTime.Now },
                new DocumentsModel { ID = 2, Name = "LeaseAgreement", Type = "Lease", Content = new byte[] { 0x04, 0x05, 0x06 }, UploadDate = DateTime.Now },
                new DocumentsModel { ID = 3, Name = "ManagerList", Type = "Manager", Content = new byte[] { 0x04, 0x05, 0x06 }, UploadDate = DateTime.Now },
                new DocumentsModel { ID = 4, Name = "ManagerDoc", Type = "Finance", Content = new byte[] { 0x04, 0x05, 0x06 }, UploadDate = DateTime.Now }
            );

            // Seed dummy data for ManagerDocumentsModel
            builder.Entity<ManagerDocumentsModel>().HasData(
                new ManagerDocumentsModel { ID = 1, ManagerID = 1, DocumentsID = 3 },
                new ManagerDocumentsModel { ID = 2, ManagerID = 1, DocumentsID = 4 }
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