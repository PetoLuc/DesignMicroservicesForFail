using Microsoft.AspNetCore.Mvc;
using Common.Model;
using Polly.CircuitBreaker;
using Common.HttpTypedClient;

namespace OrderApi
{
    [Route("api/[Controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IProductServiceClient _productServiceClient;
        public OrderController(IHttpClientFactory httpClientFactory, IProductServiceClient productServiceClient)
        {
            this._httpClientFactory = httpClientFactory;
            this._productServiceClient = productServiceClient;
        }

        //using client factory
        [HttpGet("GetOrderDetails1/{orderId}")]
        public async Task<ActionResult<Order>> GetOrderDetails1(int orderId)
        {
            var client = _httpClientFactory.CreateClient("ProductApiClient1");
            var products = await client.GetFromJsonAsync<IEnumerable<Product>>("api/product/GetProducts");
            return new Order() { Id = orderId, Products = products };
        }

        [HttpGet("GetOrderDetails2/{orderId}")]
        public async Task<ActionResult<Order>> GetOrderDetails2(int orderId)
        {
            try
            {
                var products = await _productServiceClient.GetProductsAsync();
                return new Order() { Id = orderId, Products = products, WaitForServer = false };
            }
            catch (BrokenCircuitException)
            {
                return new Order() { WaitForServer = true };
            }
        }
    }
}
