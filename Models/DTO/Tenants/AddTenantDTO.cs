using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models.DTO.Tenants
{
    public class AddTenantDTO
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string? Phone { get; set; }
        public string? LeaseType { get; set; }
        public DateTime LeaseStart { get; set; }
        public DateTime LeaseEnd { get; set; }
        public string Email { get; set; }
        public int? RoomInfoID { get; set; }
        public int? PropertyInfoID { get; set; }
        public string? DocumentsName { get; set; }
        public string? DocumentsType { get; set; }
        public byte[]? DocumentsContent { get; set; }
    }
}