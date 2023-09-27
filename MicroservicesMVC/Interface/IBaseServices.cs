

using MicroservicesMVC.Models;

namespace MicroservicesMVC.Interface
{
    public interface IBaseServices
    {
        Task<ResponseDTO?> SendAsync(RequestDTO requestDTO);
    }
}
