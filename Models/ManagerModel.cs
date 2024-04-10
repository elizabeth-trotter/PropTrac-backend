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
        public int UserID { get; set; } // Foreign key
        public UserModel User { get; set; } // Navigation property

        // Connections To ManagerPropertiesModel, ManagerFinanceModel, ManagerDocumentsModel
        public ICollection<ManagerPropertiesModel>? ManagerProperties { get; set; } // Ensure this is correctly defined as a collection
        public ManagerFinanceModel? ManagerFinance { get; set; }
        public ICollection<ManagerDocumentsModel>? ManagerDocuments { get; set; } // Ensure this is correctly defined as a collection

        public ManagerModel()
        {

        }
    }
}