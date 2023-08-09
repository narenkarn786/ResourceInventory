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

        [HttpGet, Route("GetProductByCategory")]
        public async Task<IActionResult> GetProductByCategory(int categoryId)
        {
            try
            {
                var getproduct = await productRepository.GetProductsByCategory(categoryId);
                if (getproduct == null)
                {
                    return BadRequest("Data not found");
                }
                return Ok(getproduct);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }

        }

        [HttpPost, Route("AddNewProduct")]
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new product");
            }
        }
        [HttpPut, Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            try
            {
                var updatedProduct = await productRepository.UpdateProduct(product);

                if (updatedProduct == null)
                {
                    return NotFound($"Product with Id={product.Id} not found");
                }

                return Ok(updatedProduct);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating product");
            }
        }

        [HttpDelete, Route("DeleteProduct")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            try
            {
                var productToDelete = await productRepository.GetProductById(id);
                if (productToDelete == null)
                {
                    return NotFound($"Product with Id={id} not found");
                }
                await productRepository.DeleteProduct(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting Product");
            }
        }
    }
}
