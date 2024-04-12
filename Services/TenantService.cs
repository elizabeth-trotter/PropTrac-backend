using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PropTrac_backend.Models;
using PropTrac_backend.Models.DTO;
using PropTrac_backend.Models.DTO.TenantDashboard;
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
                    UploadDate = t.Documents != null ? t.Documents.UploadDate : null,
                    ManagerFirst = t.PropertyInfo.ManagerProperties.Manager != null ? t.PropertyInfo.ManagerProperties.Manager.FirstName : null,
                    ManagerLast = t.PropertyInfo.ManagerProperties.Manager != null ? t.PropertyInfo.ManagerProperties.Manager.LastName : null,
                    ManagerPhone = t.PropertyInfo.ManagerProperties.Manager != null ? t.PropertyInfo.ManagerProperties.Manager.Phone : null,
                    ManagerEmail = t.PropertyInfo.ManagerProperties.Manager.User != null ? t.PropertyInfo.ManagerProperties.Manager.User.Email : null
                })
                .ToList();


            return tenants;
        }

        public bool DoesRequestExist(TenantMaintenanceDTO tenantMaintenanceDTO)
        {
            //check if request exists
            //if 1 matches, then return the item
            //if no item matches, then return null

            return _context.Maintenance.SingleOrDefault(request => request.Description == tenantMaintenanceDTO.Description) != null;
        }

        public bool AddMaintenanceRequest(TenantMaintenanceDTO request)
        {
            bool result = false;

            //if request doesn't exist, add request
            if (!DoesRequestExist(request))
            {
                MaintenanceModel maintenanceModel = new();

                maintenanceModel.Description = request.Description;
                maintenanceModel.Priority = request.Priority;
                maintenanceModel.Category = request.Category;
                maintenanceModel.Image = request.Image;

                //adds new request to the Maintenance table in database
                _context.Maintenance.Add(maintenanceModel);

                //save into database, return of number of entries written into database
                result = _context.SaveChanges() != 0;
            }

            return result;
        }
    }
}