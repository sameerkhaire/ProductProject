using BAL;
using MediatR;

namespace ProductWebApi.Data
{
    public class GetProductByIdQuery:IRequest<Product>
    {
        public int Id { get; set; }
    }
}
