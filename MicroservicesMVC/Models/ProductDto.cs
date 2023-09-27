using System.ComponentModel.DataAnnotations;

namespace MicroservicesMVC.Models
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double DiscountAmount { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        [Range(1,10)]
        public int Count { get; set; }
    }
}
