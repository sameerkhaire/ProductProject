using BAL;
using MediatR;

namespace CouponApi.Data.Command
{
    public class CreateCouponCommand:IRequest<Coupon>
    {
        public CreateCouponCommand(string name, string description, int couponamount)
        {
            CouponName = name;
            CouponDescription = description;
            CouponAmount = couponamount;
        }
        public string CouponName { get; set; }
        public string CouponDescription { get; set; }
        public int CouponAmount { get; set; }

    }
}
