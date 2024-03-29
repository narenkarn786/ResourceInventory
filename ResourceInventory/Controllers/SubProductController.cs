﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceInventory.Model.ProductModel;
using ResourceInventory.Model.SubProductModel;
using ResourceInventory.Service.ProductService;
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

        [HttpGet, Route("GetSubProductByID")]
        public async Task<IActionResult> GetSubProductById(int id)
        {
            try
            {
                var result = await _subProductRepository.GetSubProductById(id);
                if (result == null)
                    return BadRequest("Data not found");
                return Ok(result);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving Sub-Product");
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

        [HttpPut,Route("UpdateSubProduct")]
        public async Task<IActionResult> UpdateSubProduct(SubProduct subProduct, int id)
        {
            try
            {
                if (id != subProduct.Id)
                {
                    return BadRequest("Sub-Product ID Mismatch");
                }

           
                var subProductToBeUpdated = await _subProductRepository.GetSubProductById(id);
                if (subProductToBeUpdated == null)
                {
                    return NotFound($"Sub-Product with Id={subProduct.Id} not found");
                }

                var updatedSubProduct = await _subProductRepository.UpdateSubProduct(subProduct);
                return Ok(updatedSubProduct);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating Sub-Product");
            }
        }

        [HttpDelete, Route("Delete-SubProduct")]
        public async Task<IActionResult> DeleteSubProduct(int id)
        {
            try
            {
                var subProductToDelete = await _subProductRepository.GetSubProductById(id);
                if (subProductToDelete == null)
                {
                    return NotFound($"Sub-Product with Id={id} not found");
                }
                await _subProductRepository.DeleteSubProduct(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting Sub-Product");
            }
        }
    }
}
