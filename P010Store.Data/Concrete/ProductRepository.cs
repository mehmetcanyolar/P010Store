using Microsoft.EntityFrameworkCore;
using P010Store.Data.Abstract;
using P010Store.Entities;

namespace P010Store.Data.Concrete
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DatabaseContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<Product>> GetAllProductsByCategoriesBrandsAsync()
        {
            return await context.Products.Include(c=>c.Category).Include(b=>b.Brand).ToListAsync();
        }
    }
}
