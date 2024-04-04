using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models
{
    public class RoomInfoModel
    {
        public int ID { get; set; } // Primary Key
        public int RoomRent { get; set; }

        // Connection to TenantModel
        public TenantModel? Tenant { get; set; }  // Navigation property

        // Connection to PropertyInfoModel
        public int PropertyInfoID { get; set; } // Foreign key
        public PropertyInfoModel PropertyInfo { get; set; } // Navigation property

        public RoomInfoModel()
        {
            
        }
    }
}