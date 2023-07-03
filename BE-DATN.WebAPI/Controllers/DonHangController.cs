using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.BUS.Admin.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class DonHangController : ControllerBase
    {
        private readonly IManageDonHang _manageDonhang;
        public DonHangController(IManageDonHang manageDonhang)
        {
            _manageDonhang = manageDonhang;
        }


        [Route("update-donhang")]
        [HttpPut]
        public async Task<IActionResult> update([FromBody] DonHangDTO request)
        {
            var result = await _manageDonhang.Update(request);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Update Failed");
        }



        [Route("search-donhang")]
        [HttpPost]

        public async Task<IActionResult> SearchDonHang([FromBody] Dictionary<string, object> formData)
        {
            var result = await _manageDonhang.SearchDonHangPaging(formData);
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }

        [Route("search-donhangtheongay")]
        [HttpPost]

        public async Task<IActionResult> SearchDonHangTheoNgay([FromBody] Dictionary<string, object> formData)
        {
            var result = await _manageDonhang.SearchDonHangPagingTheoNgay(formData);
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }


        [Route("get-by-id-donhang/{id}")]
        [HttpGet]
        public async Task<IActionResult> getdonhangbyid(int Id)
        {
            var result = await _manageDonhang.GetById(Id);
            if (result == null)
            {
                return BadRequest("Không tìm thấy đơn hàng");
            }
            return Ok(result);

        }


        [Route("get-chitiet-by-id-donhang/{id}")]
        [HttpGet]
        public async Task<IActionResult> getChiTietByIdDonHang(int Id)
        {
            var result = await _manageDonhang.GetChiTietDonHangByIdDonHang(Id);
            if (result == null)
            {
                return BadRequest("Không tìm thấy id đơn hàng");
            }
            return Ok(result);

        }


        [Route("delete-hoadon/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteHoaDon(int Id)
        {
            var result = await _manageDonhang.Delete(Id);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Delete Failed");
        }








    }
}
