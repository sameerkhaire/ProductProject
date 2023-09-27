using BAL;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductWebApi.Data;
using ProductWebApi.Data.Command;
using ProductWebApi.DTO_S;

namespace ProductWebApi.Controllers
{
    [Route("api/Product")]
    [EnableCors("AllowAllHeaders")]

    [ApiController]

    
    //[Authorize("clientIdpolicy")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _Imediator;
        private readonly ResponseDto _respoDto;
        public ProductController(IMediator mediator)
        {
            _Imediator = mediator;
            _respoDto = new ResponseDto();
        }
        [HttpGet]
        public async Task<ResponseDto> ProductList()
        {
            var prod = await _Imediator.Send(new GetProductListQuery());
            _respoDto.Result = prod;
            return _respoDto;
        }
        [HttpGet("{id}")]
        public async Task<ResponseDto> GetProduct(int id)
        {
            var prod = await _Imediator.Send(new GetProductByIdQuery() { Id = id });
            _respoDto.Result= prod;
            return _respoDto;
        }
        [HttpPost]
        public async Task<ResponseDto> AddProduct([FromBody]Product product)
        {
            var prods=await _Imediator.Send(new CreateProductCommand(product.ProductName,product.DiscountAmount,product.ExpiryMonth,product.ExpiryYear));
            _respoDto.Result= prods;
            return _respoDto;
        }
        [HttpPut]
        public async Task<ResponseDto> UpdateProduct([FromBody]Product product)
        {
          
                var prods = await _Imediator.Send(new UpdateProductCommand(product.ProductId, product.ProductName, product.DiscountAmount, product.ExpiryMonth, product.ExpiryYear));
            _respoDto.Result = prods;
            return _respoDto;

        }
        [HttpDelete("{id}",Name ="DELETE")]
        public async Task<ResponseDto> DeleteProduct(int id)
        {
            var prod = await _Imediator.Send(new DeleteProductCommand() { Id = id });
            _respoDto.Result = prod;
            return _respoDto;
        }
       
         





    }
}
