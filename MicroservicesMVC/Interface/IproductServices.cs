
using MicroservicesMVC.Models;

namespace MicroservicesMVC.Interface
{
    public interface IproductServices
    {
        Task<ResponseDTO?> GetAllProductAsync();
        Task<ResponseDTO?> GetProductByIdAsync(int id);
        Task<ResponseDTO?> CreateProductbyIdAsync(ProductDto productdto);
        Task<ResponseDTO?> UpdateProductByIdAsync(ProductDto productDto);
        Task<ResponseDTO?> DeleteProductByIdAsync(int id);

    }
}
