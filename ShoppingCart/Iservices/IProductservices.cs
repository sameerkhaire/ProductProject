using ShoppingCart.DTO_s;

namespace ShoppingCart.Iservices
{
    public interface IProductservices
    {
        Task<List<ProductDto>> GetProducts();
        Task<CouponsDto> GetCouponsByCode(string couponcode);
    }
}
