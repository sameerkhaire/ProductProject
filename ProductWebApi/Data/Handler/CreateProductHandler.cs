using BAL;
using MediatR;
using ProductWebApi.Data.Command;
using Repository.Interface;

namespace ProductWebApi.Data.Handler
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IBaseRepo<Product> _productRepo;
        public CreateProductHandler(IBaseRepo<Product> prtoductrepo)
        {
            _productRepo = prtoductrepo;
        }
        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product prod = new Product()
            {
                ProductName = request.ProductName,
                DiscountAmount = request.DiscountAmount,
                ExpiryMonth = request.ExpiryMonth,
                ExpiryYear = request.ExpiryYear

            };
            return await _productRepo.AddAsync(prod);
        }
    }
}
