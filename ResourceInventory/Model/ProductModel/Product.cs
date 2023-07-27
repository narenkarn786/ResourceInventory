using ResourceInventory.Model.CategoryModel;
using System.ComponentModel.DataAnnotations;

namespace ResourceInventory.Model.ProductModel
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? ProductName { get; set; }
        public Category? Category { get; set; } 
        public int CategoryId { get; set; }
    }
}
