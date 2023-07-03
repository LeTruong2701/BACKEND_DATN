using BE_DATN.Application.Authenticate;
using BE_DATN.Application.BUS.Admin.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController123 : ControllerBase
    {
        MD5 md = MD5.Create();
        private IManageUser1 _manageUser;

        public UserController123(IManageUser1 manageUser)
        {
            _manageUser = manageUser;

        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult authenticate([FromBody] AuthenticateModel model)
        {
            var user = _manageUser.Authenticate(model.username, model.password);

            if (user == null)
                return BadRequest(new { message = "Tài khoản hoặc mật khẩu sai!" });

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var users = await _manageUser.Get();
            if (users == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(users);
        }
    }
}
