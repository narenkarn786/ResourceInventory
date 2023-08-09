using Microsoft.EntityFrameworkCore;
using ResourceInventory.Model;
using ResourceInventory.Model.ProductModel;
using ResourceInventory.Model.SubProductModel;

namespace ResourceInventory.Service.SubProductService
{
    public class SubProductRepository : ISubProductRepository
    {
        private readonly ApplicationDBContext applicationDBContext;

        public SubProductRepository(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }

        public async Task<SubProduct> AddSubProduct(SubProductDto subProduct)
        {
            var product = await applicationDBContext.Products.FindAsync(subProduct.ProductId);
            var addsubproduct = new SubProduct
            {
                Subproductname = subProduct.SubProductName,
                Product = product,
            };
           await applicationDBContext.AddAsync(addsubproduct);
            await applicationDBContext.SaveChangesAsync();
            return addsubproduct;
        }

        public async Task DeleteSubProduct(int id)
        {
            var result = await applicationDBContext.SubProducts.FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                applicationDBContext.SubProducts.Remove(result);
                await applicationDBContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SubProduct>> GetSubProductById(int id)
        {
            var subProductById = await applicationDBContext.SubProducts.Where(s => s.Id == id).ToListAsync();
            return subProductById;
        }

        public async Task<IEnumerable<SubProduct>> GetSubproductByProduct(int productId)
        {
            var getsubproduct = await applicationDBContext.SubProducts.Where(s => s.ProductId == productId).Include(s=>s.Product).ToListAsync();
            return getsubproduct;
        }

        public async Task<SubProduct> UpdateSubProduct(SubProduct subProduct)
        {
            var subProductExist = await applicationDBContext.SubProducts
                    .Include(p => p.Product) // This includes the Product details of ProductId
                    .FirstOrDefaultAsync(p => p.Id == subProduct.Id);

            if (subProductExist == null)
            {
                throw new InvalidOperationException("Sub-Product not found");
            }

            subProductExist.Subproductname = subProduct.Subproductname;

            // Check if the ProductId has changed
            if (subProduct.ProductId != subProductExist.ProductId)
            {
                var updatedProduct = await applicationDBContext.Products.FindAsync(subProduct.ProductId);

                if (updatedProduct == null)
                {
                    throw new InvalidOperationException("Product not found");
                }

                subProductExist.Product = updatedProduct;
            }

            await applicationDBContext.SaveChangesAsync();

            return subProductExist;
        }
    }
}
