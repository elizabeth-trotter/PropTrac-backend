using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models.DTO
{
    public class TenantDashboardDTO
    {
        // Properties from TenantModel
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; } // Values can be null because these will not be filled during account creation *below
        public string? LeaseType { get; set; }
        public DateTime LeaseStart { get; set; }
        public DateTime LeaseEnd { get; set; }
        
        // Connection to TenantPaymentInfoModel
        // ?

        // Properties from RoomInfoModel
        public int? RoomInfoID { get; set; }
        public int? RoomRent { get; set; }

        // Connection to PropertyInfoModel
        public int? PropertyInfoID { get; set; }
        public string? HouseNumber { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Zip { get; set; }
        public string? State { get; set; }
        public string? HouseOrRoomType { get; set; }
        public int? HouseRent { get; set; }

        // Properties from DocumentModel
        public int? DocumentID { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public byte[]? Content { get; set; }
        public DateTime? UploadDate { get; set; }
    }
}