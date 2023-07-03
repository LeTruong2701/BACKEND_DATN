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
    public class NguoiDungController : ControllerBase
    {
        private readonly IManageNguoiDung _manageNguoiDung;
        public NguoiDungController(IManageNguoiDung manageNguoiDung)
        {
            _manageNguoiDung = manageNguoiDung;
        }

        [Route("create-nguoidung")]
        [HttpPost]
        public async Task<IActionResult> create([FromBody] NguoiDungModel nguoidung)
        {
            var result = await _manageNguoiDung.Create(nguoidung);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Create Failed");
        }

        [Route("search-nguoidung")]
        [HttpPost]

        public async Task<IActionResult> SearchNguoiDung([FromBody] Dictionary<string, object> formData)
        {
            var result = await _manageNguoiDung.SearchNguoiDungPaging(formData);
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }

        [Route("get-listnguoidung")]
        [HttpGet]
        public async Task<IActionResult> get()
        {
            var listnguoidung = await _manageNguoiDung.GetListNguoiDung();
            if (listnguoidung == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(listnguoidung);
        }

        [Route("get-by-id-nguoidung/{id}")]
        [HttpGet]
        public async Task<IActionResult> getbyid(int Id)
        {
            var result = await _manageNguoiDung.GetById(Id);
            if (result == null)
            {
                return BadRequest("Cannot find category");
            }
            return Ok(result);

        }

        [Route("update-nguoidung")]
        [HttpPut]
        public async Task<IActionResult> update([FromBody] NguoiDungModel request)
        {
            var result = await _manageNguoiDung.Update(request);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Update Failed");
        }

        [Route("delete-nguoidung/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteNguoidung(int Id)
        {
            var result = await _manageNguoiDung.Delete(Id);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Delete Failed");
        }

    }
}
