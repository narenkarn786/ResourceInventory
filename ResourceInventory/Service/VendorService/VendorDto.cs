using System.ComponentModel.DataAnnotations;

namespace ResourceInventory.Service.VendorService
{
    public class VendorDto
    {
        public string VendorName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        [Phone]
        public string Contact { get; set; }
        public int CategoryID { get; set; }
    }
}
