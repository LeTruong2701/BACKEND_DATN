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
    public class ThuongHieuController : ControllerBase
    {
        private readonly IManageThuongHieu _manageThuongHieu;
        public ThuongHieuController(IManageThuongHieu manageThuongHieu)
        {
            _manageThuongHieu = manageThuongHieu;
        }

        [Route("create-thuonghieu")]
        [HttpPost]
        public async Task<IActionResult> create([FromBody] ThuongHieuModel thuonghieu)
        {
            var result = await _manageThuongHieu.Create(thuonghieu);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Create Failed");
        }

        [Route("search-thuonghieu")]
        [HttpPost]

        public async Task<IActionResult> SearchThuongHieu([FromBody] Dictionary<string, object> formData)
        {
            var result = await _manageThuongHieu.SearchThuongHieuPaging(formData);
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }


        [Route("get-listthuonghieu")]
        [HttpGet]
        public async Task<IActionResult> get()
        {
            var listthuonghieu = await _manageThuongHieu.GetListThuongHieu();
            if (listthuonghieu == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(listthuonghieu);
        }

        [Route("get-by-id-thuonghieu/{id}")]
        [HttpGet]
        public async Task<IActionResult> getbyid(int Id)
        {
            var result = await _manageThuongHieu.GetById(Id);
            if (result == null)
            {
                return BadRequest("Cannot find category");
            }
            return Ok(result);

        }

        [Route("update-thuonghieu")]
        [HttpPut]
        public async Task<IActionResult> update([FromBody] ThuongHieuModel request)
        {
            var result = await _manageThuongHieu.Update(request);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Update Failed");
        }

        [Route("delete-thuonghieu/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteThuongHieu(int Id)
        {
            var result = await _manageThuongHieu.Delete(Id);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Delete Failed");
        }
    }
}
