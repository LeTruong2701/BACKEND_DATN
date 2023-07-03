using BE_DATN.Application.BUS.Home.DTO;
using BE_DATN.Application.BUS.Home.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI.Controllers.Home
{
    [Route("api/userkhachhang")]
    [ApiController]
    public class UserKhachHangController : ControllerBase
    {
        private readonly IUserManage _manageUser;
        public UserKhachHangController(IUserManage userManage)
        {
            _manageUser = userManage;
        }

        [Route("create-userkhachhang")]
        [HttpPost]
        public async Task<IActionResult> DangkyUser([FromBody] DangkyModel us)
        {
            var result = await _manageUser.DangkyUserKhachHang(us);
            if (result != null)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Create Failed");
        }


        [Route("get-by-id-khachhang/{id}")]
        [HttpGet]
        public async Task<IActionResult> getbyid(int Id)
        {
            var result = await _manageUser.GetById(Id);
            if (result == null)
            {
                return BadRequest("Không tìm thấy khách hàng");
            }
            return Ok(result);

        }

        [Route("update-khachhang")]
        [HttpPut]
        public async Task<IActionResult> update([FromBody] KhachhangModel request)
        {
            var result = await _manageUser.Update(request);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Update Failed");
        }


        [Route("update-password")]
        [HttpPut]
        public async Task<IActionResult> updatePassword([FromBody] DoiMatKhauModel request)
        {
            var result = await _manageUser.ChangePassword(request);
            if (result is BadRequestObjectResult badRequest)
            {
                return BadRequest(badRequest.Value);
            }

            return Ok(new { data = "OK" });

        }
    }
}
