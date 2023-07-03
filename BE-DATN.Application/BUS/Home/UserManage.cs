using BE_DATN.Application.BUS.Home.DTO;
using BE_DATN.Application.BUS.Home.Interfaces;
using BE_DATN.Data.EF;
using BE_DATN.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Home
{
    public class UserManage : IUserManage
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<Roles> _roleManager;
        private readonly BEDATNDbContext _context;
        public UserManage(UserManager<Users> userManager, RoleManager<Roles> roleManager, BEDATNDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ActionResult> ChangePassword(DoiMatKhauModel dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null) return new BadRequestObjectResult($"User {dto.UserName} không tìm thấy");

            if (!await _userManager.CheckPasswordAsync(user, dto.CurentPassword))
                return new BadRequestObjectResult("Mật khẩu cũ không chính xác!");


            var result = await _userManager.ChangePasswordAsync(user, dto.CurentPassword, dto.NewPassword);

            if (!result.Succeeded) return new BadRequestObjectResult(result.Errors);

            return new OkObjectResult("Đổi mật khẩu thành công");
        }

        public async Task<ActionResult> DangkyUserKhachHang(DangkyModel dto)
        {

            var khachhang = new KhachHang()
            {
                
                TenKhachHang = dto.TenKhachHang,
                GioiTinh = dto.GioiTinh,
                NgaySinh = dto.NgaySinh,
                DiaChi = dto.DiaChi,
                SDT = dto.SDT,
                Email = dto.Email
            };

            _context.KhachHangs.Add(khachhang);
            await _context.SaveChangesAsync();

            int IdKhachHang = khachhang.IdKhachHang;



            var existUsername = await _userManager.FindByNameAsync(dto.UserName);

            if (existUsername != null) return new BadRequestObjectResult($"Username {dto.UserName} has already been taken");

            var appUser = new Users
            {
                UserName = dto.UserName,
                Email = dto.Email,
                IdKhachHang=IdKhachHang,
                TrangThai=1

                // Các thuộc tính khác của đối tượng Users
            };


            var result1 = await _userManager.CreateAsync(appUser, dto.Password);

            if (!result1.Succeeded) return new BadRequestObjectResult(result1.Errors);

            var roleResult = await _userManager.AddToRoleAsync(appUser, "Customer");
            if (!roleResult.Succeeded) return new BadRequestObjectResult(roleResult.Errors);

            

            var response = await _userManager.FindByNameAsync(dto.UserName);

            return new OkObjectResult(response);
        }

        public async Task<KhachHang> GetById(int Id)
        {
            var khachhang = await _context.KhachHangs.FindAsync(Id);

            return khachhang;
        }

        public async Task<int> Update(KhachhangModel kh)
        {
            var khachhang = await _context.KhachHangs.FindAsync(kh.IdKhachHang);

            khachhang.TenKhachHang = kh.TenKhachHang;
            khachhang.GioiTinh = kh.GioiTinh;
            khachhang.NgaySinh = kh.NgaySinh;
            khachhang.DiaChi = kh.DiaChi;
            khachhang.SDT = kh.SDT;
            khachhang.Email = kh.Email;
            khachhang.AnhDaiDien = kh.AnhDaiDien;
            khachhang.DiaChiGiaoHang = kh.DiaChiGiaoHang;

            await _context.SaveChangesAsync();

            return 1;
        }
    }
}
