using ResourceInventory.Model.ProductModel;
using ResourceInventory.Service.CategoryService;

namespace ResourceInventory.Service.ProductService
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsByCategory(int categoryId);
        Task<Product> AddProduct(AddProductDto product);
    }
}
