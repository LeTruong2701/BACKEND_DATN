using BE_DATN.Application.BUS.Home.DTO;
using BE_DATN.Application.BUS.Home.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI.Controllers.Home
{
    [Route("api/donhangkhachhang")]
    [ApiController]
    public class DonHangKhachHangController : ControllerBase
    {

        private readonly IDonHangKhachHang _manageDonHangKhachHang;
        public DonHangKhachHangController(IDonHangKhachHang manageDonHangKhachHang)
        {
            _manageDonHangKhachHang = manageDonHangKhachHang;
        }

        [Route("donhang")]
        [HttpPost]

        public async Task<IActionResult> SearchDonHang([FromBody] Dictionary<string, object> formData)
        {
            var result = await _manageDonHangKhachHang.SearchDonHangPaging(formData);
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }

        [Route("get-chitiet-by-id-donhang/{id}")]
        [HttpGet]
        public async Task<IActionResult> getChiTietByIdDonHang(int Id)
        {
            var result = await _manageDonHangKhachHang.GetChiTietDonHangByIdDonHang(Id);
            if (result == null)
            {
                return BadRequest("Không tìm thấy id đơn hàng");
            }
            return Ok(result);

        }

        [Route("get-donhangmomo")]
        [HttpPost]
        public async Task<IActionResult> GetDonHangMoMo([FromBody] DonHangMoMoModel us)
        {
            var result = await _manageDonHangKhachHang.GetDonHangMoMo(us);
            if (result == null)
            {
                return BadRequest("Lỗi");
            }
            return Ok(result);
        }



    }
}
