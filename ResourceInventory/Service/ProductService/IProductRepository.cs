using ResourceInventory.Model.CategoryModel;
using ResourceInventory.Model.ProductModel;


namespace ResourceInventory.Service.ProductService
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsByCategory(int categoryId);
        Task<Product> AddProduct(AddProductDto product);
        Task<IEnumerable<Product>> GetProductById(int id);
        Task<IEnumerable<Category>> GetCategoryById(int id);
        Task<Product> UpdateProduct(Product product);
        Task DeleteProduct(int id);
    }
}
