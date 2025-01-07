using ProductApi.Domain.Models.Products;

namespace ProductApi.Domain.Interfaces.Products
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetPopularProducts();
        Task Add(Product product);
    }
}
