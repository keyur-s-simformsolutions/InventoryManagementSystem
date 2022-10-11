using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Data
{
    public class CategoriesService
    {

        private readonly ApplicationDbContext _appDBContext;



        public CategoriesService(ApplicationDbContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _appDBContext.Category.ToListAsync();
        }
        #region Insert Category
        public async Task<bool> InsertCategoryAsync(Category category)
        {
            await _appDBContext.Category.AddAsync(category);
            await _appDBContext.SaveChangesAsync();
            return true;
        }
        #endregion
        #region Get Category by Id
        public async Task<Category> GetCategoryAsync(int Id)
        {
            Category category = await _appDBContext.Category.FirstOrDefaultAsync(c => c.Id.Equals(Id));
            return category;
        }
        #endregion

        #region Update Category
        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            _appDBContext.Category.Update(category);
            await _appDBContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region Category Item
        public async Task<bool> DeleteCategoryAsync(Category category)
        {
            _appDBContext.Remove(category);
            await _appDBContext.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
