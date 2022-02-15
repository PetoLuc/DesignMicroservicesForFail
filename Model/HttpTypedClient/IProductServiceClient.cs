using Common.Model;

namespace Common.HttpTypedClient
{
    public interface IProductServiceClient
    {
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
