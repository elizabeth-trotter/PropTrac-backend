using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models.DTO.Tenants
{
    public class TenantsDTO
    {
        public int ID { get; set; } // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; } // Values can be null because these will not be filled during account creation *below
        public string? LeaseType { get; set; }
        public DateTime LeaseStart { get; set; }
        public DateTime LeaseEnd { get; set; }
        public string Email { get; set; }
        public string? HouseNumber { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Zip { get; set; }
        public string? State { get; set; }
        public string? DocumentName { get; set; }
        public string? DocumentType { get; set; }
        public byte[]? DocumentContent { get; set; }
    }
}