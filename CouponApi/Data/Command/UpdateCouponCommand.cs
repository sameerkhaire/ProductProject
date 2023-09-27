using MediatR;

namespace CouponApi.Data.Command
{
    public class UpdateCouponCommand : IRequest<int>
    {
        public UpdateCouponCommand(int id,string name, string description, int couponamount)
        {
            Id = id;
            CouponName = name;
            CouponDescription = description;
            CouponAmount = couponamount;
        }
        public int Id { get; set; }
        public string CouponName { get; set; }
        public string CouponDescription { get; set; }
        public int CouponAmount { get; set; }
    }
}
