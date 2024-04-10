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
                    DocumentID = t.DocumentsID,
                    Name = t.Documents != null ? t.Documents.Name : null,
                    Type = t.Documents != null ? t.Documents.Type : null,
                    Content = t.Documents != null ? t.Documents.Content : null,
                    UploadDate = t.Documents != null ? t.Documents.UploadDate : null
                })
                .ToList();


            return tenants;
        }
    }
}