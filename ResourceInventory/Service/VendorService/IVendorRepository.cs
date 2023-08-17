using ResourceInventory.Model.CategoryModel;
using ResourceInventory.Model.VendorModel;

namespace ResourceInventory.Service.VendorService
{
    public interface IVendorRepository
    {
        Task<List<Vendor>> GetVendorsByCategory(int categoryId);
        Task<Vendor> AddVendor(VendorDto vendor);
        Task<IEnumerable<Vendor>> GetVendorById(int id);
        Task<IEnumerable<Category>> GetCategoryById(int id);
        Task<Vendor> UpdateVendor(Vendor vendor);
        Task DeleteVendor(int id);
    }
}
