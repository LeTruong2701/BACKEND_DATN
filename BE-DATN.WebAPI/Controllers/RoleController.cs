using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.BUS.Admin.Interfaces;
using BE_DATN.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<Roles> _roleManager;
        private readonly UserManager<Users> _userManager;

        private readonly IManageRole _manageRole;

        public RoleController(UserManager<Users> userManager, RoleManager<Roles> roleManager, IManageRole manageRole)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _manageRole = manageRole;
        }

        [Route("get-listrole")]
        [HttpGet]
        public async Task<IActionResult> get()
        {
            var listrole = await _manageRole.GetListRole();
            if (listrole == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(listrole);
        }

        [Route("create-role")]
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleModel r)
        {
            var result = await _manageRole.CreateRole(r);
            if (result != null)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Create Failed");
        }

        [Route("search-role")]
        [HttpPost]

        public async Task<IActionResult> SearchRole([FromBody] Dictionary<string, object> formData)
        {
            var result = await _manageRole.SearchRolePaging(formData);
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }

        [Route("get-by-id-role/{id}")]
        [HttpGet]
        public async Task<IActionResult> getRoleById(string Id)
        {
            var result = await _manageRole.GetRoleById(Id);
            if (result == null)
            {
                return BadRequest("Cannot find role");
            }
            return Ok(result);

        }

        [Route("update-role")]
        [HttpPut]
        public async Task<IActionResult> update([FromBody] RoleModel request)
        {
            var result = await _manageRole.UpdateRole(request);
            if (result != null)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Update Failed");
        }

        [Route("delete-role/{name}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string name)
        {
            var result = await _manageRole.DeleteRole(name);
            if (result.Succeeded)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest(new { errors = result.Errors });
        }

    }
}
