using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InventoryManagementSystem.Data
{
    public class InventoryService
    {
        private readonly ApplicationDbContext _appDBContext;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(ApplicationDbContext appDBContext, ILogger<InventoryService> logger)
        {
            _appDBContext = appDBContext;
            _logger = logger;
        }

        public async Task<List<Item>> GetAllInventoryAsync()
        {
            try
            {
                return await _appDBContext.Item.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all inventory items.");
                throw;
            }
        }

        public async Task<bool> InsertItemAsync(Item item)
        {
            try
            {
                await _appDBContext.Item.AddAsync(item);
                await _appDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while inserting an inventory item.");
                throw;
            }
        }

        public async Task<Item> GetItemAsync(int Id)
        {
            try
            {
                Item item = await _appDBContext.Item.FirstOrDefaultAsync(c => c.Id.Equals(Id));
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting an inventory item by Id.");
                throw;
            }
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            try
            {
                _appDBContext.Item.Update(item);
                await _appDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating an inventory item.");
                throw;
            }
        }

        public async Task<bool> DeleteItemAsync(Item item)
        {
            try
            {
                _appDBContext.Remove(item);
                await _appDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting an inventory item.");
                throw;
            }
        }
    }
}
