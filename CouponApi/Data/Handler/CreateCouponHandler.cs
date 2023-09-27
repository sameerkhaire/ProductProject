using BAL;
using CouponApi.Data.Command;
using MediatR;
using Repository;
using Repository.Interface;

namespace CouponApi.Data.Handler
{
    public class CreateCouponHandler : IRequestHandler<CreateCouponCommand, Coupon>
    {
            private readonly IBaseRepo<Coupon> _couponRepo;
            public CreateCouponHandler(IBaseRepo<Coupon> couponRepo)
            {
                _couponRepo=couponRepo;
            }

        public async Task<Coupon> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            Coupon coupons = new Coupon()
            {
                CouponName = request.CouponName,
                CouponDescription = request.CouponDescription,
                CouponAmount = request.CouponAmount

            };
            return await _couponRepo.AddAsync(coupons);

        }
    }
}
