using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models.DTO.Properties
{
    public class AddPropertyDTO
    {
        public int ID { get; set; } // Primary Key
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public string HouseOrRoomType { get; set; }
        public int HouseRent { get; set; }
        public int Rooms { get; set; }
        public int Baths { get; set; }
        public int Sqft { get; set; }
        public string AmenFeatList { get; set; }
        public string Description { get; set; }
        public int RoomRent { get; set; } = 0;
        public int UserID { get; set; }
    }
}