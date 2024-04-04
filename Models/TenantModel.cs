using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models
{
    public class TenantModel
    {
        public int ID { get; set; } // Primary Key
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string? Phone { get; set; } // Values can be null because these will not be filled during account creation *below
        public string? LeaseType { get; set; }
        public DateTime LeaseStart { get; set; }
        public DateTime LeaseEnd { get; set; }

        // Connection to UserModel
        public int UserID { get; set; } // Foreign key
        public UserModel User { get; set; } // Navigation property

        // Connection to TenantPaymentInfoModel
        public TenantPaymentInfoModel? TenantPaymentInfo { get; set; } // Navigation property

        // Connection to RoomInfoModel
        public int? RoomInfoID { get; set; } // Foreign key
        public RoomInfoModel? RoomInfo { get; set; } // Navigation property

        // Connection to PropertyInfoModel
        public int? PropertyInfoID { get; set; } // Foreign key
        public PropertyInfoModel? PropertyInfo { get; set; } // Navigation property

        // // Connection to DocumentsModel
        public int? DocumentID { get; set; } // Foreign key
        public DocumentsModel? Document { get; set; } // Navigation property

        //By making both the foreign key and navigation property nullable (int? and DocumentsModel?), you ensure that when a DocumentsModel record is deleted, the corresponding DocumentID in TenantModel will be set to NULL. This aligns with the ON DELETE SET NULL cascade action behavior.

        public TenantModel()
        {

        }
    }
}