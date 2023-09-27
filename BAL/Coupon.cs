using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }
        public string CouponName { get; set; }
        public string CouponDescription { get; set; }
        public int CouponAmount { get; set; }
    }
}
