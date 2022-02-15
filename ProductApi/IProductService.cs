using Common.Model;

namespace ProductApi
{
    public interface IProductService
    {
        public List<Product> GetProducts();
    }
}
