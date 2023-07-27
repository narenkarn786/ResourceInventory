using ResourceInventory.Model.SubProductModel;

namespace ResourceInventory.Service.SubProductService
{
    public interface ISubProductRepository
    {
        Task<IEnumerable<SubProduct>> GetSubproductByProduct(int productId);
        Task<SubProduct> AddSubProduct(SubProductDto subProduct);
    }
}
