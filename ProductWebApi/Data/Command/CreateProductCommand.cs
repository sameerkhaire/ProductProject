using BAL;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ProductWebApi.Data.Command
{
    public class CreateProductCommand: IRequest<Product>
    {
        public CreateProductCommand(string name, double DisAmount,int Expirymonth, int Expiryyear)
        {
            ProductName = name;
            DiscountAmount = DisAmount;
            ExpiryMonth = Expirymonth;
            ExpiryYear = Expiryyear;
        }
        public string ProductName { get; set; }
        public double DiscountAmount { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
    }
}
