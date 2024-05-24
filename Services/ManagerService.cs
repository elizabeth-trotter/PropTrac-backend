using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropTrac_backend.Models;
using PropTrac_backend.Models.DTO;
using PropTrac_backend.Models.DTO.ManagerDashboard;
using PropTrac_backend.Models.DTO.Properties;
using PropTrac_backend.Models.DTO.Tenants;
using PropTrac_backend.Services.Context;

namespace PropTrac_backend.Services
{
    public class ManagerService
    {
        private readonly DataContext _context;
        public ManagerService(DataContext context)
        {
            _context = context;
        }

        // Helper method to get the manager by user ID
        public ManagerModel GetManagerByUserId(int userId)
        {
            return _context.Managers.SingleOrDefault(m => m.UserID == userId);
        }

        // Helper method to get property IDs managed by the manager
        private IEnumerable<int?> GetPropertyIdsManagedByManager(ManagerModel manager)
        {
            return _context.ManagerProperties
                .Where(mp => mp.ManagerID == manager.ID)
                .Select(mp => mp.PropertyInfoID)
                .ToList();
        }

        public MonthlyProfitOrLossDTO GetMonthlyProfitOrLoss(int userId, int month, int year)
        {
            var manager = GetManagerByUserId(userId);
            var propertyIds = GetPropertyIdsManagedByManager(manager);

            var expenseTotal = 0;
            foreach (var propertyId in propertyIds)
            {
                var expense = _context.PropertyExpense
                    .Where(e => e.PropertyInfoID == propertyId && e.Date.Month == month && e.Date.Year == year)
                    .Select(e => e.Amount)
                    .Sum();
                expenseTotal += expense;
            }

            var revenueTotal = 0;
            foreach (var propertyId in propertyIds)
            {
                var revenue = _context.PropertyRevenue
                    .Where(e => e.PropertyInfoID == propertyId && e.Date.Month == month && e.Date.Year == year)
                    .Select(e => e.Amount)
                    .Sum();
                revenueTotal += revenue;
            }

            var profitOrLoss = revenueTotal - expenseTotal;

            return new MonthlyProfitOrLossDTO
            {
                Month = month,
                ExpenseTotal = expenseTotal,
                RevenueTotal = revenueTotal,
                ProfitOrLossAmount = profitOrLoss
            };
        }

        public List<MonthlyProfitOrLossDTO> GetPastSixMonthsProfitOrLoss(int userId, int month, int year)
        {
            List<MonthlyProfitOrLossDTO> profitOrLossStatment = new();

            for (int i = 0; i < 6; i++)
            {
                month--;
                if (month == 0)
                {
                    month = 12;
                    year--;
                }

                var profitOrLoss = GetMonthlyProfitOrLoss(userId, month, year);
                profitOrLossStatment.Add(profitOrLoss);
            }

            return profitOrLossStatment;
        }

        private MonthlyProfitOrLossDTO GetProjectedMonthlyProfitOrLoss(int userId, int month, int year)
        {
            var manager = GetManagerByUserId(userId);
            var propertyIds = GetPropertyIdsManagedByManager(manager);

            var expenseTotal = 0;
            foreach (var propertyId in propertyIds)
            {
                var expense = _context.PropertyExpense
                    .Where(e => e.PropertyInfoID == propertyId && e.Date.Month == month && e.Date.Year == year && e.IsRecurring && e.IsFixedAmount)
                    .Select(e => e.Amount)
                    .Sum();
                expenseTotal += expense;
            }

            var revenueTotal = 0;
            foreach (var propertyId in propertyIds)
            {
                var revenue = _context.PropertyRevenue
                    .Where(e => e.PropertyInfoID == propertyId && e.Date.Month == month && e.Date.Year == year && e.IsRecurring && e.IsFixedAmount)
                    .Select(e => e.Amount)
                    .Sum();
                revenueTotal += revenue;
            }

            var profitOrLoss = revenueTotal - expenseTotal;

            return new MonthlyProfitOrLossDTO
            {
                Month = month,
                ExpenseTotal = expenseTotal,
                RevenueTotal = revenueTotal,
                ProfitOrLossAmount = profitOrLoss
            };
        }

