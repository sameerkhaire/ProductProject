using BAL;
using DAL.Data;
using MediatR;
using Repository.Interface;

namespace ProductWebApi.Data.Handler
{
    public class GetProductHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IBaseRepo<Product> _productRepo;
        public GetProductHandler(IBaseRepo<Product> productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productRepo.FindProductAsync(request.Id);
        }
    }
}
