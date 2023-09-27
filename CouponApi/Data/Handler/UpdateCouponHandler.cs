using BAL;
using CouponApi.Data.Command;
using MediatR;
using Repository.Interface;

namespace CouponApi.Data.Handler
{
    public class UpdateCouponHandler : IRequestHandler<UpdateCouponCommand, int>
    {
        private readonly IBaseRepo<Coupon> _couponRepo;
        public UpdateCouponHandler(IBaseRepo<Coupon> couponRepo)
        {
            _couponRepo = couponRepo;
        }
        public async Task<int> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            var coup = await _couponRepo.FindProductAsync(request.Id);
            if (coup != null)
            {
                coup.CouponName = request.CouponName;
                coup.CouponDescription = request.CouponDescription;
                coup.CouponAmount = request.CouponAmount;
                return await _couponRepo.UpdateAsync(coup);
            }
            return default;
        }
    }
}
