using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models
{
    public class TenantModel
    {
        public int ID { get; set; } // Primary Key
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? LeaseType { get; set; }
        public DateTime LeaseStart { get; set; }
        public DateTime LeaseEnd { get; set; }

        // Connection to UserModel
        public int UserID { get; set; } // Foreign key
        public UserModel User { get; set; } // Navigation property

        // Connection to TenantPaymentInfoModel
        // public TenantPaymentInfoModel TenantPayment { get; set; } // Navigation property

        // Connection to RoomInfoModel
        // public int RoomInfoID { get; set; } // Foreign key
        // public RoomInfoModel RoomInfo { get; set; } // Navigation property

        // Connection to PropertyInfoModel
        // public int PropertyInfoID { get; set; } // Foreign key
        // public PropertyInfoModel PropertyInfo { get; set; } // Navigation property

        // // Connection to DocumentsModel
        // public int DocumentID { get; set; } // Foreign key
        // public DocumentsModel Document { get; set; } // Navigation property

        public TenantModel()
        {

        }
    }
}