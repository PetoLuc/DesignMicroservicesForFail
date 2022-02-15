using Common.Model;

namespace ProductApi
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
        {
            var random = new Random().Next(1, 25);

            if (random > 10)
            {
                throw new Exception
                ("An exception occurred in Product Service....");
            }

            List<Product> products = new List<Product>()
        {
            new Product { Id = 1, Code = "P0001",
                Name = "Lenovo Laptop",
                Price = 1200.00 },
            new Product { Id = 2, Code = "P0002",
                Name = "DELL Laptop",
                Price = 1300.00 },
            new Product { Id = 3, Code = "P0003",
                Name = "Asus Laptop",
                Price = 1100.00 },
            new Product { Id = 4, Code = "P0004",
                Name = "HP Laptop",
                Price = 1400.00 }
        };

            return products;
        }
    }
}
