using MicroservicesMVC.Models;

namespace MicroservicesMVC.Interface
{
    public interface ICouponService
    {
        Task<ResponseDTO?> GetAllCouponAsync();
        Task<ResponseDTO?> GetCouponByIdAsync(int id);
        Task<ResponseDTO?> CreateCouponbyIdAsync(CouponDto coupondto);
        Task<ResponseDTO?> UpdateCouponByIdAsync(CouponDto coupondto);
        Task<ResponseDTO?> DeleteCouponByIdAsync(int id);
    }
}
