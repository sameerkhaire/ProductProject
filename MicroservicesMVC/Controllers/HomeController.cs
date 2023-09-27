
using MicroservicesMVC.Interface;
using MicroservicesMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace MicroservicesMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IproductServices _productservices;
        public HomeController(ILogger<HomeController> logger, IproductServices productservices)
        {
            _logger = logger;
            _productservices = productservices;
        }

        public async Task<IActionResult> Index()
        {

            List<ProductDto>? list = new();
            ResponseDTO responseDTO = await _productservices.GetAllProductAsync();
            if (responseDTO != null && responseDTO.IsSuccess!=null)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(responseDTO.Result));
            }
            else
            {
                TempData["error"] = responseDTO?.Messages;
            }

            return View(list);

        }
        public async Task<IActionResult> ProductDetails(int ProductId)
        {

            ProductDto? model = new();
            ResponseDTO responseDTO = await _productservices.GetProductByIdAsync(ProductId);
            if (responseDTO != null && responseDTO.IsSuccess != null)
            {
                model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDTO.Result));
            }
            else
            {
                TempData["error"] = responseDTO?.Messages;
            }

            return View(model);

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}