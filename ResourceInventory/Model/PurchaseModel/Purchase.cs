using ResourceInventory.Model.CategoryModel;
using ResourceInventory.Model.ProductModel;
using ResourceInventory.Model.SubProductModel;
using ResourceInventory.Service.PurchaseService;
using System.ComponentModel.DataAnnotations;

namespace ResourceInventory.Model.PurchaseModel
{
    public class Purchase
    {
        [Key]
        public int PurchaseID { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int TotalPrice { get; set; }
        public string PaymentStatus { get; set; }
        public string Invoice { get; set; }
        public string Notes { get; set; }
        public int? CategoryId { get; set; }
        public int? ProductId { get; set; }
        public int? SubProductId { get; set; }
        public Category? Category { get; set; }
        public Product? Product { get; set; }
        public SubProduct? SubProduct { get; set; }

    }
}
