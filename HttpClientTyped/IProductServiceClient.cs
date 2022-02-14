using Model;

namespace HttpClientTyped
{
    public interface IProductServiceClient
    {
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
