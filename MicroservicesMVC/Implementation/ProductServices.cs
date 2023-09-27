
using MicroservicesMVC.Interface;
using MicroservicesMVC.Models;
using MicroservicesMVC.Utility;

namespace MicroservicesMVC.Implementation
{
    public class ProductServices : IproductServices
    {
        private readonly IBaseServices _baseServices;
        public ProductServices(IBaseServices baseServices)
        {

            _baseServices = baseServices;

        }
        public async Task<ResponseDTO?> CreateProductbyIdAsync(ProductDto productdto)
        {
            return await _baseServices.SendAsync(new RequestDTO() { APIType = SD.APIType.POST, Url = SD.ProductAPIBase + "/api/Product" });
        }

        public async Task<ResponseDTO?> DeleteProductByIdAsync(int id)
        {
            return await _baseServices.SendAsync(new RequestDTO() { APIType = SD.APIType.DELETE, Url = SD.ProductAPIBase + "/api/Product/" + id });
        }

        public async Task<ResponseDTO?> GetAllProductAsync()
        {
            return await _baseServices.SendAsync(new RequestDTO() { APIType = SD.APIType.GET, Url = SD.ProductAPIBase + "/api/Product" });
        }

        public async Task<ResponseDTO?> GetProductByIdAsync(int id)
        {
            return await _baseServices.SendAsync(new RequestDTO() { APIType = SD.APIType.GET, Url = SD.ProductAPIBase + "/api/Product/"+ id });
        }

        public async Task<ResponseDTO?> UpdateProductByIdAsync(ProductDto productDto)
        {
            return await _baseServices.SendAsync(new RequestDTO() { APIType = SD.APIType.PUT, Url = SD.ProductAPIBase + "/api/Product"  });
        }
    }
}
