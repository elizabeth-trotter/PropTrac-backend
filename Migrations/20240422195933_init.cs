using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropTrac_backend.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Maintenance",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRequested = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ContractorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractorEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractorPhone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenance", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PropertyInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseOrRoomType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseRent = table.Column<int>(type: "int", nullable: false),
                    Rooms = table.Column<int>(type: "int", nullable: false),
                    Baths = table.Column<int>(type: "int", nullable: false),
                    Sqft = table.Column<int>(type: "int", nullable: false),
                    AmenFeatList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SecurityQuestion",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityQuestion", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyPropertyFinance",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyInfoID = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    ExpenseAmount = table.Column<int>(type: "int", nullable: false),
                    RevenueAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyPropertyFinance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MonthlyPropertyFinance_PropertyInfo_PropertyInfoID",
                        column: x => x.PropertyInfoID,
                        principalTable: "PropertyInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyExpense",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyInfoID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRecurring = table.Column<bool>(type: "bit", nullable: false),
                    IsFixedAmount = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyExpense", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PropertyExpense_PropertyInfo_PropertyInfoID",
                        column: x => x.PropertyInfoID,
                        principalTable: "PropertyInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyMaintenance",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyInfoID = table.Column<int>(type: "int", nullable: true),
                    MaintenanceID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyMaintenance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PropertyMaintenance_Maintenance_MaintenanceID",
                        column: x => x.MaintenanceID,
                        principalTable: "Maintenance",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PropertyMaintenance_PropertyInfo_PropertyInfoID",
                        column: x => x.PropertyInfoID,
                        principalTable: "PropertyInfo",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PropertyRevenue",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyInfoID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRecurring = table.Column<bool>(type: "bit", nullable: false),
                    IsFixedAmount = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyRevenue", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PropertyRevenue_PropertyInfo_PropertyInfoID",
                        column: x => x.PropertyInfoID,
                        principalTable: "PropertyInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomRent = table.Column<int>(type: "int", nullable: false),
                    PropertyInfoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomInfo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RoomInfo_PropertyInfo_PropertyInfoID",
                        column: x => x.PropertyInfoID,
                        principalTable: "PropertyInfo",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsManager = table.Column<bool>(type: "bit", nullable: false),
                    SecurityAnswerSalt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityAnswerHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityQuestionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserInfo_SecurityQuestion_SecurityQuestionID",
                        column: x => x.SecurityQuestionID,
                        principalTable: "SecurityQuestion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Managers_UserInfo_UserID",
                        column: x => x.UserID,
                        principalTable: "UserInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeaseType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeaseStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaseEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RoomInfoID = table.Column<int>(type: "int", nullable: true),
                    PropertyInfoID = table.Column<int>(type: "int", nullable: true),
                    DocumentsID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tenants_Documents_DocumentsID",
                        column: x => x.DocumentsID,
                        principalTable: "Documents",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Tenants_PropertyInfo_PropertyInfoID",
                        column: x => x.PropertyInfoID,
                        principalTable: "PropertyInfo",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Tenants_RoomInfo_RoomInfoID",
                        column: x => x.RoomInfoID,
                        principalTable: "RoomInfo",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Tenants_UserInfo_UserID",
                        column: x => x.UserID,
                        principalTable: "UserInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManagerDocuments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManagerID = table.Column<int>(type: "int", nullable: true),
                    DocumentsID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerDocuments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ManagerDocuments_Documents_DocumentsID",
                        column: x => x.DocumentsID,
                        principalTable: "Documents",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ManagerDocuments_Managers_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "Managers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ManagerFinance",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonthlyRentRecieved = table.Column<int>(type: "int", nullable: false),
                    ManagerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerFinance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ManagerFinance_Managers_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "Managers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManagerProperties",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManagerID = table.Column<int>(type: "int", nullable: true),
                    PropertyInfoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerProperties", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ManagerProperties_Managers_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "Managers",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ManagerProperties_PropertyInfo_PropertyInfoID",
                        column: x => x.PropertyInfoID,
                        principalTable: "PropertyInfo",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TenantPaymentInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentRecieved = table.Column<bool>(type: "bit", nullable: false),
                    DaysRemaining = table.Column<int>(type: "int", nullable: false),
                    TenantID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantPaymentInfo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TenantPaymentInfo_Tenants_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "ID", "Content", "Name", "Type", "UploadDate" },
                values: new object[,]
                {
                    { 1, new byte[] { 1, 2, 3 }, "LeaseAgreement", "Lease", new DateTime(2024, 4, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4519) },
                    { 2, new byte[] { 4, 5, 6 }, "LeaseAgreement", "Lease", new DateTime(2024, 4, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4525) },
                    { 3, new byte[] { 4, 5, 6 }, "ManagerList", "Manager", new DateTime(2024, 4, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4530) },
                    { 4, new byte[] { 4, 5, 6 }, "ManagerDoc", "Finance", new DateTime(2024, 4, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4535) }
                });

            migrationBuilder.InsertData(
                table: "Maintenance",
                columns: new[] { "ID", "Category", "ContractorEmail", "ContractorName", "ContractorPhone", "DateRequested", "Description", "Image", "Priority", "Status", "UserID" },
                values: new object[,]
                {
                    { 1, "Plumbing", "plumbing@example.com", "Plumbing Pros", "123-456-7890", new DateTime(2024, 4, 17, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4571), "There's something wrong with the toilet.", null, "Urgent", "To Do", 3 },
                    { 2, "Electricity", "electricity@example.com", "Electricity Experts", "987-654-3210", new DateTime(2024, 4, 12, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4576), "Outlets aren't working", null, "Standard", "In Progress", 4 },
                    { 3, "HVAC", "hvac@example.com", "HVAC Solutions", "555-555-5555", new DateTime(2024, 4, 7, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4580), "not sure what's wrong", null, "Standard", "Completed", 3 }
                });

            migrationBuilder.InsertData(
                table: "PropertyInfo",
                columns: new[] { "ID", "AmenFeatList", "Baths", "City", "Description", "HouseNumber", "HouseOrRoomType", "HouseRent", "Rooms", "Sqft", "State", "Street", "Zip" },
                values: new object[,]
                {
                    { 1, "Swimming Pool, Gym", 2, "Anytown", "Spacious family house", "123", "House", 2000, 3, 1800, "CA", "Main St", "12345" },
                    { 2, "Laundry, Parking, Backyard", 1, "Otherville", "Cozy condo with rooms for rent", "456", "Rooms", 1500, 2, 1000, "NY", "Oak St", "54321" },
                    { 3, "Garage, Patio", 1, "Smalltown", "Charming cottage", "789", "House", 1800, 2, 1200, "TX", "Pine St", "67890" },
                    { 4, "Utilities Included", 1, "Villageton", "Small home", "101", "House", 1200, 2, 800, "FL", "Maple St", "98765" }
                });

            migrationBuilder.InsertData(
                table: "SecurityQuestion",
                columns: new[] { "ID", "Question" },
                values: new object[,]
                {
                    { 1, "What is the name of your first pet?" },
                    { 2, "What was your favorite teacher's name?" },
                    { 3, "What was the name of your first stuffed animal?" }
                });

            migrationBuilder.InsertData(
                table: "PropertyExpense",
                columns: new[] { "ID", "Amount", "Date", "Description", "IsFixedAmount", "IsRecurring", "PropertyInfoID" },
                values: new object[,]
                {
                    { 1, 300, new DateTime(2023, 10, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4359), "Mortgage", true, true, 1 },
                    { 2, 300, new DateTime(2023, 11, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4363), "Mortgage", true, true, 1 },
                    { 3, 300, new DateTime(2023, 12, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4366), "Mortgage", true, true, 1 },
                    { 4, 300, new DateTime(2024, 1, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4369), "Mortgage", true, true, 1 },
                    { 5, 300, new DateTime(2024, 2, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4371), "Mortgage", true, true, 1 },
                    { 6, 300, new DateTime(2024, 3, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4374), "Mortgage", true, true, 1 },
                    { 7, 200, new DateTime(2024, 3, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4377), "Maintenance Repair", false, false, 1 },
                    { 8, 250, new DateTime(2024, 4, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4380), "Maintenance Repair", false, false, 1 }
                });

            migrationBuilder.InsertData(
                table: "PropertyMaintenance",
                columns: new[] { "ID", "MaintenanceID", "PropertyInfoID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "PropertyRevenue",
                columns: new[] { "ID", "Amount", "Date", "Description", "IsFixedAmount", "IsRecurring", "PropertyInfoID" },
                values: new object[,]
                {
                    { 1, 2000, new DateTime(2023, 10, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4401), "Rent", true, true, 1 },
                    { 2, 2000, new DateTime(2023, 11, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4406), "Rent", true, true, 1 },
                    { 3, 2000, new DateTime(2023, 12, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4409), "Rent", true, true, 1 },
                    { 4, 2000, new DateTime(2024, 1, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4438), "Rent", true, true, 1 },
                    { 5, 2000, new DateTime(2024, 2, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4440), "Rent", true, true, 1 },
                    { 6, 2000, new DateTime(2024, 3, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4443), "Rent", true, true, 1 }
                });

            migrationBuilder.InsertData(
                table: "RoomInfo",
                columns: new[] { "ID", "PropertyInfoID", "RoomRent" },
                values: new object[,]
                {
                    { 1, 2, 500 },
                    { 2, 2, 600 }
                });

            migrationBuilder.InsertData(
                table: "UserInfo",
                columns: new[] { "ID", "Email", "Hash", "IsManager", "Salt", "SecurityAnswerHash", "SecurityAnswerSalt", "SecurityQuestionID", "Username" },
                values: new object[,]
                {
                    { 1, "john@example.com", "hash1", true, "salt1", "security_hash1", "security_salt1", 1, "john_doe" },
                    { 2, "jane@example.com", "hash2", true, "salt2", "security_hash2", "security_salt2", 2, "jane_smith" },
                    { 3, "alice@example.com", "hash3", false, "salt3", "security_hash3", "security_salt3", 3, "alice_johnson" },
                    { 4, "bob@example.com", "hash4", false, "salt4", "security_hash4", "security_salt4", 1, "bob_williams" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "ID", "FirstName", "Language", "LastName", "Location", "Phone", "Role", "UserID" },
                values: new object[,]
                {
                    { 1, "John", "English", "Doe", "New York", "123-456-7890", "Manager", 1 },
                    { 2, "Jane", "Spanish", "Smith", "Los Angeles", "987-654-3210", "Manager", 2 }
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "ID", "DocumentsID", "FirstName", "LastName", "LeaseEnd", "LeaseStart", "LeaseType", "Phone", "PropertyInfoID", "RoomInfoID", "UserID" },
                values: new object[,]
                {
                    { 1, 1, "Alice", "Johnson", new DateTime(2025, 4, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4258), new DateTime(2024, 4, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4200), "Annual", "123-456-7890", 1, null, 3 },
                    { 2, 2, "Bob", "Williams", new DateTime(2024, 5, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4265), new DateTime(2024, 4, 22, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4264), "Monthly", "987-654-3210", 2, 1, 4 }
                });

            migrationBuilder.InsertData(
                table: "ManagerDocuments",
                columns: new[] { "ID", "DocumentsID", "ManagerID" },
                values: new object[,]
                {
                    { 1, 3, 1 },
                    { 2, 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "ManagerFinance",
                columns: new[] { "ID", "ManagerID", "MonthlyRentRecieved" },
                values: new object[,]
                {
                    { 1, 1, 3000 },
                    { 2, 2, 2500 }
                });

            migrationBuilder.InsertData(
                table: "ManagerProperties",
                columns: new[] { "ID", "ManagerID", "PropertyInfoID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 3 },
                    { 4, 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "TenantPaymentInfo",
                columns: new[] { "ID", "Balance", "DaysRemaining", "DueDate", "PaymentRecieved", "TenantID" },
                values: new object[,]
                {
                    { 1, 1000, 7, new DateTime(2024, 4, 29, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4284), false, 1 },
                    { 2, 1500, 9, new DateTime(2024, 5, 1, 12, 59, 33, 571, DateTimeKind.Local).AddTicks(4291), false, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManagerDocuments_DocumentsID",
                table: "ManagerDocuments",
                column: "DocumentsID",
                unique: true,
                filter: "[DocumentsID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerDocuments_ManagerID",
                table: "ManagerDocuments",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerFinance_ManagerID",
                table: "ManagerFinance",
                column: "ManagerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ManagerProperties_ManagerID",
                table: "ManagerProperties",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerProperties_PropertyInfoID",
                table: "ManagerProperties",
                column: "PropertyInfoID",
                unique: true,
                filter: "[PropertyInfoID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserID",
                table: "Managers",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyPropertyFinance_PropertyInfoID",
                table: "MonthlyPropertyFinance",
                column: "PropertyInfoID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyExpense_PropertyInfoID",
                table: "PropertyExpense",
                column: "PropertyInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyMaintenance_MaintenanceID",
                table: "PropertyMaintenance",
                column: "MaintenanceID",
                unique: true,
                filter: "[MaintenanceID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyMaintenance_PropertyInfoID",
                table: "PropertyMaintenance",
                column: "PropertyInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyRevenue_PropertyInfoID",
                table: "PropertyRevenue",
                column: "PropertyInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomInfo_PropertyInfoID",
                table: "RoomInfo",
                column: "PropertyInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPaymentInfo_TenantID",
                table: "TenantPaymentInfo",
                column: "TenantID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_DocumentsID",
                table: "Tenants",
                column: "DocumentsID",
                unique: true,
                filter: "[DocumentsID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_PropertyInfoID",
                table: "Tenants",
                column: "PropertyInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_RoomInfoID",
                table: "Tenants",
                column: "RoomInfoID",
                unique: true,
                filter: "[RoomInfoID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_UserID",
                table: "Tenants",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_SecurityQuestionID",
                table: "UserInfo",
                column: "SecurityQuestionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManagerDocuments");

            migrationBuilder.DropTable(
                name: "ManagerFinance");

            migrationBuilder.DropTable(
                name: "ManagerProperties");

            migrationBuilder.DropTable(
                name: "MonthlyPropertyFinance");

            migrationBuilder.DropTable(
                name: "PropertyExpense");

            migrationBuilder.DropTable(
                name: "PropertyMaintenance");

            migrationBuilder.DropTable(
                name: "PropertyRevenue");

            migrationBuilder.DropTable(
                name: "TenantPaymentInfo");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Maintenance");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "RoomInfo");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "PropertyInfo");

            migrationBuilder.DropTable(
                name: "SecurityQuestion");
        }
    }
}
