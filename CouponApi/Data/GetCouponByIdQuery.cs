using BAL;
using MediatR;

namespace CouponApi.Data
{
    public class GetCouponByIdQuery : IRequest<Coupon>
    {
            public int Id { get; set; }
    }

}
