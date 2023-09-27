using AutoMapper;
using BAL;
using DAL.Data;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using ShoppingCart.DTO_s;
using ShoppingCart.Iservices;
using ShoppingCart.Services;
using System.Data;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {


        private ResponseDto _responseDto;
        private readonly IMapper _mapper;
        private readonly ProductDbContext _db;
        private readonly IProductservices _productServices;
        public ShoppingCartController(IMapper mapper, ProductDbContext db, IProductservices productServices)
        {
            _mapper = mapper;
            this._responseDto = new ResponseDto();
            _db = db;
            _productServices = productServices;
        }
        [HttpPost("Cartupsert")]
        public async Task<ResponseDto> Cartupsert(CartDto cartDto)
        {
            try
            {
                var cartheaderdto = await _db.cartHeader.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == cartDto.CartHeader.UserId);
                if (cartheaderdto == null)
                {
                    CartHeader cartheader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _db.cartHeader.Add(cartheader);
                    await _db.SaveChangesAsync();

                    cartDto.CartDetails.First().CartHeaderId = cartheader.CartHeaderId;
                    _db.cartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                    await _db.SaveChangesAsync();

                }
                else
                {
                    var cartDetailsFromDb = await _db.cartDetails.AsNoTracking().FirstOrDefaultAsync(u => u.ProductId == cartDto.CartDetails.First().ProductId && u.CartHeaderId == cartheaderdto.CartHeaderId);
                    if (cartDetailsFromDb == null)
                    {
                        cartDto.CartDetails.First().CartHeaderId = cartheaderdto.CartHeaderId;
                        _db.cartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                       await  _db.SaveChangesAsync();
                    }
                    else
                    {
                        cartDto.CartDetails.First().Count += cartDetailsFromDb.Count;
                        cartDto.CartDetails.First().CartHeaderId = (int)cartDetailsFromDb.CartHeaderId;
                        cartDto.CartDetails.First().CartDetailsId = cartDetailsFromDb.CartDetailsId;
                        _db.cartDetails.Update(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        _db.SaveChangesAsync();
                    }
                }
                _responseDto.Result = cartDto;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message.ToString();
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }

        [HttpPost("RemoveCart")]
        public async Task<ResponseDto> RemoveCart([FromBody]int cartdetailsid)
        {
            try
            {
                CartDetails cartDetails= _db.cartDetails.Where(u=>u.CartDetailsId==cartdetailsid).FirstOrDefault();
                int totalcountcartitem = _db.cartDetails.Where(x => x.CartHeaderId == cartDetails.CartHeaderId).Count();
                _db.cartDetails.Remove(cartDetails);
                if(totalcountcartitem ==1)
                {
                    var cartHeaderRemove = await _db.cartHeader.FirstOrDefaultAsync(x =>x.CartHeaderId==cartDetails.CartHeaderId);
                    _db.cartHeader.Remove(cartHeaderRemove);
                }
                await _db.SaveChangesAsync();
                _responseDto.Result = true;
            }
            catch (Exception ex) 
            {
                _responseDto.Message=ex.Message.ToString();
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }
        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseDto> GetCart(int userId)
        {
            try
            {
                CartDto cartDto = new CartDto()
                {
                    CartHeader = _mapper.Map<CartHeaderDto>(_db.cartHeader.First(x => x.UserId == userId))
                };
                cartDto.CartDetails=_mapper.Map<IEnumerable<CartDetailsDto>>(_db.cartDetails.Where(x=>x.CartHeaderId==cartDto.CartHeader.CartHeaderId));
                List<ProductDto> productdto = await _productServices.GetProducts();
                foreach(var item in  cartDto.CartDetails)
                {
                    item.Product = productdto.FirstOrDefault(x => x.ProductId == item.ProductId);
                    cartDto.CartHeader.CartTotal += (item.Count * item.Product.DiscountAmount);
                }
                if (!string.IsNullOrEmpty(cartDto.CartHeader.CouponCode))
                {
                    CouponsDto coupons = await _productServices.GetCouponsByCode(cartDto.CartHeader.CouponCode);
                    if(coupons!=null && cartDto.CartHeader.CartTotal> 1)
                    {
                        cartDto.CartHeader.CartTotal -= coupons.CouponAmount;
                        cartDto.CartHeader.discount = coupons.CouponAmount;
                    }
                }
                _responseDto.Result = cartDto;

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess=false;
                _responseDto.Message= ex.Message.ToString();
            }
            return _responseDto;
        }
    }
}
