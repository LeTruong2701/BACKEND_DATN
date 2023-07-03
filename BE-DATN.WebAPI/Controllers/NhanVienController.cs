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
    public class NhanVienController : ControllerBase
    {
        private readonly IManageNhanVien _manageNhanVien;
        public NhanVienController(IManageNhanVien manageNhanVien)
        {
            _manageNhanVien = manageNhanVien;
        }


        [Route("create-nhanvien")]
        [HttpPost]
        public async Task<IActionResult> create([FromBody] NhanVienModel nhanvien)
        {
            var result = await _manageNhanVien.Create(nhanvien);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Create Failed");
        }

        [Route("search-nhanvien")]
        [HttpPost]

        public async Task<IActionResult> SearchKhuyenMai([FromBody] Dictionary<string, object> formData)
        {
            var result = await _manageNhanVien.SearchNhanVienPaging(formData);
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }


        [Route("get-by-id-nhanvien/{id}")]
        [HttpGet]
        public async Task<IActionResult> getbyid(int Id)
        {
            var result = await _manageNhanVien.GetById(Id);
            if (result == null)
            {
                return BadRequest("Cannot find category");
            }
            return Ok(result);

        }

        [Route("update-nhanvien")]
        [HttpPut]
        public async Task<IActionResult> update([FromBody] NhanVienModel request)
        {
            var result = await _manageNhanVien.Update(request);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Update Failed");
        }

        [Route("delete-nhanvien/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteNhanvien(int Id)
        {
            var result = await _manageNhanVien.Delete(Id);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Delete Failed");
        }
    }
}
