using Newtonsoft.Json;
using ShoppingCart.DTO_s;
using ShoppingCart.Iservices;

namespace ShoppingCart.Services
{
    public class ProductServices : IProductservices
    {
        public readonly IHttpClientFactory _httpClientFactory;
        public ProductServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CouponsDto> GetCouponsByCode(string couponcode)
        {
            var client = _httpClientFactory.CreateClient("couponss");
            var response = await client.GetAsync($"/api/coupon/GetCuponCode/{couponcode}");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponsDto>(Convert.ToString(resp.Result));
            }
            return new CouponsDto();
        }

        public async Task<List<ProductDto>> GetProducts()
        {

            var client = _httpClientFactory.CreateClient("products");
            var response = await client.GetAsync($"/api/Product");
            var apiContent=await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(resp.Result));
            }
            return new List<ProductDto>();
        }
    }
}
