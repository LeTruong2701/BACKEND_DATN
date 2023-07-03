using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.BUS.Admin.Interfaces;
using BE_DATN.Data.EF;
using BE_DATN.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using BE_DATN.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace BE_DATN.Application.BUS.Admin
{
    public class ManageUser : IManageUser
    {
        private readonly UserManager<Users> _userManager;
        private readonly BEDATNDbContext _context;

        //private readonly RoleManager<Roles> _roleManager;
        //private readonly UserManager<Users> _userManager;

        public ManageUser(UserManager<Users> userManager, BEDATNDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<ActionResult> ChangePassword(ChangePasswordModel dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null) return new BadRequestObjectResult($"User {dto.UserName} not found");

            if (!await _userManager.CheckPasswordAsync(user, dto.CurentPassword))
                return new BadRequestObjectResult("Mật khẩu cũ không chính xác!");


            var result = await _userManager.ChangePasswordAsync(user, dto.CurentPassword, dto.NewPassword);

            if (!result.Succeeded) return new BadRequestObjectResult(result.Errors);

            return new OkObjectResult("Password changed successfully");
        }

        public async Task<ActionResult> DangkyUser(DangkyUserModel dto)
        {
            var existUsername = await _userManager.FindByNameAsync(dto.UserName);

            if (existUsername != null) return new BadRequestObjectResult($"Username đã tồn tại");

            var existEmail = await _userManager.FindByEmailAsync(dto.Email);

            if (existEmail != null) return new BadRequestObjectResult($"Email đã có người sử dụng");

            var existIdNguoiDung = await _context.User1s.FirstOrDefaultAsync(u => u.IdNguoiDung == dto.IdNguoiDung);
            if (existIdNguoiDung != null) return new BadRequestObjectResult($"Người dùng đã có tài khoản");

            var appUser = new Users
            {
                UserName = dto.UserName,
                Email = dto.Email,                
                IdNguoiDung=dto.IdNguoiDung,
                TrangThai=1,

                // Các thuộc tính khác của đối tượng Users
            };


            var result1 = await _userManager.CreateAsync(appUser, dto.Password);

            if (!result1.Succeeded) return new BadRequestObjectResult(result1.Errors);

            List<string> roles = new List<string>();

            roles.AddRange(dto.Roles);
            

            var result2 = await _userManager.AddToRolesAsync(appUser, roles);

            if (!result2.Succeeded) return new BadRequestObjectResult(result2.Errors);

            var response = await _userManager.FindByNameAsync(dto.UserName);

            return new OkObjectResult(response);
        }

        public async Task<IdentityResult> DeleteUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null) return IdentityResult.Failed(new IdentityError { Description = $"Username {userName} not found" });

            // Xóa các role của người dùng
            var roles = await _userManager.GetRolesAsync(user);
            var result1 = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result1.Succeeded) return IdentityResult.Failed(result1.Errors.ToArray());

            // Xóa người dùng
            var result2 = await _userManager.DeleteAsync(user);

            if (!result2.Succeeded) return IdentityResult.Failed(result2.Errors.ToArray());

            return IdentityResult.Success;
        }

        public async Task<KhachHang> GetById(int Id)
        {
            var khachhang = await _context.KhachHangs.FindAsync(Id);

            return khachhang;
        }

        public async Task<Users> GetUserByIdUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return user;
        }

        public async Task<UserModel> GetUserByUserId(string id)
        {
            var data = from us in _context.User1s
                       select new {
                           us.Id,
                           us.UserName,
                           us.NormalizedUserName,
                           us.Email,
                           us.NormalizedEmail,
                           us.EmailConfirmed,
                           us.PasswordHash,
                           us.SecurityStamp,
                           us.ConcurrencyStamp,
                           us.PhoneNumber,
                           us.PhoneNumberConfirmed,
                           us.TwoFactorEnabled,
                           us.LockoutEnd,
                           us.LockoutEnabled,
                           us.AccessFailedCount,
                           us.IdNguoiDung,
                           us.IdKhachHang,
                           us.TrangThai
                       
                       };
            var user = await data.FirstOrDefaultAsync(a => a.Id == id);
            var user1 = await _userManager.FindByNameAsync(user.UserName);
            var userRole = await _userManager.GetRolesAsync(user1);
            if (user != null)
            {
                return new UserModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    NormalizedUserName = user.NormalizedUserName,
                    Email = user.Email,
                    NormalizedEmail = user.NormalizedEmail,
                    EmailConfirmed = user.EmailConfirmed,
                    PasswordHash = user.PasswordHash,
                    SecurityStamp = user.SecurityStamp,
                    ConcurrencyStamp = user.ConcurrencyStamp,
                    PhoneNumber = user.PhoneNumber,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    LockoutEnd = user.LockoutEnd,
                    LockoutEnabled = user.LockoutEnabled,
                    AccessFailedCount = user.AccessFailedCount,
                    IdNguoiDung = user.IdNguoiDung,
                    IdKhachHang = user.IdKhachHang,
                    TrangThai = user.TrangThai,
                    Roles=userRole.ToList()
                };
            }
            return null;
        }

        public async Task<PagedResult<UserModel>> SearchUserPaging([FromBody] Dictionary<string, object> formData)
        {
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";
            var result = from us in _context.User1s
                         select new
                         {
                             Id = us.Id,
                             UserName = us.UserName,
                             Email = us.Email,
                             IdNguoiDung = us.IdNguoiDung,
                             IdKhachHang = us.IdKhachHang,
                             TrangThai=us.TrangThai
                         };

            // Lọc các sản phẩm theo tên
            result = result.Where(x => x.UserName.Contains(ten));

            // Lấy tổng số sản phẩm thỏa mãn
            int totalItems = await result.CountAsync();

            var kq = new List<UserModel>();
            foreach (var x in await result.Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync())
            {
                var user = new UserModel()
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    IdNguoiDung = x.IdNguoiDung,
                    IdKhachHang = x.IdKhachHang,
                    TrangThai=x.TrangThai
                };

                var userEntity = await _userManager.FindByIdAsync(user.Id);
                // Lấy danh sách các vai trò của người dùng và gán cho thuộc tính Roles
                var roles = await _userManager.GetRolesAsync(userEntity);
                user.Roles = roles.ToList();

                // Thêm đối tượng UserModel vào danh sách kq
                kq.Add(user);
            }
            var pageResult = new PagedResult<UserModel>()
            {
                totalItem = totalItems,
                page = page,
                pageSize = pageSize,
                data = kq

            };

            return pageResult;
        }

        //public async Task<PagedResult<UserModel>> SearchUserPaging([FromBody] Dictionary<string, object> formData)
        //{
        //    var page = int.Parse(formData["page"].ToString());
        //    var pageSize = int.Parse(formData["pageSize"].ToString());

        //    var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";
        //    var result = from us in _context.User1s
        //                   select new { 
        //                       Id = us.Id,
        //                       UserName = us.UserName,
        //                       Email = us.Email,
        //                       IdNguoiDung = us.IdNguoiDung,
        //                       IdKhachHang = us.IdKhachHang
        //                   };

        //    var kq = await result.Where(x => x.UserName.Contains(ten)).Skip(pageSize * (page - 1)).Take(pageSize)
        //        .Select( x => new UserModel()
        //    {
        //            Id = x.Id,
        //            UserName = x.UserName,
        //            Email = x.Email,
        //            IdNguoiDung = x.IdNguoiDung,
        //            IdKhachHang = x.IdKhachHang,


        //        }).ToListAsync();

        //    var pageResult = new PagedResult<UserModel>()
        //    {
        //        totalItem = result.Count(),
        //        page = page,
        //        pageSize = pageSize,
        //        data = kq

        //    };

        //    return pageResult;
        //}

        public async Task<ActionResult> UpdateUser(DangkyUserModel dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id);

            if (user == null) return new BadRequestObjectResult($"Không tìm thấy user");

            //var existIdNguoiDung = await _context.User1s.FirstOrDefaultAsync(u => u.IdNguoiDung == dto.IdNguoiDung);
            //if (existIdNguoiDung != null) return new BadRequestObjectResult($"Người dùng đã có tài khoản");

            // Cập nhật thông tin người dùng
            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.IdNguoiDung = dto.IdNguoiDung;
            user.TrangThai = dto.TrangThai;

            var result1 = await _userManager.UpdateAsync(user);

            if (!result1.Succeeded) return new BadRequestObjectResult(result1.Errors);

            // Xóa các roles hiện tại của người dùng
            var currentRoles = await _userManager.GetRolesAsync(user);
            var result2 = await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!result2.Succeeded) return new BadRequestObjectResult(result2.Errors);

            // Thêm các roles mới cho người dùng
            List<string> roles = new List<string>();

            roles.AddRange(dto.Roles);
            var result3 = await _userManager.AddToRolesAsync(user, roles);

            if (!result3.Succeeded) return new BadRequestObjectResult(result3.Errors);


            var response = await _userManager.FindByNameAsync(dto.UserName);

            return new OkObjectResult(response);
        }
    }
}
