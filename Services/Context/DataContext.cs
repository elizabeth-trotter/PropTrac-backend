using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropTrac_backend.Models;
using PropTrac_backend.Models.Property;

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
        public DbSet<MonthlyPropertyFinanceModel> MonthlyPropertyFinance { get; set; }
        public DbSet<PropertyExpenseModel> PropertyExpense { get; set; }
        public DbSet<PropertyRevenueModel> PropertyRevenue { get; set; }
        public DbSet<PropertyMaintenanceModel> PropertyMaintenance { get; set; }
        public DbSet<MaintenanceModel> Maintenance { get; set; }
        // add additional models here once known

        // creating constructor to inject models into our database
        public DataContext(DbContextOptions options) : base(options) { }

        // this function will build out our table in the database
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configure relationships if necessary
            // builder.Entity<TenantModel>()
            //     .HasOne(t => t.User)
            //     .WithOne(u => u.Tenant)
            //     .HasForeignKey<TenantModel>(t => t.UserID);

            // builder.Entity<ManagerModel>()
            //     .HasOne(m => m.User)
            //     .WithOne(u => u.Manager)
            //     .HasForeignKey<ManagerModel>(m => m.UserID);


            // Configuration for model relationships, keys, indexes, etc.
            // builder.Entity<ManagerDocumentsModel>()
            //     .HasKey(md => md.ID);

            // builder.Entity<ManagerDocumentsModel>()
            //     .HasOne(md => md.Manager)
            //     .WithMany(m => m.ManagerDocuments)
            //     .HasForeignKey(md => md.ManagerID);

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
                new PropertyInfoModel { ID = 1, HouseNumber = "123", Street = "Main St", City = "Anytown", Zip = "12345", State = "CA", HouseOrRoomType = "House", HouseRent = 2000, Rooms = 3, Baths = 2, Sqft = 1800, AmenFeatList = "Swimming Pool, Gym", Description = "Spacious family house" },
                new PropertyInfoModel { ID = 2, HouseNumber = "456", Street = "Oak St", City = "Otherville", Zip = "54321", State = "NY", HouseOrRoomType = "Rooms", HouseRent = 1500, Rooms = 2, Baths = 1, Sqft = 1000, AmenFeatList = "Laundry, Parking, Backyard", Description = "Cozy condo with rooms for rent" },
                new PropertyInfoModel { ID = 3, HouseNumber = "789", Street = "Pine St", City = "Smalltown", Zip = "67890", State = "TX", HouseOrRoomType = "House", HouseRent = 1800, Rooms = 2, Baths = 1, Sqft = 1200, AmenFeatList = "Garage, Patio", Description = "Charming cottage" },
                new PropertyInfoModel { ID = 4, HouseNumber = "101", Street = "Maple St", City = "Villageton", Zip = "98765", State = "FL", HouseOrRoomType = "House", HouseRent = 1200, Rooms = 2, Baths = 1, Sqft = 800, AmenFeatList = "Utilities Included", Description = "Small home" }
            );

            // Seed dummy data for PropertyExpenseModel
            builder.Entity<PropertyExpenseModel>().HasData(
                new PropertyExpenseModel { ID = 1, PropertyInfoID = 1, Date = DateTime.Now.AddMonths(-6), Amount = 300, Description = "Mortgage", IsRecurring = true, IsFixedAmount = true },
                new PropertyExpenseModel { ID = 2, PropertyInfoID = 1, Date = DateTime.Now.AddMonths(-5), Amount = 300, Description = "Mortgage", IsRecurring = true, IsFixedAmount = true },
                new PropertyExpenseModel { ID = 3, PropertyInfoID = 1, Date = DateTime.Now.AddMonths(-4), Amount = 300, Description = "Mortgage", IsRecurring = true, IsFixedAmount = true },
                new PropertyExpenseModel { ID = 4, PropertyInfoID = 1, Date = DateTime.Now.AddMonths(-3), Amount = 300, Description = "Mortgage", IsRecurring = true, IsFixedAmount = true },
                new PropertyExpenseModel { ID = 5, PropertyInfoID = 1, Date = DateTime.Now.AddMonths(-2), Amount = 300, Description = "Mortgage", IsRecurring = true, IsFixedAmount = true },
                new PropertyExpenseModel { ID = 6, PropertyInfoID = 1, Date = DateTime.Now.AddMonths(-1), Amount = 300, Description = "Mortgage", IsRecurring = true, IsFixedAmount = true },
                new PropertyExpenseModel { ID = 7, PropertyInfoID = 1, Date = DateTime.Now.AddMonths(-1), Amount = 200, Description = "Maintenance Repair", IsRecurring = false, IsFixedAmount = false },
                new PropertyExpenseModel { ID = 8, PropertyInfoID = 1, Date = DateTime.Now, Amount = 250, Description = "Maintenance Repair", IsRecurring = false, IsFixedAmount = false }
            );

            // Seed dummy data for PropertyRevenueModel
            builder.Entity<PropertyRevenueModel>().HasData(
                new PropertyRevenueModel { ID = 1, PropertyInfoID = 1, Date = DateTime.Now.AddMonths(-6), Amount = 2000, Description = "Rent", IsRecurring = true, IsFixedAmount = true },
                new PropertyRevenueModel { ID = 2, PropertyInfoID = 1, Date = DateTime.Now.AddMonths(-5), Amount = 2000, Description = "Rent", IsRecurring = true, IsFixedAmount = true },
                new PropertyRevenueModel { ID = 3, PropertyInfoID = 1, Date = DateTime.Now.AddMonths(-4), Amount = 2000, Description = "Rent", IsRecurring = true, IsFixedAmount = true },
                new PropertyRevenueModel { ID = 4, PropertyInfoID = 1, Date = DateTime.Now.AddMonths(-3), Amount = 2000, Description = "Rent", IsRecurring = true, IsFixedAmount = true },
                new PropertyRevenueModel { ID = 5, PropertyInfoID = 1, Date = DateTime.Now.AddMonths(-2), Amount = 2000, Description = "Rent", IsRecurring = true, IsFixedAmount = true },
                new PropertyRevenueModel { ID = 6, PropertyInfoID = 1, Date = DateTime.Now.AddMonths(-1), Amount = 2000, Description = "Rent", IsRecurring = true, IsFixedAmount = true }
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
                new ManagerFinanceModel { ID = 2, MonthlyRentRecieved = 2500, ManagerID = 2 }
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

            // Seed dummy data for MaintenanceModel
            builder.Entity<MaintenanceModel>().HasData(
                new MaintenanceModel { ID = 1, Status = "To Do", Category = "Plumbing", Priority = "Urgent", Description = "There's something wrong with the toilet.", DateRequested = DateTime.Now.AddDays(-5), ContractorName = "Plumbing Pros", ContractorEmail = "plumbing@example.com", ContractorPhone = "123-456-7890", UserID = 3 },
                new MaintenanceModel { ID = 2, Status = "In Progress", Category = "Electricity", Priority = "Standard", Description = "Outlets aren't working", DateRequested = DateTime.Now.AddDays(-10), ContractorName = "Electricity Experts", ContractorEmail = "electricity@example.com", ContractorPhone = "987-654-3210", UserID = 4 },
                new MaintenanceModel { ID = 3, Status = "Completed", Category = "HVAC", Priority = "Standard", Description = "not sure what's wrong", DateRequested = DateTime.Now.AddDays(-15), ContractorName = "HVAC Solutions", ContractorEmail = "hvac@example.com", ContractorPhone = "555-555-5555", UserID = 3 }
            );

            // Seed dummy data for PropertyMaintenanceModel
            builder.Entity<PropertyMaintenanceModel>().HasData(
                new PropertyMaintenanceModel { ID = 1, PropertyInfoID = 1, MaintenanceID = 1 },
                new PropertyMaintenanceModel { ID = 2, PropertyInfoID = 2, MaintenanceID = 2 },
                new PropertyMaintenanceModel { ID = 3, PropertyInfoID = 1, MaintenanceID = 3 }
            );


            base.OnModelCreating(builder);
        }
    }
}