﻿using Microsoft.EntityFrameworkCore;
using P010Store.Data.Abstract;
using P010Store.Entities;

namespace P010Store.Data.Concrete
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext _context) : base(_context)
        {
        }

        public async Task<Category> GetProductsByCategoryAsync(int id)
        {


            return await context.Categories.Where(c => c.Id == id).AsNoTracking().Include(p => p.Products).FirstOrDefaultAsync();
         }
    }
}
