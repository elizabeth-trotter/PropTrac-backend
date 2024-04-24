using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models.DTO
{
    public class PropertyStatsDTO
    {
        public string FirstName { get; set; }
        public int ActiveTenants { get; set; }
        public int OpenListings { get; set; }
        public int Properties { get; set; }
    }
}