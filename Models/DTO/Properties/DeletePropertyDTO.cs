using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropTrac_backend.Models.DTO.Properties
{
    public class DeletePropertyDTO
    {
        public int PropertyID { get; set; }
    }

    public class DeleteRoomDTO
    {
        public int PropertyID { get; set; }
        public int RoomID { get; set; }
    }
}