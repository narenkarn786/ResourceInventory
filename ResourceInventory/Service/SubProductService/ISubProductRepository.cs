using ResourceInventory.Model.ProductModel;
using ResourceInventory.Model.SubProductModel;

namespace ResourceInventory.Service.SubProductService
{
    public interface ISubProductRepository
    {
        Task<IEnumerable<SubProduct>> GetSubproductByProduct(int productId);
        Task<SubProduct> AddSubProduct(SubProductDto subProduct);
        Task<SubProduct> UpdateSubProduct(SubProduct subProduct);
        Task<IEnumerable<SubProduct>> GetSubProductById(int id);
        Task DeleteSubProduct(int id);
    }
}
