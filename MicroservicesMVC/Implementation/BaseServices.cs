
using MicroservicesMVC.Interface;
using Newtonsoft.Json;
using static MicroservicesMVC.Utility.SD;
using System.Text;
using MicroservicesMVC.Models;

namespace MicroservicesMVC.Implementation
{
    public class BaseServices : IBaseServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ResponseDTO?> SendAsync(RequestDTO requestDTO)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(requestDTO.Url);
                if (requestDTO.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
                }
                HttpResponseMessage? apiRespo = null;
                switch (requestDTO.APIType)
                {
                    case APIType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case APIType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case APIType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                apiRespo = await client.SendAsync(message);
                switch (apiRespo.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Messages = "Not Found" };
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Messages = "Unauthorized" };
                    case System.Net.HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Messages = "Access Denied" };
                    case System.Net.HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Messages = "Internal Server Error" };
                    default:
                        var apicon = await apiRespo.Content.ReadAsStringAsync();
                        var apirespo = JsonConvert.DeserializeObject<ResponseDTO>(apicon);
                        return apirespo;
                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDTO()
                {
                    Messages = ex.Message,
                    IsSuccess = false,
                };
                return dto;
            }
        }
    }
}
