using BAL;
using MediatR;
using Repository.Interface;

namespace CouponApi.Data.Handler
{
    public class GetCouponByIdHandler : IRequestHandler<GetCouponByIdQuery, Coupon>
    {
            private readonly IBaseRepo<Coupon> _couponRepo;
            public GetCouponByIdHandler(IBaseRepo<Coupon> couponRepo)
            {
                _couponRepo = couponRepo;
            }

        public async Task<Coupon> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
        {
            return await _couponRepo.FindProductAsync(request.Id);
        }
    }
}
