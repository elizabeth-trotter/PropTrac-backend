using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PropTrac_backend.Models;
using PropTrac_backend.Models.DTO;
using PropTrac_backend.Models.DTO.ManagerDashboard;
using PropTrac_backend.Models.DTO.Properties;
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

            // Get the count of open listings with the properties managed by the manager
            var openListingsCount = 0;
            foreach (var propertyId in propertyIds)
            {
                var property = _context.PropertyInfo.Find(propertyId);
                if (property != null)
                {
                    // If the property type is "house," check if any tenants are assigned
                    if (property.HouseOrRoomType.ToLower() == "house")
                    {
                        if (!_context.Tenants.Any(t => t.PropertyInfoID == propertyId))
                        {
                            openListingsCount++;
                        }
                    }
                    // If the property type is "rooms," check if any of the associated rooms are unoccupied
                    else if (property.HouseOrRoomType.ToLower() == "rooms")
                    {
                        var roomIds = _context.RoomInfo
                            .Where(r => r.PropertyInfoID == propertyId)
                            .Select(r => r.ID);

                        if (!_context.Tenants.Any(t => t.RoomInfoID.HasValue && roomIds.Contains(t.RoomInfoID.Value)))
                        {
                            openListingsCount++;
                        }
                    }
                }
            }

            // Get the count of properties managed by the manager
            var propertiesCount = propertyIds.Count();

            return new PropertyStatsDTO
            {
                ActiveTenants = activeTenantsCount,
                OpenListings = openListingsCount,
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
                    TenantAssigned = p.Tenant != null
                })
                .ToList();

            var properties = roomProperties.Concat(houseProperties)
                .OrderBy(p => p.ID)
                .ToList();

            return properties;
        }
    }
}