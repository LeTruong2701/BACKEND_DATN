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
    public class HoaDonNhapController : ControllerBase
    {

        private readonly IManageHoaDonNhap _manageHoaDonNhap;
        public HoaDonNhapController(IManageHoaDonNhap manageHoaDonNhap)
        {
            _manageHoaDonNhap = manageHoaDonNhap;
        }


        [Route("search-hoadonnhap")]
        [HttpPost]
        public async Task<IActionResult> SearchHoaDonNhap([FromBody] Dictionary<string, object> formData)
        {
            var result = await _manageHoaDonNhap.SearchHoaDonNhapPaging(formData);
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }
        [Route("get-by-id-hoadonnhap/{id}")]
        [HttpGet]
        public async Task<IActionResult> getbyidhoadonnhap(int Id)
        {
            var result = await _manageHoaDonNhap.GetById(Id);
            if (result == null)
            {
                return BadRequest("Cannot find category");
            }
            return Ok(result);

        }

        [Route("get-chitiethoadonnhap-by-id-hoadonnhap/{id}")]
        [HttpGet]
        public async Task<IActionResult> getchitiethoadonnhapbyidhoadonnhap(int Id)
        {
            var result = await _manageHoaDonNhap.GetChiTietHoaDonNhap(Id);
            if (result == null)
            {
                return BadRequest("Cannot find category");
            }
            return Ok(result);

        }


        [Route("create-hoadonnhap")]
        [HttpPost]
        public async Task<IActionResult> create([FromBody] DataNhapHoaDonNhap data)
        {
            var result = await _manageHoaDonNhap.CreateHoaDonNhap(data);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Create Failed");
        }

        [Route("update-hoadonnhap")]
        [HttpPut]
        public async Task<IActionResult> updateHoaDonNhap([FromBody] HoaDonNhapModel hdn)
        {
            var result = await _manageHoaDonNhap.Update(hdn);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Update Failed");
        }

        [Route("get-listsanphamchuathanhtoan/{id}")]
        [HttpGet]
        public async Task<IActionResult> getbyid(int Id)
        {
            var listsp = await _manageHoaDonNhap.GetListMauSanPhamChuaThanhToan(Id);
            if (listsp == null)
            {
                return BadRequest("Cannot find category");
            }
            return Ok(listsp);

        }

        //[Route("get-listsanphamnhap/{id}")]
        //[HttpGet]
        //public async Task<IActionResult> getlistsanphamnhap(int Id)
        //{
        //    var listsp = await _manageHoaDonNhap.GetListSanPhamNhap(Id);
        //    if (listsp == null)
        //    {
        //        return BadRequest("Cannot find category");
        //    }
        //    return Ok(listsp);

        //}

    }
}
