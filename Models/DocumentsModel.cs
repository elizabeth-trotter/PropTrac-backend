using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models
{
    public class DocumentsModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public byte[]? Content { get; set; }
        public DateTime UploadDate { get; set; }

        // Connection to TenantModel
        public TenantModel? Tenant { get; set; }  // Navigation property

        public DocumentsModel()
        {

        }
    }
}