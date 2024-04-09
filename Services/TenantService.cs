using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PropTrac_backend.Models;
using PropTrac_backend.Models.DTO;
using PropTrac_backend.Services.Context;

namespace PropTrac_backend.Services
{
    public class TenantService
    {
        private readonly DataContext _context;

        public TenantService(DataContext context)
        {
            _context = context;
        }

        public List<TenantDashboardDTO> GetTenantDashboardByUserId(int userId)
        {
            // Retrieve tenants based on the provided user ID
            // var tenants = _context.Tenants.Where(t => t.UserID == userId).ToList();

            var tenants = _context.Tenants
                .Where(t => t.UserID == userId)
                .Select(t => new TenantDashboardDTO
                {
                    ID = t.ID,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Phone = t.Phone,
                    LeaseType = t.LeaseType,
                    LeaseStart = t.LeaseStart,
                    LeaseEnd = t.LeaseEnd,
                    RoomInfoID = t.RoomInfoID,
                    RoomRent = t.RoomInfo != null ? t.RoomInfo.RoomRent : null,
                    PropertyInfoID = t.PropertyInfoID,
                    HouseNumber = t.PropertyInfo != null ? t.PropertyInfo.HouseNumber : null,
                    Street = t.PropertyInfo != null ? t.PropertyInfo.Street : null,
                    City = t.PropertyInfo != null ? t.PropertyInfo.City : null,
                    Zip = t.PropertyInfo != null ? t.PropertyInfo.Zip : null,
                    State = t.PropertyInfo != null ? t.PropertyInfo.State : null,
                    HouseOrRoomType = t.PropertyInfo != null ? t.PropertyInfo.HouseOrRoomType : null,
                    HouseRent = t.PropertyInfo != null ? t.PropertyInfo.HouseRent : null,
                    DocumentID = t.DocumentID,
                    Name = t.Document != null ? t.Document.Name : null,
                    Type = t.Document != null ? t.Document.Type : null,
                    Content = t.Document != null ? t.Document.Content : null,
                    UploadDate = t.Document != null ? t.Document.UploadDate : null
                })
                .ToList();


            return tenants;
        }
    }
}