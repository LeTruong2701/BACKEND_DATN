using BE_DATN.Application.BUS.Home.DTO;
using BE_DATN.Application.BUS.Home.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI.Controllers.Home
{
    [Route("api/checkout")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckout _manageCheckout;
        public CheckoutController(ICheckout manageCheckout)
        {
            _manageCheckout = manageCheckout;
        }


        [Route("get-khachhang-by-id/{id}")]
        [HttpGet]
        public async Task<IActionResult> getbyid(int Id)
        {
            var result = await _manageCheckout.GetKhachHangById(Id);
            if (result == null)
            {
                return BadRequest("Không tìm thấy khách hàng");
            }
            return Ok(result);

        }

        [Route("get-khuyenmai-by-makhuyenmai/{makhuyenmai}")]
        [HttpGet]
        public async Task<IActionResult> getKhuyenMaiByMaKhuyenMai(string makhuyenmai)
        {
            var result = await _manageCheckout.GetKhuyenMaiByMaKhuyenMai(makhuyenmai);
            if (result == null)
            {
                return BadRequest("Không tìm thấy mã khuyến mãi");
            }
            return Ok(result);

        }

        [Route("create-donhang")]
        [HttpPost]
        public async Task<IActionResult> DangkyUser([FromBody] DonHangModel us)
        {
            var result = await _manageCheckout.CreateDonHang(us);
            if (result != null)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Create Failed");
        }


        [Route("create-donhangmomo")]
        [HttpPost]
        public async Task<IActionResult> Createdonhangmomo([FromBody] DonHangModel us)
        {
            var result = await _manageCheckout.CreateDonHangVoiViMoMo(us);
            if (result != null)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Create Failed");
        }


    }
}
