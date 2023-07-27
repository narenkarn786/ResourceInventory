using Microsoft.EntityFrameworkCore;
using ResourceInventory.Model;
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

        public async Task<IEnumerable<SubProduct>> GetSubproductByProduct(int productId)
        {
            var getsubproduct = await applicationDBContext.SubProducts.Where(s => s.ProductId == productId).Include(s=>s.Product).ToListAsync();
            return getsubproduct;
        }


    }
}
