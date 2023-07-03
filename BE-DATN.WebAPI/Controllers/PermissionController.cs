using BE_DATN.WebAPI.jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_DATN.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        [Authorize(Roles = SecurityRoles.Admin)]
        [HttpGet]
        public IActionResult AdminRole()
        {
            return Ok("Hello Admin");
        }

        [Authorize(Roles = SecurityRoles.Manager)]
        [HttpGet]
        public IActionResult ManagerRole()
        {
            return Ok("Hello Manager");
        }

        [Authorize(Roles = SecurityRoles.User)]
        [HttpGet]
        public IActionResult UserRole()
        {
            return Ok("Hello User");
        }
    }
}
