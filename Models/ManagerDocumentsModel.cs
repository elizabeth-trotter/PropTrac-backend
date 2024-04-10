using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models
{
    public class ManagerDocumentsModel
    {
        public int ID { get; set; }
        
        // Connection to ManagerModel
        public int ManagerID { get; set; } // Foreign key
        public ManagerModel Manager { get; set; } // Navigation property

        // Connection to DocumentsModel
        public int DocumentsID { get; set; } // Foreign key
        public DocumentsModel Documents { get; set; } // Navigation property
    }
}