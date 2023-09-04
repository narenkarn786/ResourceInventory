using Microsoft.AspNetCore.Mvc;
using ResourceInventory.Model.PurchaseModel;
using ResourceInventory.Service.PurchaseService;

namespace ResourceInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : Controller
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IWebHostEnvironment _env;
        public PurchaseController(IPurchaseRepository purchaseRepository, IWebHostEnvironment env)
        {
            _purchaseRepository = purchaseRepository;
            _env = env;

        }  

        [HttpGet("AllPurchases")]
        public async Task<IActionResult> AllPurchases() {
            try
            {
                var listOfPurchases = await _purchaseRepository.GetAllPurchases();
                return Ok(listOfPurchases);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving purchase list");

            }
        }

        [HttpGet("GetPurchaseById")]
        public async Task<IActionResult> GetPurchaseById(int id)
        {
            try
            {
                if (!await _purchaseRepository.PurchaseExists(id))
                {
                    return NotFound();
                }
                var purchaseById = await _purchaseRepository.GetPurchaseById(id);
                return Ok(purchaseById);
            }
            catch(Exception)
            {
                return BadRequest(ModelState);
            }
            
        }

        [HttpPost("AddPurchase")]
        public async Task<IActionResult> AddPurchase([FromForm] PurchaseDTO purchaseDTO)
        {
            try
            {
                if (purchaseDTO == null)
                {
                    return BadRequest(ModelState);
                }
                if (purchaseDTO.Invoice == null || purchaseDTO.Invoice.Length == 0)
                    return BadRequest("An image (photo) is required for the purchase create.");
                var purchaseToAdd = await _purchaseRepository.AddPurchase(purchaseDTO,purchaseDTO.Invoice);
                return Ok(purchaseToAdd);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new purchase");

            }
        }

        [HttpDelete("{purchaseId}")]
        public async Task<IActionResult> DeletePurchase(int purchaseId)
        {
            try
            {
                var purchaseToDelte = _purchaseRepository.GetPurchaseById(purchaseId);
                if (purchaseToDelte == null)
                {
                    return NotFound();
                }
                await _purchaseRepository.DeletePurchase(purchaseId);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting purchase");

            }
        }

        [HttpPut("UpdatePurchase")]
        public async Task<IActionResult> UpdatePurchase([FromForm] PurchaseDTO updateDTO, int id)
        {
            try
            {
                if (id != updateDTO.PurchaseId)
                    return BadRequest("Purchase Id not found");

                if (updateDTO.Invoice == null || updateDTO.Invoice.Length == 0)
                    return BadRequest("An image (photo) is required for the purchase update.");

                var purchaseToUpdate = await _purchaseRepository.UpdatePurchase(updateDTO, updateDTO.Invoice);

                if (purchaseToUpdate == null)
                {
                    return NotFound("Purchase not found");
                }

                return Ok(purchaseToUpdate);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating purchase");
            }
        }
        //public async Task<IActionResult> UpdatePurchase(Purchase purchase, int id)
        //{
        //    try
        //    {
        //        if (id != purchase.PurchaseID)
        //            return BadRequest("Purchase Id not found");

        //        var purchaseToUpdate = await _purchaseRepository.GetPurchaseById(id);
        //        if(purchaseToUpdate == null)
        //        {
        //            return NotFound();
        //        }
        //       var update = await _purchaseRepository.UpdatePurchase(purchase, purchase.Invoice);
        //        return Ok(update);
        //    }
        //    catch(Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error Updating purchase");
        //    }
        //}
    }
}
