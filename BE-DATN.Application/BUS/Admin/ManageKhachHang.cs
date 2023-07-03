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
    public class ManageKhachHang : IManageKhachHang
    {
        private readonly UserManager<Users> _userManager;
        private readonly BEDATNDbContext _context;
        public ManageKhachHang(UserManager<Users> userManager,BEDATNDbContext context)
        {
            _userManager= userManager;
            _context = context;
        }
        public async Task<int> Create(KhachHangModel kh)
        {
            //var khachhang = new  KhachHang()
            //{
            //    IdKhachHang=kh.IdKhachHang,
            //    TenKhachHang=   kh.TenKhachHang,
            //    GioiTinh=    kh.GioiTinh,
            //    NgaySinh= kh.NgaySinh,  
            //    DiaChi= kh.DiaChi,
            //    SDT=kh.SDT,
            //    Email=kh.Email,
            //    AnhDaiDien= kh.AnhDaiDien,
            //    DiaChiGiaoHang =kh.DiaChiGiaoHang,
            //};

            _context.KhachHangs.Add(kh.khachhang);
            await _context.SaveChangesAsync();

            return 1;
        }

        

        public async Task<int> Delete(int Id)
        {
            var khachhang = await _context.KhachHangs.FindAsync(Id);

            _context.KhachHangs.Remove(khachhang);
            await _context.SaveChangesAsync();

            var users = await _context.User1s.Where(x => x.IdKhachHang == Id).ToListAsync();
            foreach (var us in users)
            {
                var userId=us.Id;
                var user = await _userManager.FindByIdAsync(userId);
                user.IdKhachHang = null;
                await _userManager.UpdateAsync(user);

            }
            return 1;
        }

        public async Task<KhachHang> GetById(int Id)
        {
            var khachhang = await _context.KhachHangs.FindAsync(Id);

            return khachhang;
        }

        public Task<List<KhachHang>> GetKhachHang()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<KhachHang>> SearchKhachHangPaging([FromBody] Dictionary<string, object> formData)
        {
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";
            var result = from kh in _context.KhachHangs
                         select new { IdKhachHang = kh.IdKhachHang, TenKhachHang = kh.TenKhachHang, GioiTinh = kh.GioiTinh,
                             NgaySinh = kh.NgaySinh,DiaChi=kh.DiaChi,SDT=kh.SDT ,AnhDaiDien=kh.AnhDaiDien,DiaChiGiaoHang=kh.DiaChiGiaoHang,
                             Email = kh.Email,NgayTao=kh.NgayTao };

            // Lọc các sản phẩm theo tên
            result = result.Where(x => x.TenKhachHang.Contains(ten)||x.DiaChi.Contains(ten)||x.SDT.Contains(ten));

            // Lấy tổng số sản phẩm thỏa mãn
            int totalItems = await result.CountAsync();

            var kq = await result.Skip(pageSize * (page - 1)).Take(pageSize).Select(x => new KhachHang()
            {
                IdKhachHang = x.IdKhachHang,
                TenKhachHang = x.TenKhachHang,
                GioiTinh = x.GioiTinh,
                NgaySinh = x.NgaySinh,
                DiaChi = x.DiaChi,
                SDT = x.SDT,
                AnhDaiDien = x.AnhDaiDien,
                DiaChiGiaoHang = x.DiaChiGiaoHang,
                Email = x.Email,
                NgayTao=x.NgayTao

            }).ToListAsync();

            var pageResult = new PagedResult<KhachHang>()
            {
                totalItem = totalItems,
                page = page,
                pageSize = pageSize,
                data = kq

            };

            return pageResult;
        }

       

        public async Task<int> Update(KhachHangModel kh)
        {
            var khachhang = await _context.KhachHangs.FindAsync(kh.khachhang.IdKhachHang);

            khachhang.TenKhachHang = kh.khachhang.TenKhachHang;
            khachhang.GioiTinh = kh.khachhang.GioiTinh;
            khachhang.NgaySinh = kh.khachhang.NgaySinh;
            khachhang.DiaChi = kh.khachhang.DiaChi;
            khachhang.SDT = kh.khachhang.SDT;
            khachhang.AnhDaiDien = kh.khachhang.AnhDaiDien;
            khachhang.DiaChiGiaoHang = kh.khachhang.DiaChiGiaoHang;
            khachhang.Email = kh.khachhang.Email;

            await _context.SaveChangesAsync();

            return 1;
        }
    }
}
