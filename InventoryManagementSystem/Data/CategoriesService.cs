using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Data
{
    public class CategoriesService
    {
        private readonly ApplicationDbContext _appDBContext;
        private readonly ILogger<CategoriesService> _logger;

        public CategoriesService(ApplicationDbContext appDBContext, ILogger<CategoriesService> logger)
        {
            _appDBContext = appDBContext;
            _logger = logger;
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            try
            {
                return await _appDBContext.Category.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all categories.");
                throw;
            }
        }

        public async Task<bool> InsertCategoryAsync(Category category)
        {
            try
            {
                await _appDBContext.Category.AddAsync(category);
                await _appDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while inserting a category.");
                throw;
            }
        }

        public async Task<Category> GetCategoryAsync(int Id)
        {
            try
            {
                Category category = await _appDBContext.Category.FirstOrDefaultAsync(c => c.Id.Equals(Id));
                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting a category by Id.");
                throw;
            }
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            try
            {
                _appDBContext.Category.Update(category);
                await _appDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a category.");
                throw;
            }
        }

        public async Task<bool> DeleteCategoryAsync(Category category)
        {
            try
            {
                _appDBContext.Remove(category);
                await _appDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a category.");
                throw;
            }
        }
    }
}
