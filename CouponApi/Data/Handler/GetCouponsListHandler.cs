using BAL;
using MediatR;
using Repository.Interface;

namespace CouponApi.Data.Handler
{
    public class GetCouponsListHandler : IRequestHandler<GetCouponListQuery, List<Coupon>>
    {
        private readonly IBaseRepo<Coupon> _couponRepo;
        public GetCouponsListHandler(IBaseRepo<Coupon> couponRepo)
        {
            _couponRepo = couponRepo;
        }
        public async Task<List<Coupon>> Handle(GetCouponListQuery request, CancellationToken cancellationToken)
        {
            return await _couponRepo.GetAllAsync();
        }
    }
}
