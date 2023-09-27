using MicroservicesMVC.Interface;
using MicroservicesMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MicroservicesMVC.Controllers
{

    public class ProductController : Controller
    {
        private readonly IproductServices _productservices;
        public ProductController(IproductServices productServices)
        {
            _productservices = productServices;
        }
        public async Task<IActionResult> Index()
        {

            List<ProductDto>? list = new();
            ResponseDTO responseDTO = await _productservices.GetAllProductAsync();
            if (responseDTO != null && responseDTO.IsSuccess != null)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(responseDTO.Result));
            }
            else
            {
                TempData["error"] = responseDTO?.Messages;
            }

            return View(list);

        }
    }
}