        public List<MonthlyProfitOrLossDTO> GetProjectedProfitOrLoss(int userId, int month, int year)
        {
            List<MonthlyProfitOrLossDTO> profitOrLossStatment = new();

            for (int i = 0; i < 6; i++)
            {
                var profitOrLoss = GetProjectedMonthlyProfitOrLoss(userId, month - 1, year);
                profitOrLoss = new MonthlyProfitOrLossDTO
                {
                    Month = month + i,
                    ExpenseTotal = profitOrLoss.ExpenseTotal,
                    RevenueTotal = profitOrLoss.RevenueTotal,
                    ProfitOrLossAmount = profitOrLoss.ProfitOrLossAmount
                };
                profitOrLossStatment.Add(profitOrLoss);
            }

            return profitOrLossStatment;
        }

        public PropertyStatsDTO GetPropertyStats(int userId)
        {
            var manager = GetManagerByUserId(userId);

            if (manager == null)
            {
                return null;
            }

            // Get the property IDs managed by the manager
            var propertyIds = GetPropertyIdsManagedByManager(manager);

            // Get the count of tenants associated with the properties managed by the manager
            var activeTenantsCount = _context.Tenants
                .Where(t => propertyIds.Contains(t.PropertyInfoID))
                .Count();

            // Get the count of properties managed by the manager
            var propertiesCount = 0;

            foreach (var propertyId in propertyIds)
            {
                var property = _context.PropertyInfo.Find(propertyId);
                if (property != null)
                {
                    // If the property type is "house," check if any tenants are assigned
                    if (property.HouseOrRoomType.ToLower() == "house")
                    {
                        propertiesCount++;
                    }
                    // If the property type is "rooms," check if any of the associated rooms are unoccupied
                    else if (property.HouseOrRoomType.ToLower() == "rooms")
                    {
                        var roomIds = _context.RoomInfo
                            .Where(r => r.PropertyInfoID == propertyId)
                            .Select(r => r.ID);

                        foreach (var roomId in roomIds)
                        {
                            propertiesCount++;
                        }
                    }
                }
            }

            var openListings = propertiesCount - activeTenantsCount;

            return new PropertyStatsDTO
            {
                FirstName = manager.FirstName,
                ActiveTenants = activeTenantsCount,
                OpenListings = openListings,
                Properties = propertiesCount
            };
        }

        public List<MaintenanceStatsDTO> GetMaintenanceStats(int userId)
        {
            var manager = GetManagerByUserId(userId);

            if (manager == null)
            {
                return null;
            }

            var propertyIds = GetPropertyIdsManagedByManager(manager);

            // Retrieve all maintenance IDs associated with the properties managed by the manager
            var maintenanceIDs = _context.PropertyMaintenance
                .Where(pm => propertyIds.Contains(pm.PropertyInfoID))
                .Select(pm => pm.MaintenanceID);

            // Retrieve all maintenance requests associated with the properties managed by the manager
            var maintenanceRequests = _context.Maintenance
                .Where(m => maintenanceIDs.Contains(m.ID));

            // Map each maintenance request to a MaintenanceStatsDTO object
            var maintenanceStats = maintenanceRequests.Select(mr => new MaintenanceStatsDTO
            {
                ID = mr.ID,
                Status = mr.Status,
                Category = mr.Category,
                Priority = mr.Priority,
                DateRequested = mr.DateRequested,
                PropertyInfoID = mr.PropertyMaintenance.PropertyInfo.ID
            }).ToList();

            return maintenanceStats;
        }

