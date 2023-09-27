using MediatR;

namespace CouponApi.Data.Command
{
    public class DeleteCouponCommand : IRequest<int>
    {
            public int Id { get; set; }
    }
}
