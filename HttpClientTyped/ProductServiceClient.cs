using Model;
using System.Net.Http.Json;

namespace HttpClientTyped
{
    //typed client
    public class ProductServiceClient : IProductServiceClient
    {
        private readonly HttpClient _httpClient;

        public ProductServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5166");
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
            => await _httpClient.GetFromJsonAsync<IEnumerable<Product>>("api/product/GetProducts");
    }
}
