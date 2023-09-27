using BAL;
using CouponApi.Data;
using CouponApi.Data.Command;
using CouponApi.DTO_s;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using static Azure.Core.HttpHeader;

namespace CouponApi.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly IMediator _Imediator;
        private readonly IcouponServices _icouponservices;
        private readonly ResponseDto _responseDto;
        
        public CouponController(IMediator mediator, IcouponServices icouponservices)
        {
            _Imediator = mediator;
            _icouponservices = icouponservices;
            _responseDto = new ResponseDto();
        }
        [HttpGet]
        public async Task<ResponseDto> CouponList()
        {
            var prod = await _Imediator.Send(new GetCouponListQuery());
            _responseDto.Result=prod;
            return _responseDto;
        }
        [HttpGet("{id}")]
        public async Task<ResponseDto> GetCoupon(int id)
        {
            var coupons = await _Imediator.Send(new GetCouponByIdQuery() { Id = id });
            _responseDto.Result = coupons;
            return _responseDto;
        }
        [HttpGet("GetCuponCode/{couponcode}")]
        public IActionResult GetCuponCode(string couponcode)
        {
            var data= _icouponservices.GetCouponCode(couponcode);
            _responseDto.Result = data;
            return Ok(_responseDto);
        }
        [HttpPost]
        public async Task<ResponseDto> AddCoupon([FromBody] Coupon coupons)
        {
            var coupon = await _Imediator.Send(new CreateCouponCommand(coupons.CouponName, coupons.CouponDescription, coupons.CouponAmount));
           _responseDto.Result = coupon;
            return _responseDto;
        }
        [HttpPut]
        public async Task<int> UpdateCoupon([FromBody] Coupon coupons)
        {

            var coupon = await _Imediator.Send(new UpdateCouponCommand(coupons.CouponId, coupons.CouponName,coupons.CouponDescription, coupons.CouponAmount));
            return coupon;

        }
        [HttpDelete("{id}", Name = "DELETE")]
        public async Task<ResponseDto> DeleteCoupon(int id)
        {
            var coupon = await _Imediator.Send(new DeleteCouponCommand() { Id = id });
            _responseDto.Result = coupon;
            return _responseDto;
        }
    }
}
