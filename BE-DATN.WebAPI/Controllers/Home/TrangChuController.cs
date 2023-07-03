using BE_DATN.Application.BUS.Home.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI.Controllers.Home
{
    [ApiController]
    [Route("api/home")]
    public class TrangChuController : ControllerBase
    {
        private readonly ITrangChu _manageTrangChu;
        public TrangChuController(ITrangChu manageTrangChu)
        {
            _manageTrangChu = manageTrangChu;
        }

        [Route("get-sanphammoi")]
        [HttpGet]
        public async Task<IActionResult> getSanPhamMoi()
        {
            var listspmoi = await _manageTrangChu.GetSanPhamMoi();
            if (listspmoi == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(listspmoi);
        }

        [Route("get-aothethao")]
        [HttpGet]
        public async Task<IActionResult> getAoTheThao()
        {
            var listaothethao = await _manageTrangChu.GetAoTheThao();
            if (listaothethao == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(listaothethao);
        }

        [Route("get-quanthethao")]
        [HttpGet]
        public async Task<IActionResult> getQuanTheThao()
        {
            var listquanthethao = await _manageTrangChu.GetQuanTheThao();
            if (listquanthethao == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(listquanthethao);
        }

        [Route("get-phukienthethao")]
        [HttpGet]
        public async Task<IActionResult> getPhuKienTheThao()
        {
            var listphukienthethao = await _manageTrangChu.GetPhuKienTheThao();
            if (listphukienthethao == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(listphukienthethao);
        }

        [Route("get-danhmuc")]
        [HttpGet]
        public IActionResult getDanhMuc()
        {
            var danhmuc = _manageTrangChu.GetData();
            if (danhmuc == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(danhmuc);
        }

    }
}
