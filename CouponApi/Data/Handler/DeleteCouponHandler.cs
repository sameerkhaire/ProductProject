using BAL;
using CouponApi.Data.Command;
using MediatR;
using Repository.Interface;

namespace CouponApi.Data.Handler
{
    public class DeleteCouponHandler : IRequestHandler<DeleteCouponCommand, int>
    {

            private readonly IBaseRepo<Coupon> _couponRepo;
            public DeleteCouponHandler(IBaseRepo<Coupon> couponRepo)
            {
              _couponRepo = couponRepo;
            }

        public Task<int> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            return _couponRepo.DeleteAsync(request.Id);
        }


    }
}
