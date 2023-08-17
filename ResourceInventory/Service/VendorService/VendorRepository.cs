using Microsoft.EntityFrameworkCore;
using ResourceInventory.Model;
using ResourceInventory.Model.CategoryModel;
using ResourceInventory.Model.ProductModel;
using ResourceInventory.Model.VendorModel;

namespace ResourceInventory.Service.VendorService
{
    public class VendorRepository : IVendorRepository
    {
        private readonly ApplicationDBContext _context;

        public VendorRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Vendor> AddVendor(VendorDto vendor)
        {
            var category = await _context.Categories.FindAsync(vendor.CategoryID);
            var newVendor = new Vendor
            {
                VendorName = vendor.VendorName,
                Email = vendor.Email,
                Address = vendor.Address,
                Contact = vendor.Contact,
                Category = category
            };
            await _context.Vendors.AddAsync(newVendor);
            await _context.SaveChangesAsync();
            return newVendor;
        }

        public async Task DeleteVendor(int id)
        {
            var result = await _context.Vendors.FirstOrDefaultAsync(v => v.ID == id);
            if (result != null)
            {
                _context.Vendors.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetCategoryById(int id)
        {
            var categoryById = await _context.Categories.Where(c => c.Id == id).ToListAsync();
            return categoryById;
        }

        public async Task<IEnumerable<Vendor>> GetVendorById(int id)
        {
            var vendorById = await _context.Vendors.Where(v => v.ID == id).ToListAsync();
            return vendorById;
        }

        public async Task<List<Vendor>> GetVendorsByCategory(int categoryId)
        {
            var getVendors = await _context.Vendors.Where(v => v.CategoryID == categoryId).Include(p => p.Category).ToListAsync();
            return getVendors;
        }

        public async Task<Vendor> UpdateVendor(Vendor vendor)
        {
            var vendorExist = await _context.Vendors
                    .Include(v => v.Category) // This includes the category details of categoryId
                    .FirstOrDefaultAsync(v => v.ID == vendor.ID);

            if (vendorExist == null)
            {
                throw new InvalidOperationException("Vendor not found");
            }

            vendorExist.VendorName = vendor.VendorName;
            vendorExist.Email = vendor.Email;
            vendorExist.Address = vendor.Address;
            vendorExist.Contact = vendor.Contact;

            // Check if the CategoryId has changed
            if (vendor.CategoryID != vendorExist.CategoryID)
            {
                var updatedCategory = await _context.Categories.FindAsync(vendor.CategoryID);

                if (updatedCategory == null)
                {
                    throw new InvalidOperationException("Category not found");
                }

                vendorExist.Category = updatedCategory;
            }

            await _context.SaveChangesAsync();

            return vendorExist;
        }
    }
}
