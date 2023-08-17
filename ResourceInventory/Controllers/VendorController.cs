using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceInventory.Model.CategoryModel;
using ResourceInventory.Model.ProductModel;
using ResourceInventory.Model.VendorModel;
using ResourceInventory.Service.ProductService;
using ResourceInventory.Service.VendorService;

namespace ResourceInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorRepository _vendorRepository;

        public VendorController(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        [HttpGet, Route("GetVendorByCategory")]
        public async Task<IActionResult> GetVendorByCategory(int categoryId)
        {
            try
            {
                var getvendor = await _vendorRepository.GetVendorsByCategory(categoryId);
                if (getvendor == null)
                {
                    return BadRequest("Data not found");
                }
                return Ok(getvendor);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }

        }

        [HttpGet, Route("GetVendorById")]
        public async Task<IActionResult> GetVendorById(int id)
        {
            try
            {
                var result = await _vendorRepository.GetVendorById(id);
                if (result == null)
                    return BadRequest("Data not found");
                return Ok(result);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving Vendor");
            }
        }
        [HttpPost, Route("AddVendor")]
        public async Task<IActionResult> AddVendor(VendorDto vendor)
        {
            try
            {
                if (vendor == null)
                {
                    return BadRequest("Data cannot be saved");
                }
                var addvendor = await _vendorRepository.AddVendor(vendor);
                return Ok(addvendor);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new Vendor");
            }
        }
        [HttpPut, Route("UpdateVendor")]
        public async Task<IActionResult> UpdateVendor(Vendor vendor, int id)
        {
            try
            {
                if (id != vendor.ID)
                    return BadRequest("Vendor ID Mismatch");

                var vendorToUpdate = await _vendorRepository.GetVendorById(id);
                if (vendorToUpdate == null)
                {
                    return NotFound($"vendor with Id={vendor.ID} not found");
                }

                var updateVendor = await _vendorRepository.UpdateVendor(vendor);
                return Ok(updateVendor);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating vendor");
            }
        }
        [HttpDelete, Route("DeleteVendor")]
        public async Task<ActionResult> DeleteVendor(int id)
        {
            try
            {
                var vendorToDelete = await _vendorRepository.GetVendorById(id);
                if (vendorToDelete == null)
                {
                    return NotFound($"Vendor with Id={id} not found");
                }
                await _vendorRepository.DeleteVendor(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting vendor");
            }
        }
    }
}
