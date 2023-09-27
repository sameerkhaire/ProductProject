using BAL;
using MediatR;
using ProductWebApi.Data.Command;
using Repository.Interface;

namespace ProductWebApi.Data.Handler
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly IBaseRepo<Product> _productRepo;
        public DeleteProductHandler(IBaseRepo<Product> productRepo)
        {
            _productRepo = productRepo;
        }
        public Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            //var prod = _productRepo.FindProductAsync(request.Id);
            //if (prod == null) { return default; }
            return _productRepo.DeleteAsync(request.Id);
        }
    }
}
