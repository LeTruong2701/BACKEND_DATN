using BE_DATN.Application.Authenticate;
using BE_DATN.Data.EF;
using BE_DATN.Data.Entities;
using BE_DATN.WebAPI.jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace BE_DATN.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;
        private readonly BEDATNDbContext _context;

        public AuthenticationController(UserManager<Users> userManager, IConfiguration configuration, BEDATNDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<UserViewModel>> Login(UserLoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            var checkPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (checkPassword == false)
                return BadRequest(new { message = "Tài khoản hoặc mật khẩu sai!" });

            var result = from ac in _context.User1s
                         select new UserViewModel
                         {
                             Email = ac.Email,

                             UserName = ac.UserName,
                             PassWord = ac.PasswordHash,
                             IdNguoiDung=ac.IdNguoiDung,
                             IdKhachHang=ac.IdKhachHang,
                             TrangThai=ac.TrangThai
                             

                         };

            var user1 = result.SingleOrDefault(x => x.UserName == dto.UserName && checkPassword == true);



            //if (user == null) return new BadRequestObjectResult("Username or Password incorrect");



            //if (!checkPassword) return new BadRequestObjectResult("Username or Password incorrect");

            IList<string>? userRoles = await _userManager.GetRolesAsync(user);

            var token = TokenHelper.GenerateToken(
                _configuration["JWT:Secret"]
                , _configuration["JWT:ValidIssuer"]
                , _configuration["JWT:ValidAudience"]
                , userRoles
                , user.Id.ToString()
                , user.UserName
                , user.Email);

            user1.Token = token;
            user1.Roles= userRoles.ToList();

            return user1;
           
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(UserLoginDto dto)
        //{
        //    var user = await _userManager.FindByNameAsync(dto.UserName);

        //    if (user == null) return new BadRequestObjectResult("Username or Password incorrect");

        //    var checkPassword = await _userManager.CheckPasswordAsync(user, dto.Password);

        //    if (!checkPassword) return new BadRequestObjectResult("Username or Password incorrect");

        //    IList<string>? userRoles = await _userManager.GetRolesAsync(user);

        //    var token = TokenHelper.GenerateToken(
        //        _configuration["JWT:Secret"]
        //        , _configuration["JWT:ValidIssuer"]
        //        , _configuration["JWT:ValidAudience"]
        //        , userRoles
        //        , user.Id.ToString()
        //        , user.UserName
        //        , user.Email);

        //    return Ok(new
        //    {
        //        Token = token,
        //        ValidTo = TokenHelper.GetValidTo(token)
        //    });
        //}
    }
}
