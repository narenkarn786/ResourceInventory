using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceInventory.Model;
using ResourceInventory.Model.CategoryModel;
using System;

namespace ResourceInventory.Service.CategoryService
{
  
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext applicationDBContext;

        public CategoryRepository(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }

        public async Task<Category> AddCategory(Category category)
        {
            var result= await applicationDBContext.AddAsync(category);
            await applicationDBContext.SaveChangesAsync();
            return result.Entity;
        }

       
        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return await applicationDBContext.Categories.ToListAsync();
            
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var result= await applicationDBContext.Categories.FindAsync(id);
            return result;
           
            
        }

        public async Task<Category> UpdateCategory(Category category)
        {
           var result=await applicationDBContext.Categories.FirstOrDefaultAsync(c=>c.Id==category.Id);
            if (result!= null)
            {
                result.Name = category.Name;
                result.Active = category.Active;
                await applicationDBContext.SaveChangesAsync();
                return result;
            }
            return null;
             
        }

        public async Task DeleteCategory(int id)
        {
            var result = await applicationDBContext.Categories.FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                applicationDBContext.Categories.Remove(result);
                await applicationDBContext.SaveChangesAsync();
            }
        }

    }
}
