using BE_DATN.Application.BUS.Admin.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class KhoController : ControllerBase
    {
        private readonly IManageKho _manageKho;
        public KhoController(IManageKho manageKho)
        {
            _manageKho = manageKho;
        }



        [Route("search-kho")]
        [HttpPost]

        public async Task<IActionResult> SearchKhuyenMai([FromBody] Dictionary<string, object> formData)
        {
            var result = await _manageKho.SearchKhoPaging(formData);
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }

    }
}
