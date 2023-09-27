using MicroservicesMVC.Interface;
using MicroservicesMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MicroservicesMVC.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        public async Task<IActionResult> Index()
        {

            List<CouponDto>? list = new();
            ResponseDTO? responseDTO = await _couponService.GetAllCouponAsync();
            if (responseDTO != null && responseDTO.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(responseDTO.Result));
            }
            else
            {
                TempData["error"] = responseDTO?.Messages;
            }

            return View(list);
        }
        public async Task<IActionResult> CreateCupon()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCupon(CouponDto couponDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDTO? respo = await _couponService.CreateCouponbyIdAsync(couponDto);
                if (respo != null && respo.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = respo?.Messages;
                }
            }

            return View(couponDto);
        }

        public async Task<IActionResult> DeleteCupon(int CouponId)
        {
            ResponseDTO? responseDTO = await _couponService.GetCouponByIdAsync(CouponId);
            if (responseDTO != null && responseDTO.IsSuccess)
            {
                CouponDto? couponDto = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDTO.Result));
                return View(couponDto);
            }
            else
            {
                TempData["error"] = responseDTO?.Messages;
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCupon(CouponDto couponDto)
        {
            ResponseDTO? responseDTO = await _couponService.DeleteCouponByIdAsync(couponDto.CouponId);
            if (responseDTO != null && responseDTO.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = responseDTO?.Messages;
            }
            return View(couponDto);
        }
    }
}
