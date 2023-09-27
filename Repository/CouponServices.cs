using BAL;
using DAL.Data;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CouponServices : IcouponServices
    {
        private readonly ProductDbContext _db;
        public CouponServices(ProductDbContext db)
        {
            _db = db;
        }
        public Coupon GetCouponCode(string couponcode)
        {
            var data= _db.coupons.FirstOrDefault(x => x.CouponName.ToLower().Contains(couponcode.ToLower()));
            return data;
        }
    }
}