        public List<PropertiesDTO> GetAllProperties(int userId)
        {
            var roomProperties = _context.PropertyInfo
                .Where(p => p.ManagerProperties.Manager.User.ID == userId && p.HouseOrRoomType.ToLower() == "rooms")
                .SelectMany(p => p.RoomInfo.Select(room => new PropertiesDTO
                {
                    ID = p.ID,
                    RoomID = room.ID,
                    HouseNumber = p.HouseNumber,
                    Street = p.Street,
                    City = p.City,
                    Zip = p.Zip,
                    State = p.State,
                    HouseOrRoomType = p.HouseOrRoomType,
                    HouseRent = p.HouseRent,
                    RoomRent = room.RoomRent,
                    Rooms = p.Rooms,
                    Baths = p.Baths,
                    Sqft = p.Sqft,
                    AmenFeatList = p.AmenFeatList,
                    Description = p.Description,
                    TenantID = room.Tenant != null ? room.Tenant.ID : null,
                    TenantAssigned = room.Tenant != null
                }))
                .ToList();

            var houseProperties = _context.PropertyInfo
                .Where(p => p.ManagerProperties.Manager.User.ID == userId && p.HouseOrRoomType.ToLower() != "rooms")
                .Select(p => new PropertiesDTO
                {
                    ID = p.ID,
                    HouseNumber = p.HouseNumber,
                    Street = p.Street,
                    City = p.City,
                    Zip = p.Zip,
                    State = p.State,
                    HouseOrRoomType = p.HouseOrRoomType,
                    HouseRent = p.HouseRent,
                    Rooms = p.Rooms,
                    Baths = p.Baths,
                    Sqft = p.Sqft,
                    AmenFeatList = p.AmenFeatList,
                    Description = p.Description,
                    TenantID = p.Tenant != null ? p.Tenant.Single(t => t.PropertyInfoID == p.ID).ID : null,
                    TenantAssigned = p.Tenant != null && p.Tenant.SingleOrDefault(t => t.PropertyInfoID == p.ID) != null
                })
                .ToList();

            var properties = roomProperties.Concat(houseProperties)
                .OrderBy(p => p.ID)
                .ToList();

            return properties;
        }

        public bool AddPropertyByUserID(AddPropertyDTO request)
        {
            bool result = false;

            PropertyInfoModel propertyInfoModel = new()
            {
                HouseNumber = request.HouseNumber,
                Street = request.Street,
                City = request.City,
                Zip = request.Zip,
                State = request.State,
                HouseOrRoomType = request.HouseOrRoomType,
                HouseRent = request.HouseRent,
                Rooms = request.Rooms,
                Baths = request.Baths,
                Sqft = request.Sqft,
                AmenFeatList = request.AmenFeatList,
                Description = request.Description,
            };

            //adds new request to the PropertyInfo table in database
            _context.PropertyInfo.Add(propertyInfoModel);
            // SaveChanges to generate the ID for propertyInfoModel
            _context.SaveChanges();

            if (request.HouseOrRoomType.ToLower() == "rooms")
            {
                foreach (var roomDTO in request.RoomsList)
                {
                    RoomInfoModel roomInfoModel = new()
                    {
                        RoomRent = roomDTO.RoomRent,
                        PropertyInfoID = propertyInfoModel.ID
                    };

                    _context.RoomInfo.Add(roomInfoModel);
                }
            }

            var manager = GetManagerByUserId(request.UserID);

            ManagerPropertiesModel managerPropertiesModel = new()
            {
                ManagerID = manager.ID,
                PropertyInfoID = propertyInfoModel.ID
            };

            _context.ManagerProperties.Add(managerPropertiesModel);

            //save into database, return of number of entries written into database
            result = _context.SaveChanges() != 0;

            return result;
        }

        private PropertyInfoModel FindPropertyWithRooms(int propertyId)
        {
            return _context.PropertyInfo
                .Include(p => p.RoomInfo)
                .SingleOrDefault(p => p.ID == propertyId);
        }

