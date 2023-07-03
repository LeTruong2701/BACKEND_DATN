using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.BUS.Admin.Interfaces;
using BE_DATN.Application.Common;
using BE_DATN.Data.EF;
using BE_DATN.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Admin
{
    public class ManageNhanVien : IManageNhanVien
    {

        private readonly BEDATNDbContext _context;
        public ManageNhanVien(BEDATNDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(NhanVienModel nv)
        {
            _context.NhanViens.Add(nv.nhanVien);
            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<int> Delete(int Id)
        {
            var nhanvien = await _context.NhanViens.FindAsync(Id);

            _context.NhanViens.Remove(nhanvien);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<NhanVien> GetById(int Id)
        {
            var nhanvien = await _context.NhanViens.FindAsync(Id);

            return nhanvien;
        }

        public Task<List<NhanVien>> GetNhanVien()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<NhanVien>> SearchNhanVienPaging([FromBody] Dictionary<string, object> formData)
        {
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";
            var result = from nv in _context.NhanViens
                         select new { IdNhanVien = nv.IdNhanVien,TenNhanVien=nv.TenNhanVien,GioiTinh=nv.GioiTinh, NgaySinh = nv.NgaySinh, DiaChi = nv.DiaChi, SDT = nv.SDT, Email = nv.Email,NgayVaoLam=nv.NgayVaoLam,TrangThai=nv.TrangThai };

            // Lọc các sản phẩm theo tên
            result = result.Where(x => x.TenNhanVien.Contains(ten));

            // Lấy tổng số thỏa mãn
            int totalItems = await result.CountAsync();

            var kq = await result.Skip(pageSize * (page - 1)).Take(pageSize).Select(x => new NhanVien()
            {
                IdNhanVien = x.IdNhanVien,
                TenNhanVien = x.TenNhanVien,
                GioiTinh = x.GioiTinh,
                NgaySinh = x.NgaySinh,
                DiaChi = x.DiaChi,
                SDT = x.SDT,
                Email = x.Email,
                NgayVaoLam = x.NgayVaoLam,
                TrangThai = x.TrangThai

            }).ToListAsync();

            var pageResult = new PagedResult<NhanVien>()
            {
                totalItem = totalItems,
                page = page,
                pageSize = pageSize,
                data = kq

            };

            return pageResult;
        }

        public async Task<int> Update(NhanVienModel nv)
        {
            var nhanvien = await _context.NhanViens.FindAsync(nv.nhanVien.IdNhanVien);

            nhanvien.TenNhanVien = nv.nhanVien.TenNhanVien;
            nhanvien.GioiTinh = nv.nhanVien.GioiTinh;
            nhanvien.NgaySinh = nv.nhanVien.NgaySinh;
            nhanvien.DiaChi = nv.nhanVien.DiaChi;
            nhanvien.SDT = nv.nhanVien.SDT;
            nhanvien.Email = nv.nhanVien.Email;
            nhanvien.NgayVaoLam = nv.nhanVien.NgayVaoLam;
            nhanvien.TrangThai = nv.nhanVien.TrangThai;


       
        await _context.SaveChangesAsync();

            return 1;
        }
    }
}
