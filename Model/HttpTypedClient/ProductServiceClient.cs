using Common.Model;
using System.Net.Http.Json;

namespace Common.HttpTypedClient
{
    //typed client
    public class ProductServiceClient : IProductServiceClient
    {
        private readonly HttpClient _httpClient;

        public ProductServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //TODO from configuration
            _httpClient.BaseAddress = new Uri("http://localhost:5166");
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
            => await _httpClient.GetFromJsonAsync<IEnumerable<Product>>("api/product/GetProducts");
    }
}
