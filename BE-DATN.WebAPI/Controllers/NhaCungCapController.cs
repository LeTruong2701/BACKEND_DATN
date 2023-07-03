using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.BUS.Admin.Interfaces;
using BE_DATN.Data.Entities;
using BE_DATN.WebAPI.jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class NhaCungCapController : ControllerBase
    {
        private readonly IManageNhaCungCap _manageNhaCungCap;
        public NhaCungCapController(IManageNhaCungCap manageNhaCungCap)
        {
            _manageNhaCungCap = manageNhaCungCap;
        }

        [Route("get-listnhacungcap")]
        [HttpGet]
        public async Task<IActionResult> getlistnhacungcap()
        {
            var listncc = await _manageNhaCungCap.GetNhaCungCap();
            if (listncc == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(listncc);
        }

        
        [Route("create-nhacungcap")]
        [HttpPost]
        public async Task<IActionResult> create([FromBody] NhaCungCapModel nhacungcap)
        {
            var result = await _manageNhaCungCap.Create(nhacungcap);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Create Failed");
        }

        [Route("search-nhacungcap")]
        [HttpPost]

        public async Task<IActionResult> SearchNhaCungCap([FromBody] Dictionary<string, object> formData)
        {
            var danhmucs = await _manageNhaCungCap.SearchNhaCungCapPaging(formData);
            if (danhmucs == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(danhmucs);
        }


        [Route("get-by-id-nhacungcap/{id}")]
        [HttpGet]
        public async Task<IActionResult> getbyid(int Id)
        {
            var nhacungcap = await _manageNhaCungCap.GetById(Id);
            if (nhacungcap == null)
            {
                return BadRequest("Cannot find category");
            }
            return Ok(nhacungcap);

        }


        
        [Route("update-nhacungcap")]
        [HttpPut]
        public async Task<IActionResult> update([FromBody] NhaCungCapModel request)
        {
            var result = await _manageNhaCungCap.Update(request);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Update Failed");
        }

        [Route("delete-nhacungcap/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteNhacungcap(int Id)
        {
            var result = await _manageNhaCungCap.Delete(Id);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Delete Failed");
        }

    }
}
