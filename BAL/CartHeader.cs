using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class CartHeader
    {
        [Key]
        public int CartHeaderId { get; set; }
        public int UserId { get; set; }
        public string? CouponCode { get; set; }
        [NotMapped]
        public double discount { get; set; }
        [NotMapped]
        public double CartTotal { get; set; }
    }
}
