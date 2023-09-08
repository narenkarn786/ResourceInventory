using ResourceInventory.Model.CategoryModel;
using ResourceInventory.Model.ProductModel;
using ResourceInventory.Model.SubProductModel;

namespace ResourceInventory.Service.PurchaseService
{
    public class PurchaseDTO
    {
        public int PurchaseId { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int TotalPrice { get; set; }
        public string? PaymentStatus { get; set; }
        public IFormFile? Invoice { get; set; }
        public string? Notes { get; set; }
        public int CategoryId { get; set;}
        public int ProductId { get; set;}
        public int SubProductId { get; set;}
    }
}
