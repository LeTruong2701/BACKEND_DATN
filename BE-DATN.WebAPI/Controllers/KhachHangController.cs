using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.BUS.Admin.Interfaces;
using BE_DATN.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class KhachHangController : ControllerBase
    {
        private readonly IManageKhachHang _manageKhachHang;
        public KhachHangController(IManageKhachHang manageKhachHang)
        {
            _manageKhachHang = manageKhachHang;
        }

        [Route("create-khachhang")]
        [HttpPost]
        public async Task<IActionResult> create([FromBody] KhachHangModel khachhang)
        {
            var result = await _manageKhachHang.Create(khachhang);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Create Failed");
        }

        [Route("search-khachhang")]
        [HttpPost]

        public async Task<IActionResult> SearchKhachHang([FromBody] Dictionary<string, object> formData)
        {
            var result = await _manageKhachHang.SearchKhachHangPaging(formData);
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }

        [Route("delete-khachhang/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteKhachHang(int Id)
        {
            var result = await _manageKhachHang.Delete(Id);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Delete Failed");
        }
    }
}
