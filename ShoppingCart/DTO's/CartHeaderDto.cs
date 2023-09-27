using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.DTO_s
{
    public class CartHeaderDto
    {
        public int CartHeaderId { get; set; }
        public int UserId { get; set; }
        public string? CouponCode { get; set; }
        public double discount { get; set; }
        public double CartTotal { get; set; }
    }
}
