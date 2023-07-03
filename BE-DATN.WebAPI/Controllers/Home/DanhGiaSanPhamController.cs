using BE_DATN.Application.BUS.Home.DTO;
using BE_DATN.Application.BUS.Home.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI.Controllers.Home
{
    [Route("api/danhgiasanpham")]
    [ApiController]
    public class DanhGiaSanPhamController : ControllerBase
    {
        private readonly IDanhGiaSanPham _manageDanhGiaSanPham;
        public DanhGiaSanPhamController(IDanhGiaSanPham manageDanhGiaSanPham)
        {
            _manageDanhGiaSanPham = manageDanhGiaSanPham;
        }


        [Route("get-list-idsp-khachhang-da-mua/{id}")]
        [HttpGet]
        public async Task<IActionResult> getQuanTheThao(int id)
        {
            var result = await _manageDanhGiaSanPham.GetListSPKhachHangDaMua(id);
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }

        [Route("get-list-danh-gia-sanpham/{id}")]
        [HttpGet]
        public async Task<IActionResult> getListDanhGiaSanPham(int id)
        {
            var result = await _manageDanhGiaSanPham.GetListDanhGiaSanPham(id);
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }

        [Route("get-by-id-danhgia/{id}")]
        [HttpGet]
        public async Task<IActionResult> getbyid(int Id)
        {
            var result = await _manageDanhGiaSanPham.GetById(Id);
            if (result == null)
            {
                return BadRequest("Không tìm thấy");
            }
            return Ok(result);

        }


        [Route("create-danhgia")]
        [HttpPost]
        public async Task<IActionResult> create([FromBody] DanhGiaSanPhamModel dgsp)
        {
            var result = await _manageDanhGiaSanPham.Create(dgsp);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Create Failed");
        }

        [Route("update-danhgia")]
        [HttpPut]
        public async Task<IActionResult> update([FromBody] DanhGiaSanPhamModel request)
        {
            var result = await _manageDanhGiaSanPham.Update(request);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Update Failed");
        }


        [Route("delete-danhgia/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDanhGia(int Id)
        {
            var result = await _manageDanhGiaSanPham.Delete(Id);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Delete Failed");
        }
    }
}
