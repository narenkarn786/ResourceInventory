using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceInventory.Model.CategoryModel;
using ResourceInventory.Model.ProductModel;
using ResourceInventory.Service.CategoryService;
using ResourceInventory.Service.ProductService;

namespace ResourceInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet,Route("GetProductByCategory")]
        public async Task<IActionResult> GetProductByCategory(int categoryId)
        {
            try
            {                
                var getproduct = await productRepository.GetProductsByCategory(categoryId);
                if (getproduct == null)
                {
                  return  BadRequest("Data not found");
                }               
                    return Ok(getproduct);                 
                
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
           
        }

        [HttpPost,Route("AddNewProduct")]
        public async Task<IActionResult> AddProduct(AddProductDto product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Data cannot be saved");
                }
                var addproduct = await productRepository.AddProduct(product);
              return Ok(addproduct);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new product");
            }

            
            

        }
    }
}
