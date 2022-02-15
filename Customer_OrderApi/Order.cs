using Common.Model;

namespace OrderApi
{
    public class Order
    {
        public int Id { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public bool WaitForServer { get; set; } = false;
    }
}
