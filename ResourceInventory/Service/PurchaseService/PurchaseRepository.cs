using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ResourceInventory.Model;
using System.IO;
using ResourceInventory.Model.CategoryModel;
using ResourceInventory.Model.ProductModel;
using ResourceInventory.Model.PurchaseModel;
using ResourceInventory.Model.SubProductModel;
using static System.Net.Mime.MediaTypeNames;

namespace ResourceInventory.Service.PurchaseService
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        public PurchaseRepository(ApplicationDBContext context, IWebHostEnvironment environment, IMapper mapper) { 
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<Purchase> AddPurchase(PurchaseDTO purchaseDTO, IFormFile image)
        {
            var product = await _context.Products.FindAsync(purchaseDTO.ProductId);
            var category = await _context.Categories.FindAsync(purchaseDTO.CategoryId);
            var subProduct = await _context.SubProducts.FindAsync(purchaseDTO.SubProductId);

            if (product == null || category == null || subProduct == null)
            {
                return null;
            }
            var addPurchase = new Purchase
            {
                DateOfPurchase = purchaseDTO.DateOfPurchase,
                Quantity = purchaseDTO.Quantity,
                UnitPrice = purchaseDTO.UnitPrice,
                TotalPrice = purchaseDTO.Quantity * purchaseDTO.UnitPrice,
                PaymentStatus = purchaseDTO.PaymentStatus,
                //Invoice = purchaseDTO.Invoice,
                Notes = purchaseDTO.Notes,
                Category = category,
                Product = product,
                SubProduct = subProduct
            };
            if (image != null && image.Length > 0)
            {
                addPurchase.Invoice = SaveImage(image);
            }
            
            await _context.AddAsync(addPurchase);
            await _context.SaveChangesAsync();
            return addPurchase;
        }

   

        public async Task DeletePurchase(int purchaseId)
        {
            var purchaseToDelete = await _context.Purchases.FirstOrDefaultAsync(p => p.PurchaseID == purchaseId);
            if(purchaseToDelete != null)
            {
                _context.Purchases.Remove(purchaseToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Purchase>> GetAllPurchases()
        {
            var allPurchases = await _context.Purchases.ToListAsync();
            return allPurchases;
        }

        public async Task<Purchase> GetPurchaseById(int purchaseId)
        {
            var purchaseById = await _context.Purchases.Where(x => x.PurchaseID == purchaseId).FirstOrDefaultAsync();
           
            return purchaseById;
        }

        public async Task<bool> PurchaseExists(int purchaseId)
        {
            var purchase = await _context.Purchases.AnyAsync(x=>x.PurchaseID == purchaseId);
            return purchase;
        }

        public async Task<Purchase> UpdatePurchase(PurchaseDTO purchaseDTO, IFormFile image)
        {
            var purchaseToUpdate = await _context.Purchases
                .Include(x => x.SubProduct)
                .FirstOrDefaultAsync(p => p.PurchaseID == purchaseDTO.PurchaseId);

            if (purchaseToUpdate == null)
            {
                throw new InvalidOperationException("Purchase not found");
            }

            purchaseToUpdate.Quantity = purchaseDTO.Quantity;
            purchaseToUpdate.UnitPrice = purchaseDTO.UnitPrice;
            purchaseToUpdate.TotalPrice = purchaseDTO.Quantity * purchaseDTO.UnitPrice;
            purchaseToUpdate.PaymentStatus = purchaseDTO.PaymentStatus;
            purchaseToUpdate.Notes = purchaseDTO.Notes;
            purchaseToUpdate.DateOfPurchase = purchaseDTO.DateOfPurchase;

            if (image != null && image.Length > 0)
            {
                DeleteExistingImage(purchaseToUpdate); // Optionally, delete the old image if necessary.
                purchaseToUpdate.Invoice = SaveImage(image);
            }

            if (purchaseDTO.SubProductId != purchaseToUpdate.SubProductId)
            {
                var updatedSubProduct = await _context.SubProducts.FindAsync(purchaseDTO.SubProductId);

                if (updatedSubProduct == null)
                {
                    throw new InvalidOperationException("SubProduct not found");
                }

                purchaseToUpdate.SubProduct = updatedSubProduct;
            }

            await _context.SaveChangesAsync();
            return purchaseToUpdate;
        }

        private string SaveImage(IFormFile image)
        { 
            //string a = Path.GetFullPath(_environment.WebRootPath);
            //var uploadsFolderPath = Path.Combine(_environment.WebRootPath,"Uploads");
            //var uploadsFolderPath = Path.Combine(_environment.WebRootPath + "\\Test\\");
            string uploadsFolderPath = Path.Combine(_environment.WebRootPath, "Image");

            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var uniqueFileName = Path.GetRandomFileName() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            // Return the relative path of the image, which can be saved in the database.
            return "/Image/" + uniqueFileName;
        }

        // Helper method to delete an existing image when updating a task.
        private void DeleteExistingImage(Purchase purchase)
        {
            if (string.IsNullOrWhiteSpace(purchase.Invoice))
                return;

            var existingFilePath = Path.Combine(_environment.WebRootPath, purchase.Invoice.TrimStart('/'));
            if (System.IO.File.Exists(existingFilePath))
                System.IO.File.Delete(existingFilePath);
        }
    }
}
