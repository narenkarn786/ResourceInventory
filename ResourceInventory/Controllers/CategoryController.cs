using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceInventory.Model.CategoryModel;
using ResourceInventory.Service.CategoryService;

namespace ResourceInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost,Route("AddCategories")]
        public async Task<IActionResult> AddCategory(Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Data not able to insert");

                }
                else
                {
                    var add = await _categoryRepository.AddCategory(category);
                    return Ok(add);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Error creating new category");
            }
        }

        [HttpGet,Route("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                return Ok(await _categoryRepository.GetAllCategory());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving categories");
            }
        }

        [HttpGet,Route("GetCategoriesById")]
        public async Task<IActionResult> GetCategoriesById(int id)
        {
            try
            {
                var result=await _categoryRepository.GetCategoryById(id);
                if(result==null)
                    return BadRequest("Data not found");
                return Ok(result);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving Category by Id");
            }

            
        }
        [HttpPut("{id:int}"),Route("UpdateCategory")]
        public async Task<ActionResult<Category>> UpdateCategories(Category category,int id)
        {
            try
            {
                if (id != category.Id)
                    return BadRequest("Employee Id Mismatch");

                var categoryToUpdate = await _categoryRepository.GetCategoryById(id);
                if (categoryToUpdate == null)
                {
                    return NotFound($"Category with Id={id} not found");
                }
                return await _categoryRepository.UpdateCategory(category);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Updating categories");
            }
        }

        [HttpDelete("{id:int}"),Route("DeleteCategory")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            try
            {
                var categoryToDelete = await _categoryRepository.GetCategoryById(id);
                if (categoryToDelete == null)
                {
                    return NotFound($"Category with Id={id} not found");
                }
                await _categoryRepository.DeleteCategory(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting Category");
            }
        }
    }
}
