using BAL;
using MediatR;

namespace CouponApi.Data
{
    public class GetCouponListQuery:IRequest<List<Coupon>>
    {
    }
}
