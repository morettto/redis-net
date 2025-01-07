using ProductApi.Domain.Interfaces.Products;
using ProductApi.Domain.Interfaces.Redis;
using ProductApi.Domain.Models.Products;
using ProductApi.Infrastructure.Contexts;

namespace ProductApi.Application.Services.Products
{
    public class ProductService : IProductService
    {
        private const string CacheKey = "Products";
        private const string LastProductedAddedKey = "LastProductedAddedKey";

        private readonly IRedisService _redisService;
        private ProductApiContext _dbContext;

        public ProductService(IRedisService redisService, ProductApiContext dbContext)
        {
            _redisService = redisService;
            _dbContext = dbContext;
        }

        public async Task Add(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _redisService.SetAsync(LastProductedAddedKey, $"{product.Id} {product.Name}");
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<Product>> GetPopularProducts()
        {
            // Tenta obter os produtos do cache
            var cachedProducts = await _redisService.GetAsync<IEnumerable<Product>>(CacheKey);

            if (cachedProducts != null && cachedProducts.Any())
                return cachedProducts;

            // Simula uma consulta pesada no banco
            var products = GetPopularProductsFromDatabase();

            // Salva os produtos no cache por 5 minutos
            await _redisService.SetAsync(CacheKey, products);

            return products;
        }

        private List<Product> GetPopularProductsFromDatabase()
        {
            // Dados simulados
            return _dbContext.Products.ToList();
        }
    }
}