        public bool EditPropertyByID(EditPropertyDTO propertyToUpdate)
        {
            var existingProperty = FindPropertyWithRooms(propertyToUpdate.ID);

            if (existingProperty == null)
            {
                return false; // Property not found, return false
            }

            // Update PropertyInfoModel properties
            existingProperty.HouseNumber = propertyToUpdate.HouseNumber;
            existingProperty.Street = propertyToUpdate.Street;
            existingProperty.City = propertyToUpdate.City;
            existingProperty.Zip = propertyToUpdate.Zip;
            existingProperty.State = propertyToUpdate.State;
            existingProperty.HouseOrRoomType = propertyToUpdate.HouseOrRoomType;
            existingProperty.HouseRent = propertyToUpdate.HouseRent;
            existingProperty.Rooms = propertyToUpdate.Rooms;
            existingProperty.Baths = propertyToUpdate.Baths;
            existingProperty.Sqft = propertyToUpdate.Sqft;
            existingProperty.AmenFeatList = propertyToUpdate.AmenFeatList;
            existingProperty.Description = propertyToUpdate.Description;

            // Update RoomInfoModel entities
            if (propertyToUpdate.HouseOrRoomType.ToLower() == "rooms")
            {
                if (propertyToUpdate.RoomsList != null)
                {
                    // Update existing rooms with values from propertyToUpdate
                    foreach (var roomDTO in propertyToUpdate.RoomsList)
                    {
                        var existingRoom = existingProperty.RoomInfo.SingleOrDefault(room => room.ID == roomDTO.RoomID);
                        if (existingRoom != null)
                        {
                            existingRoom.RoomRent = roomDTO.RoomRent;
                        }
                        else
                        {
                            return false; // Room not found, return false
                        }
                    }
                }
            }

            // Save changes to the database
            return _context.SaveChanges() != 0;
        }

        public bool DeletePropertyById(int propertyId)
        {
            var propertyToDelete = FindPropertyWithRooms(propertyId);

            if (propertyToDelete == null)
            {
                return false;
            }

            // Remove all attached rooms
            if (propertyToDelete.HouseOrRoomType.ToLower() == "rooms")
            {
                _context.RoomInfo.RemoveRange(propertyToDelete.RoomInfo);
            }

            // Remove related entries from ManagerProperties
            var managerPropertiesToDelete = _context.ManagerProperties
                .Where(mp => mp.PropertyInfoID == propertyId);
            _context.ManagerProperties.RemoveRange(managerPropertiesToDelete);

            // Remove the property from the context
            _context.PropertyInfo.Remove(propertyToDelete);

            // Save changes to the database
            return _context.SaveChanges() != 0;
        }

        public bool DeleteRoomById(int propertyId, int roomId)
        {
            var propertyToDeleteRoomFrom = FindPropertyWithRooms(propertyId);

            if (propertyToDeleteRoomFrom == null)
            {
                return false;
            }

            // Find the room to delete
            var roomToDelete = propertyToDeleteRoomFrom.RoomInfo.FirstOrDefault(r => r.ID == roomId);

            if (roomToDelete == null)
            {
                return false;
            }

            // Remove the room from the property
            propertyToDeleteRoomFrom.RoomInfo.Remove(roomToDelete);

            // Remove the room from the context
            _context.RoomInfo.Remove(roomToDelete);

            // Save changes to the database
            return _context.SaveChanges() != 0;
        }

        public ManagerAccountInfoDTO GetManagerInfo(int userId)
        {
            var manager = _context.Managers
                   .Include(m => m.User) // Eager loading User entity
                   .SingleOrDefault(m => m.UserID == userId);

            if (manager == null)
            {
                return null;
            }

            var managerInfo = new ManagerAccountInfoDTO();

            if (manager.User != null && manager.User.Email != null)
            {
                managerInfo.Email = manager.User.Email;
            }

            managerInfo.ID = manager.UserID;
            managerInfo.FirstName = manager.FirstName;
            managerInfo.LastName = manager.LastName;
            managerInfo.Phone = manager.Phone;
            managerInfo.Role = manager.Role;
            managerInfo.Location = manager.Location;
            managerInfo.Language = manager.Language;

            return managerInfo;
        }

        public bool EditManagerInfo(ManagerAccountInfoDTO managerToUpdate)
        {
            var manager = GetManagerByUserId(managerToUpdate.ID);

            if (manager == null)
            {
                return false;
            }

            if (manager.User != null && manager.User.Email != null)
            {
                manager.User.Email = managerToUpdate.Email;
            }

            manager.FirstName = managerToUpdate.FirstName;
            manager.LastName = managerToUpdate.LastName;
            manager.Phone = managerToUpdate.Phone;
            manager.Role = managerToUpdate.Role;
            manager.Location = managerToUpdate.Location;
            manager.Language = managerToUpdate.Language;

            // Save changes to the database
            return _context.SaveChanges() != 0;
        }

