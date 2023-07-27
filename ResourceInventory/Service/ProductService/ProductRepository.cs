using Microsoft.EntityFrameworkCore;
using ResourceInventory.Model;
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
            var getproduct=await applicationDBContext.Products.Where(p=>p.CategoryId==categoryId).Include(p=>p.Category).ToListAsync();          
            return getproduct;
           
        }

      public async  Task<Product> AddProduct(AddProductDto product)
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


    }
}
