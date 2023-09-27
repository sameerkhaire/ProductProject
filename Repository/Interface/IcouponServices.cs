using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IcouponServices
    {
        Coupon GetCouponCode(string couponcode);
    }
}
