using Microsoft.EntityFrameworkCore;
using ResourceInventory.Model;
using ResourceInventory.Model.CategoryModel;
using ResourceInventory.Model.ProductModel;
using ResourceInventory.Service.CategoryService;

namespace ResourceInventory.Service.ProductService
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext applicationDBContext;
        public ProductRepository(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }


        public async Task<List<Product>> GetProductsByCategory(int categoryId)
        {
            var getproduct = await applicationDBContext.Products.Where(p => p.CategoryId == categoryId).Include(p => p.Category).ToListAsync();
            return getproduct;

        }

        public async Task<Product> AddProduct(AddProductDto product)
        {
            var category = await applicationDBContext.Categories.FindAsync(product.CategoryId);
            var newProduct = new Product
            {
                ProductName = product.ProductName,
                Category = category
            };
            await applicationDBContext.Products.AddAsync(newProduct);
            await applicationDBContext.SaveChangesAsync();
            return newProduct;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var productExist = await applicationDBContext.Products
                    .Include(p => p.Category) // This includes the category details of categoryId
                    .FirstOrDefaultAsync(p => p.Id == product.Id);

            if (productExist == null)
            {
                throw new InvalidOperationException("Product not found");
            }

            productExist.ProductName = product.ProductName;

            // Check if the CategoryId has changed
            if (product.CategoryId != productExist.CategoryId)
            {
                var updatedCategory = await applicationDBContext.Categories.FindAsync(product.CategoryId);

                if (updatedCategory == null)
                {
                    throw new InvalidOperationException("Category not found");
                }

                productExist.Category = updatedCategory;
            }

            await applicationDBContext.SaveChangesAsync();

            return productExist;

        }

        public async Task DeleteProduct(int id)
        {
            var result = await applicationDBContext.Products.FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                applicationDBContext.Products.Remove(result);
                await applicationDBContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetCategoryById(int id)
        {
            var categoryById = await applicationDBContext.Categories.Where(c => c.Id == id).ToListAsync();
            return categoryById;
        }

        public async Task<IEnumerable<Product>> GetProductById(int id)
        {
            var productById = await applicationDBContext.Products.Where(c => c.Id == id).ToListAsync();
            return productById;
        }
    }
}
