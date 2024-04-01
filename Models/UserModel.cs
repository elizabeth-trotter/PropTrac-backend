using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string? UserName { get; set; }
        public string? Salt { get; set; }
        public string? Hash { get; set; }

        public UserModel()
        {
            
        }
    }
}