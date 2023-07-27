using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceInventory.Model.SubProductModel;
using ResourceInventory.Service.SubProductService;

namespace ResourceInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubProductController : ControllerBase
    {
        private readonly ISubProductRepository _subProductRepository;

        public SubProductController(ISubProductRepository subProductRepository)
        {
            _subProductRepository = subProductRepository;
        }

        [HttpGet,Route("GetSubproductByProduct")]
        public async Task<IActionResult> GetSubproductByProduct(int productId)
        {
            try
            {
                
                var getsubproduct = await _subProductRepository.GetSubproductByProduct(productId);
                if(getsubproduct == null)              
                    return NotFound("Data not found");                
                return Ok(getsubproduct);
            }
            catch (Exception) {
                return StatusCode(StatusCodes.Status404NotFound, "Error retrieving data");
            }
           
        }


        [HttpPost,Route("AddSubProduct")]
        public async Task<IActionResult> AddSubProduct(SubProductDto subProduct)
        {
            try
            {
                if (subProduct == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Data Not Found");
                }
                var addsubproduct = await _subProductRepository.AddSubProduct(subProduct);
                return Ok(addsubproduct);
            }
            catch (Exception ) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error inserting new subproduct");
            }

            }          
    }
}
