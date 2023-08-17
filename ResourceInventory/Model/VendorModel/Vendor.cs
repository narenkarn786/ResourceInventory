using ResourceInventory.Model.CategoryModel;
using System.ComponentModel.DataAnnotations;

namespace ResourceInventory.Model.VendorModel
{
    public class Vendor
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string VendorName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Phone]
        public string Contact { get; set; } 
        public Category Category { get; set; }
        public int CategoryID { get; set; }
    }
}
