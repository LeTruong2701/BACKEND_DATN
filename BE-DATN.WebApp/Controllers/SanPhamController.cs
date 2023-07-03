using BE_DATN.Application.BUS.Admin.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BE_DATN.WebApp.Controllers
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


        [Route("getall-sanpham")]
        [HttpGet]

        public async Task<IActionResult> get()
        {
            var sanphams = await _manageSanPham.Get();
            if (sanphams == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(sanphams);
        }
    }
}
