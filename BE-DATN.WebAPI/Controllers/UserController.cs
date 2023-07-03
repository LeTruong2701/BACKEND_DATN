using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.BUS.Admin.Interfaces;
using BE_DATN.Data.Entities;
using BE_DATN.WebAPI.jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class UserController : ControllerBase
    {
        private readonly RoleManager<Roles> _roleManager;
        private readonly UserManager<Users> _userManager;

        private readonly IManageUser _manageUser;

        public UserController(UserManager<Users> userManager, RoleManager<Roles> roleManager, IManageUser manageUser)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _manageUser = manageUser;
        }

        [Route("initrole")]
        [HttpPost]
        public async Task<IActionResult> InitRole()
        {
            foreach (var role in SecurityRoles.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new Roles
                    {
                        Name = role,
                        NormalizedName = role.ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Description = $"Role for {role}"
                    });
                }
            }

            return Ok("Done");
        }
        //[HttpPost]
        //public async Task<IActionResult> InitRole()
        //{
        //    foreach (var role in SecurityRoles.Roles)
        //    {
        //        if (!await _roleManager.RoleExistsAsync(role))
        //        {
        //            var newRole = new AppRole
        //            {
        //                Name = role,
        //                NormalizedName = role.ToUpper(),
        //                ConcurrencyStamp = Guid.NewGuid().ToString(),
        //                Description = $"Role for {role}"
        //            };

        //            await _roleManager.AddAsync(newRole);
        //        }
        //    }

        //    return Ok("Done");
        //}

        [Route("get-khachhang-by-idkhachhang/{id}")]
        [HttpGet]
        public async Task<IActionResult> getbyid(int Id)
        {
            var result = await _manageUser.GetById(Id);
            if (result == null)
            {
                return BadRequest("Không tìm thấy");
            }
            return Ok(result);

        }



        [Route("create-user")]
        [HttpPost]
        public async Task<IActionResult> DangkyUser([FromBody] DangkyUserModel us)
        {
            var response = await _manageUser.DangkyUser(us);

            if (response is BadRequestObjectResult badRequest)
            {
                var errorMessage = "";

                if (badRequest.Value.ToString() == "Username đã tồn tại")
                {
                    errorMessage = "Username đã tồn tại";
                }
                else if (badRequest.Value.ToString() == "Email đã có người sử dụng")
                {
                    errorMessage = "Email đã có người sử dụng";
                }
                else if (badRequest.Value.ToString() == "Người dùng đã có tài khoản")
                {
                    errorMessage = "Người dùng đã có tài khoản";
                }

                return new BadRequestObjectResult(new { error = errorMessage });
            }

            return Ok(new { data = "OK" });


            //var response = await _manageUser.DangkyUser(us);

            //if (response is BadRequestObjectResult badRequest)
            //{
            //    return badRequest;
            //}

            //return Ok(new { data = "OK" });
        }

        [Route("search-user")]
        [HttpPost]

        public async Task<IActionResult> SearchUser([FromBody] Dictionary<string, object> formData)
        {
            var result = await _manageUser.SearchUserPaging(formData);
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }

        [Route("create-user1")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            var existUsername = await _userManager.FindByNameAsync(dto.UserName);

            if (existUsername != null) return new BadRequestObjectResult($"Username {dto.UserName} has already been taken");

            var appUser = UserHelper.ToApplicationUser(dto);

            var result1 = await _userManager.CreateAsync(appUser, dto.Password);

            if (!result1.Succeeded) return new BadRequestObjectResult(result1.Errors);

            List<string> roles = new List<string>();

            if (dto.IsAdmin) roles.AddRange(SecurityRoles.Roles.ToList());
            else roles.Add("User");

            var result2 = await _userManager.AddToRolesAsync(appUser, roles);

            if (!result2.Succeeded) return new BadRequestObjectResult(result2.Errors);

            var response = await _userManager.FindByNameAsync(dto.UserName);

            return Ok(response);
        }


        [Route("get-by-id-user/{id}")]
        [HttpGet]
        public async Task<IActionResult> getUserById(string Id)
        {
            var result = await _manageUser.GetUserByUserId(Id);
            if (result == null)
            {
                return BadRequest("Cannot find user");
            }
            return Ok(result);

        }

        [Route("update-password")]
        [HttpPut]
        public async Task<IActionResult> updatePassword([FromBody] ChangePasswordModel request)
        {
            var result = await _manageUser.ChangePassword(request);
            if (result is BadRequestObjectResult badRequest)
            {
                return BadRequest(badRequest.Value);
            }

            return Ok(new { data = "OK" });
            
        }



        [Route("update-user")]
        [HttpPut]
        public async Task<IActionResult> update([FromBody] DangkyUserModel request)
        {
            var response = await _manageUser.UpdateUser(request);

            if (response is BadRequestObjectResult badRequest)
            {
                var errorMessage = "";

                if (badRequest.Value.ToString() == "Không tìm thấy user")
                {
                    errorMessage = "Không tìm thấy user";
                }
               
                else if (badRequest.Value.ToString() == "Người dùng đã có tài khoản")
                {
                    errorMessage = "Người dùng đã có tài khoản";
                }

                return new BadRequestObjectResult(new { error = errorMessage });
            }

            return Ok(new { data = "OK" });
        }

        [Route("delete-user/{username}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var result = await _manageUser.DeleteUser(username);
            if (result.Succeeded)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest(new { errors = result.Errors });
        }







    }
    public static class UserHelper
    {
        public static Users ToApplicationUser(UserRegisterDto userDto)
        {
            Users applicationUser = new Users
            {
                UserName = userDto.UserName,
                Email = userDto.Email
                // Các thuộc tính khác của đối tượng AppUser
            };

            return applicationUser;
        }
    }
}
