using ResourceInventory.Model.CategoryModel;

namespace ResourceInventory.Service.CategoryService
{
    public interface ICategoryRepository
    {
        Task<Category> AddCategory(Category category);
        Task<IEnumerable<Category>> GetAllCategory();
        Task<Category> GetCategoryById(int id);
        Task<Category> UpdateCategory(Category category);
        Task DeleteCategory(int id);
    }
}
