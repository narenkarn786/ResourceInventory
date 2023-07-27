using ResourceInventory.Model.ProductModel;
using System.ComponentModel.DataAnnotations;

namespace ResourceInventory.Model.SubProductModel
{
    public class SubProduct
    {
        [Key]
        public int Id { get; set; }
        public string? Subproductname { get; set; }
        public Product? Product { get; set; }
        public int ProductId { get; set; }
    }
}
