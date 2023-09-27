using BAL;
using MediatR;

namespace ProductWebApi.Data
{
    public class GetProductListQuery: IRequest<List<Product>>
    {
    }
}
