
using ResourceInventory.Model.PurchaseModel;

namespace ResourceInventory.Service.PurchaseService
{
    public interface IPurchaseRepository
    {
        Task<List<Purchase>> GetAllPurchases();
        Task<Purchase> GetPurchaseById(int purchaseId);
        Task<Purchase> AddPurchase(PurchaseDTO purchaseDTO, IFormFile image);
        Task<Purchase> UpdatePurchase(PurchaseDTO purchase, IFormFile image);
        Task DeletePurchase(int purchaseId);
        Task<bool> PurchaseExists(int purchaseId);
    }
}
