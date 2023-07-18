using P010Store.Entities;

namespace P010Store.Data.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsByCategoriesBrandsAsync();
    }
}
