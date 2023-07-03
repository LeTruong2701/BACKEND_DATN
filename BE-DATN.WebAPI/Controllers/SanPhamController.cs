using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.BUS.Admin.Interfaces;
using BE_DATN.Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class SanPhamController : ControllerBase
    {
        private readonly IManageSanPham _manageSanPham;
        public SanPhamController(IManageSanPham manageSanPham)
        {
            _manageSanPham = manageSanPham;
        }


        [Route("search-sanphamsample")]
        [HttpPost]
        public async Task<IActionResult> SearchSanPhamSample([FromBody] Dictionary<string, object> formData)
        {
            var result = await _manageSanPham.SearchSanPhamPaging(formData);
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }

        [Route("get-by-id-sanphamsample/{id}")]
        [HttpGet]
        public async Task<IActionResult> getbyidsanphamsample(int Id)
        {
            var result = await _manageSanPham.GetById(Id);
            if (result == null)
            {
                return BadRequest("Cannot find category");
            }
            return Ok(result);

        }

        [Route("create-sanphamsample")]
        [HttpPost]
        public async Task<IActionResult> createSanphamsample([FromBody] SanPhamModel sanpham)
        {
            var result = await _manageSanPham.Create(sanpham);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Create Failed");
        }

        [Route("update-sanphamsample")]
        [HttpPut]
        public async Task<IActionResult> updateSanphamsample([FromBody] SanPhamModel sanpham)
        {
            var result = await _manageSanPham.Update(sanpham);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Update Failed");
        }

        [Route("delete-sanphamsample/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteSanphamsample(int Id)
        {
            var result = await _manageSanPham.Delete(Id);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Delete Failed");
        }


        //Màu sản phẩm
        [Route("get-mausanpham-by-id-sanphamsample")]
        [HttpPost]
        public async Task<IActionResult> getMauSanPhamByIdSanphamsample([FromBody] Dictionary<string, object> formData)
        {
            var result = await _manageSanPham.GetListMauSanPhamById(formData);
            if (result == null)
            {
                return BadRequest("Cannot find category");
            }
            return Ok(result);

        }

        //lấy màu sản phẩm theo id màu sản phẩm
        [Route("get-mausanpham-by-id-mausanpham/{id}")]
        [HttpGet]
        public async Task<IActionResult> getMauSanPhamByIdMauSanPham(int Id)
        {
            var mausanpham = await _manageSanPham.GetMauSanPhamById(Id);
            if (mausanpham == null)
            {
                return BadRequest("Cannot find category");
            }
            return Ok(mausanpham);

        }

        [Route("create-mausanpham")]
        [HttpPost]
        public async Task<IActionResult> createMauSanPham([FromBody] MauSanPhamModel mausanpham)
        {
            var result = await _manageSanPham.CreateMauSanPham(mausanpham);
            if (result!=null)
            {
                return Ok(new { data = result });
            }

            return BadRequest("Create Failed");
        }

        [Route("update-mausanpham")]
        [HttpPut]
        public async Task<IActionResult> update([FromBody] MauSanPhamModel request)
        {
            var result = await _manageSanPham.UpdateMauSanPham(request);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Update Failed");
        }

        [Route("delete-mausanpham/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteMauSanPham(int Id)
        {
            var result = await _manageSanPham.DeleteMauSanPham(Id);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Delete Failed");
        }


        //size
        //lấy list size sản phẩm của 1 màu sản phẩm
        //Màu sản phẩm
        [Route("get-listsizesp-by-id-mausanpham")]
        [HttpPost]
        public async Task<IActionResult> getListSizeSanPhamByIdMauSanpham([FromBody] Dictionary<string, object> formData)
        {
            var result = await _manageSanPham.GetListSizeSanPhamByIdMauSanPham(formData);
            if (result == null)
            {
                return BadRequest("Cannot find category");
            }
            return Ok(result);

        }

    }
}
