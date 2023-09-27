using AutoMapper;
using BAL;
using ShoppingCart.DTO_s;

namespace ShoppingCart
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            
            CreateMap<CartHeader, CartHeaderDto>();

            CreateMap<CartHeaderDto, CartHeader>();
            CreateMap<CartDetails, CartDetailsDto>();

            CreateMap<CartDetailsDto, CartDetails>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Coupon, CouponsDto>();
            CreateMap<CouponsDto, Coupon>();

        }
    }
}
