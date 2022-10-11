using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace InventoryManagementSystem.Data
{
    public class InventoryService
    {
        #region Property
        private readonly ApplicationDbContext _appDBContext;
        #endregion

        
        public InventoryService(ApplicationDbContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public async Task<List<Item>> GetAllInventoryAsync()
        {
            return await _appDBContext.Item.ToListAsync();
        }
        #region Insert Employee
        public async Task<bool> InsertItemAsync(Item item)
        {
            await _appDBContext.Item.AddAsync(item);
            await _appDBContext.SaveChangesAsync();
            return true;
        }
        #endregion
        #region Get item by Id
        public async Task<Item> GetItemAsync(int Id)
        {
            Item item = await _appDBContext.Item.FirstOrDefaultAsync(c => c.Id.Equals(Id));
            return item;
        }
        #endregion

        #region Update item
        public async Task<bool> UpdateItemAsync(Item item)
        {
            _appDBContext.Item.Update(item);
            await _appDBContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region DeleteItem
        public async Task<bool> DeleteItemAsync(Item item)
        {
            _appDBContext.Remove(item);
            await _appDBContext.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
