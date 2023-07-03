using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.BUS.Admin.Interfaces;
using BE_DATN.Application.Common;
using BE_DATN.Data.EF;
using BE_DATN.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Admin
{
    public class ManageNguoiDung : IManageNguoiDung
    {
        private readonly BEDATNDbContext _context;
        private readonly UserManager<Users> _userManager;
        public ManageNguoiDung(BEDATNDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager=userManager;
        }
        public async Task<int> Create(NguoiDungModel nd)
        {
            _context.NguoiDungs.Add(nd.nguoiDung);
            await _context.SaveChangesAsync();

            

            return 1;
        }

        public async Task<int> Delete(int Id)
        {
            var nguoidung = await _context.NguoiDungs.FindAsync(Id);

            _context.NguoiDungs.Remove(nguoidung);
            await _context.SaveChangesAsync();

            var userName = (from usn in _context.User1s
                            where usn.IdNguoiDung == Id
                            select usn.UserName).SingleOrDefault();

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null) return 0;

            // Xóa các role của người dùng
            var roles = await _userManager.GetRolesAsync(user);
            var result1 = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result1.Succeeded) return 0;

            // Xóa người dùng
            var result2 = await _userManager.DeleteAsync(user);

            if (!result2.Succeeded) return 0;

            
            return 1;
        }

        public async Task<NguoiDung> GetById(int Id)
        {
            var nguoidung = await _context.NguoiDungs.FindAsync(Id);

            return nguoidung;
        }

        public async Task<List<NguoiDungModel>> GetListNguoiDung()
        {
            var data = from th in _context.NguoiDungs
                       select new {
                           IdNguoiDung = th.IdNguoiDung,
                           HoTen = th.HoTen,
                           GioiTinh = th.GioiTinh,
                           NgaySinh = th.NgaySinh,
                           DiaChi = th.DiaChi,
                           SDT = th.SDT,
                           AnhDaiDien = th.AnhDaiDien,
                           Email = th.Email,
                           TrangThai = th.TrangThai,
                       };
            return await data.Where(a => a.TrangThai == 1).Select(x => new NguoiDungModel()
            {
                IdNguoiDung = x.IdNguoiDung,
                HoTen = x.HoTen,
                GioiTinh = x.GioiTinh,
                NgaySinh = x.NgaySinh,
                DiaChi = x.DiaChi,
                SDT = x.SDT,
                AnhDaiDien = x.AnhDaiDien,

                Email = x.Email,
                TrangThai = x.TrangThai,


            }).ToListAsync();
        }

        public Task<List<NguoiDung>> GetNguoiDung()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<NguoiDung>> SearchNguoiDungPaging([FromBody] Dictionary<string, object> formData)
        {
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";
            var result = from nv in _context.NguoiDungs
                         select new { IdNguoiDung = nv.IdNguoiDung, HoTen = nv.HoTen, GioiTinh = nv.GioiTinh, NgaySinh = nv.NgaySinh, DiaChi=nv.DiaChi, SDT=nv.SDT, Email=nv.Email, AnhDaiDien=nv.AnhDaiDien, TrangThai=nv.TrangThai };

            // Lọc các sản phẩm theo tên
            result = result.Where(x => x.HoTen.Contains(ten));

            // Lấy tổng số sản phẩm thỏa mãn
            int totalItems = await result.CountAsync();

            var kq = await result.Skip(pageSize * (page - 1)).Take(pageSize).Select(x => new NguoiDung()
            {
                IdNguoiDung = x.IdNguoiDung,
                HoTen = x.HoTen,
                GioiTinh = x.GioiTinh,
                NgaySinh = x.NgaySinh,
                DiaChi = x.DiaChi,
                SDT = x.SDT,
                AnhDaiDien = x.AnhDaiDien,
                
                Email = x.Email,
                TrangThai = x.TrangThai,


            }).ToListAsync();

            var pageResult = new PagedResult<NguoiDung>()
            {
                totalItem =totalItems,
                page = page,
                pageSize = pageSize,
                data = kq

            };

            return pageResult;
        }

        public async Task<int> Update(NguoiDungModel nd)
        {
            var nguoidung = await _context.NguoiDungs.FindAsync(nd.IdNguoiDung);

            nguoidung.HoTen = nd.HoTen;
            nguoidung.GioiTinh = nd.GioiTinh;
            nguoidung.NgaySinh = nd.NgaySinh;
            nguoidung.DiaChi = nd.DiaChi;
            nguoidung.SDT = nd.SDT;
            nguoidung.Email = nd.Email;
            nguoidung.AnhDaiDien = nd.AnhDaiDien;

            await _context.SaveChangesAsync();

            return 1;
        }
    }
}
