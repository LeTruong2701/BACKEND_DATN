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
    public class DanhMucController : ControllerBase
    {
        private readonly IManageDanhMuc _manageDanhMuc;
        public DanhMucController(IManageDanhMuc manageDanhMuc)
        {
            _manageDanhMuc = manageDanhMuc;
        }

        [Route("search-danhmuc")]
        [HttpPost]

        public async Task<IActionResult> SearchDanhMuc([FromBody] Dictionary<string, object> formData)
        {
            var danhmucs = await _manageDanhMuc.SearchDanhMucPaging(formData);
            if (danhmucs == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(danhmucs);
        }

        [Route("get-by-id-danhmuc/{id}")]
        [HttpGet]
        public async Task<IActionResult> getbyid(int Id)
        {
            var danhmuc = await _manageDanhMuc.GetById(Id);
            if (danhmuc == null)
            {
                return BadRequest("Cannot find category");
            }
            return Ok(danhmuc);

        }


        [Route("get-danhmuclon")]
        [HttpGet]
        public async Task<IActionResult> get()
        {
            var listdanhmuclon = await _manageDanhMuc.GetDanhMucLon();
            if (listdanhmuclon == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(listdanhmuclon);
        }

        [Route("get-listdanhmucon")]
        [HttpGet]
        public async Task<IActionResult> getlistdanhmuccon()
        {
            var result = await _manageDanhMuc.GetListDanhMucCon();
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }

        [Route("get-danhmucnho/{id}")]
        [HttpGet]
        public async Task<IActionResult> getdanhmucnho(int Id)
        {
            var listdanhmuclon = await _manageDanhMuc.GetDanhMucNho(Id);
            if (listdanhmuclon == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(listdanhmuclon);
        }

        [Route("create-danhmuc")]
        [HttpPost]
        public async Task<IActionResult> create([FromBody] DanhMucModel danhmuc)
        {
            var danhmucs = await _manageDanhMuc.Create(danhmuc);
            if (danhmucs == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Create Failed");

        }

        [Route("update-danhmuc")]
        [HttpPut]
        public async Task<IActionResult> update([FromBody] DanhMucModel request)
        {
            var result = await _manageDanhMuc.Update(request);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Update  Failed");
        }




        [Route("delete-danhmuc/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDanhMuc(int Id)
        {
            var result = await _manageDanhMuc.Delete(Id);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Delete Failed");
        }
    }
}