        public List<TenantsDTO> GetAllTenantInfo(int userId)
        {
            var manager = GetManagerByUserId(userId);
            var propertyIds = GetPropertyIdsManagedByManager(manager);

            var tenantsList = new List<TenantsDTO>();

            // Use eager loading to include related entities (User and Documents)
            foreach (var propertyId in propertyIds)
            {
                var property = _context.PropertyInfo
                    .Include(p => p.Tenant) // Include Tenants related to the property
                        .ThenInclude(t => t.User) // Include User related to the tenant
                    .Include(p => p.Tenant) // Include Tenants related to the property
                        .ThenInclude(t => t.Documents) // Include Documents related to the tenant
                    .FirstOrDefault(p => p.ID == propertyId);

                if (property != null)
                {
                    var tenants = _context.Tenants.Where(t => t.PropertyInfoID == propertyId).ToList();
                    foreach (var tenant in tenants)
                    {
                        var tenantDto = new TenantsDTO
                        {
                            ID = tenant.ID,
                            FirstName = tenant.FirstName,
                            LastName = tenant.LastName,
                            Phone = tenant.Phone,
                            LeaseType = tenant.LeaseType,
                            LeaseStart = tenant.LeaseStart,
                            LeaseEnd = tenant.LeaseEnd,
                            Email = tenant.User?.Email ?? "", // Use null-conditional operator and null-coalescing operator
                            HouseNumber = tenant.PropertyInfo?.HouseNumber ?? "", // Use null-conditional operator and null-coalescing operator
                            Street = tenant.PropertyInfo?.Street ?? "", // Use null-conditional operator and null-coalescing operator
                            City = tenant.PropertyInfo?.City ?? "", // Use null-conditional operator and null-coalescing operator
                            State = tenant.PropertyInfo?.State ?? "", // Use null-conditional operator and null-coalescing operator
                            Zip = tenant.PropertyInfo?.Zip ?? "", // Use null-conditional operator and null-coalescing operator
                            DocumentName = tenant.Documents?.Name ?? "", // Use null-conditional operator and null-coalescing operator
                            DocumentType = tenant.Documents?.Type ?? "", // Use null-conditional operator and null-coalescing operator
                            DocumentContent = tenant.Documents?.Content
                        };
                        tenantsList.Add(tenantDto);
                    }
                }
            }

            return tenantsList;
        }

        public bool DoesTenantExist(string email)
        {
            //check if request exists
            //if 1 matches, then return the item
            //if no item matches, then return null

            return _context.UserInfo.SingleOrDefault(request => request.Email == email) != null;
        }

        public bool AddTenant(AddTenantDTO addTenantDTO)
        {
            bool result = false;

            //if tenant doesn't exist, add tenant
            if (!DoesTenantExist(addTenantDTO.Email))
            {
                UserModel newUserModel = new()
                {
                    Email = addTenantDTO.Email,
                    IsManager = false
                };

                _context.UserInfo.Add(newUserModel);
                _context.SaveChanges();

                DocumentsModel newDocumentModel = null;

                // Add document if provided
                if (addTenantDTO.DocumentsContent != null)
                {
                    newDocumentModel = new()
                    {
                        Name = addTenantDTO.DocumentsName,
                        Type = addTenantDTO.DocumentsType,
                        Content = addTenantDTO.DocumentsContent
                    };

                    _context.Documents.Add(newDocumentModel);
                    _context.SaveChanges();
                }

                TenantModel newTenantModel = new()
                {
                    FirstName = addTenantDTO.FirstName,
                    LastName = addTenantDTO.LastName,
                    Phone = addTenantDTO.Phone,
                    LeaseType = addTenantDTO.LeaseType,
                    LeaseStart = addTenantDTO.LeaseStart,
                    LeaseEnd = addTenantDTO.LeaseEnd,
                    UserID = newUserModel.ID,
                    RoomInfoID = addTenantDTO.RoomInfoID,
                    PropertyInfoID = addTenantDTO.PropertyInfoID,
                    DocumentsID = newDocumentModel != null ? newDocumentModel.ID : null
                };

                _context.Tenants.Add(newTenantModel);
                _context.SaveChanges();

                result = true;
            }

            return result;
        }
    }
}