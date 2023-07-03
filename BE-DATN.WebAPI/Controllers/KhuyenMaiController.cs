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
    public class KhuyenMaiController : ControllerBase
    {
        private readonly IManageKhuyenMai _manageKhuyenMai;
        public KhuyenMaiController(IManageKhuyenMai manageKhuyenMai)
        {
            _manageKhuyenMai = manageKhuyenMai;
        }

        [Route("create-khuyenmai")]
        [HttpPost]
        public async Task<IActionResult> create([FromBody] KhuyenMaiModel khuyenmai)
        {
            var result = await _manageKhuyenMai.Create(khuyenmai);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Create Failed");
        }

        [Route("search-khuyenmai")]
        [HttpPost]

        public async Task<IActionResult> SearchKhuyenMai([FromBody] Dictionary<string, object> formData)
        {
            var result = await _manageKhuyenMai.SearchKhuyenMaiPaging(formData);
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }

        [Route("get-khuyenmai-hople")]
        [HttpGet]
        public async Task<IActionResult> getkhuyenmaihople()
        {
            var result = await _manageKhuyenMai.GetKhuyenMaiTrangNguoiDung();
            if (result == null)
            {
                return BadRequest("Failed");
            }
            return Ok(result);

        }


        [Route("get-by-id-khuyenmai/{id}")]
        [HttpGet]
        public async Task<IActionResult> getbyid(int Id)
        {
            var result = await _manageKhuyenMai.GetById(Id);
            if (result == null)
            {
                return BadRequest("Cannot find category");
            }
            return Ok(result);

        }

        [Route("update-khuyenmai")]
        [HttpPut]
        public async Task<IActionResult> update([FromBody] KhuyenMaiModel request)
        {
            var result = await _manageKhuyenMai.Update(request);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Update Failed");
        }

        [Route("delete-khuyenmai/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteNhacungcap(int Id)
        {
            var result = await _manageKhuyenMai.Delete(Id);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Delete Failed");
        }
    }
}
