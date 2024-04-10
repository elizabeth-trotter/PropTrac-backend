using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PropTrac_backend.Models.DTO;
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

        public PropertyStatsDTO GetPropertyStats(int userId)
        {
            // Get the manager with the specified user ID
            var manager = _context.Managers.FirstOrDefault(m => m.UserID == userId);

            if (manager == null)
            {
                return null;
            }

            // LINQ Method Syntax

            // Get the county of properties by the manager
            var propertyCount = _context.ManagerProperties.Count(mp => mp.Manager.UserID == userId);

            // Get the count of 
            var listingCount = 0;

            // Get the count of tenants associated with the properties managed by the manager
            var tenantCount = _context.Tenants
                .Count(t => _context.ManagerProperties
                    .Any(mp => mp.ManagerID == manager.ID && mp.PropertyInfoID == t.PropertyInfoID));

            // Create a PropertyStatsDTO object
            var propertyStats = new PropertyStatsDTO
            {
                ActiveTenants = tenantCount,
                OpenListings = listingCount,
                Properties = propertyCount
            };

            return propertyStats;
        }

        // public bool DoesManagerExist(){

        // }
    }
}