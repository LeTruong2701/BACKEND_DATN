using BE_DATN.Application.BUS.Home.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI.Controllers.Home
{
    [Route("api/shop")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShop _manageShop;
        public ShopController(IShop manageShop)
        {
            _manageShop = manageShop;
        }

        [Route("get-listthuonghieu")]
        [HttpGet]
        public async Task<IActionResult> get()
        {
            var listthuonghieu = await _manageShop.GetListThuongHieu();
            if (listthuonghieu == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(listthuonghieu);
        }

        [Route("search")]
        [HttpPost]
        public async Task<IActionResult> SearchSanPhamPaging([FromBody] Dictionary<string, object> formData)
        {
            var result =  _manageShop.SearchSanPhamPaging(formData);
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }

    }
}
