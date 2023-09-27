using BAL;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductWebApi.Data.Command;
using Repository.Interface;

namespace ProductWebApi.Data.Handler
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, int>
    {
        private readonly IBaseRepo<Product> _productRepo;
        public UpdateProductHandler(IBaseRepo<Product> productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var prod = await _productRepo.FindProductAsync(request.Id);
            if (prod != null) 
            {
                prod.ProductName = request.ProductName;
                prod.DiscountAmount = request.DiscountAmount;
                prod.ExpiryMonth = request.ExpiryMonth;
                prod.ExpiryYear = request.ExpiryYear;
                return await _productRepo.UpdateAsync(prod);
            }
            return default;
        }
    }
}
