using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models
{
    public class ManagerModel
    {
        public int ID { get; set; } // Primary Key
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string? Phone { get; set; } // Values can be null because these will not be filled during account creation**
        public string? Role { get; set; }
        public string? Location { get; set; }
        public string? Language { get; set; }

        // Connection to UserModel
        // Explicitly specify foreign key using ForeignKey attribute
        // [ForeignKey("User")]
        public int UserID { get; set; } // Foreign key
        public UserModel User { get; set; } // Navigation property

        // Connections To ManagerPropertiesModel, ManagerFinanceModel, ManagerDocumentsModel
        public ManagerPropertiesModel ManagerProperties { get; set; }
        public ManagerFinanceModel ManagerFinance { get; set; }
        public ManagerDocumentsModel ManagerDocuments { get; set; }

        public ManagerModel()
        {

        }
    }
}