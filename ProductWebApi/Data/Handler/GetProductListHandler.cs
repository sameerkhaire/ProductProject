using BAL;
using MediatR;
using Repository.Interface;

namespace ProductWebApi.Data.Handler
{
    public class GetProductListHandler : IRequestHandler<GetProductListQuery, List<Product>>
    {
        private readonly IBaseRepo<Product> _productRepo;
        public GetProductListHandler(IBaseRepo<Product> prtoductrepo)
        {
            _productRepo = prtoductrepo;
        }
       public async Task<List<Product>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            return  await _productRepo.GetAllAsync();
        }
    }
}
