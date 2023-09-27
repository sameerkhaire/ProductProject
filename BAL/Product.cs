using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        [Required]
        public double DiscountAmount { get; set; }
        [Required]
        public int ExpiryMonth { get; set; }
        [Required]
        public int ExpiryYear { get; set; }

    }
}
