using MicroservicesMVC.Interface;
using MicroservicesMVC.Models;
using MicroservicesMVC.Utility;

namespace MicroservicesMVC.Implementation
{
    public class CouponServcies : ICouponService
    {
        private readonly IBaseServices _baseServices;
        public CouponServcies(IBaseServices baseServices)
        {

            _baseServices = baseServices;

        }
        public  async Task<ResponseDTO?> CreateCouponbyIdAsync(CouponDto coupondto)
        {
            return await _baseServices.SendAsync(new RequestDTO() { APIType = SD.APIType.POST, Data = coupondto, Url = SD.CouponAPIBase + "/api/coupon" });
        }

        public async Task<ResponseDTO?> DeleteCouponByIdAsync(int id)
        {
            return await _baseServices.SendAsync(new RequestDTO() { APIType = SD.APIType.DELETE, Url = SD.CouponAPIBase + "/api/coupon/" + id });
        }

        public async Task<ResponseDTO?> GetAllCouponAsync()
        {
            return await _baseServices.SendAsync(new RequestDTO() { APIType = SD.APIType.GET, Url = SD.CouponAPIBase + "/api/coupon" });
        }

        public async Task<ResponseDTO?> GetCouponByIdAsync(int id)
        {
            return await _baseServices.SendAsync(new RequestDTO() { APIType = SD.APIType.GET, Url = SD.CouponAPIBase + "/api/coupon/" + id });
        }

        public async Task<ResponseDTO?> UpdateCouponByIdAsync(CouponDto coupondto)
        {
            return await _baseServices.SendAsync(new RequestDTO() { APIType = SD.APIType.PUT, Url = SD.CouponAPIBase + "/api/coupon" });
        }
    }
}
