using MediatR;

namespace ProductWebApi.Data.Command
{
    public class UpdateProductCommand:IRequest<int>
    {
        public UpdateProductCommand(int id, string Name, double DisAmount, int ExpiMonth, int ExpirYear)
        {
            Id = id;
            ProductName = Name;
            DiscountAmount = DisAmount;
            ExpiryMonth=ExpiMonth;
            ExpiryYear=ExpirYear;
        }
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double DiscountAmount { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
    }
}
