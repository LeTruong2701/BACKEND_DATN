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
    public class ManageKhuyenMai : IManageKhuyenMai
    {

        private readonly BEDATNDbContext _context;
        public ManageKhuyenMai(BEDATNDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(KhuyenMaiModel km)
        {
            _context.KhuyenMais.Add(km.khuyenMai);
            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<int> Delete(int Id)
        {
            var khuyenmai = await _context.KhuyenMais.FindAsync(Id);

            _context.KhuyenMais.Remove(khuyenmai);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<KhuyenMai> GetById(int Id)
        {
            var khuyenmai = await _context.KhuyenMais.FindAsync(Id);

            return khuyenmai;
        }

        public Task<List<KhuyenMai>> GetKhuyenMai()
        {
            throw new NotImplementedException();
        }

        public Task<List<KhuyenMai>> GetKhuyenMaiTrangNguoiDung()
        {
            var result = from nv in _context.KhuyenMais
                         where nv.NgayBatDau <= DateTime.Now && DateTime.Now <= nv.NgayKetThuc && nv.TrangThai == 1
                         select new KhuyenMai
                         {
                             IdKhuyenMai = nv.IdKhuyenMai,
                             MaKhuyenMai = nv.MaKhuyenMai,
                             TenKhuyenMai = nv.TenKhuyenMai,
                             MoTa = nv.MoTa,
                             PhanTramGiam = nv.PhanTramGiam,
                             NgayBatDau = nv.NgayBatDau,
                             GiaTienGiam = nv.GiaTienGiam,
                             NgayKetThuc = nv.NgayKetThuc,
                             TrangThai = nv.TrangThai,
                             DieuKienHoaDon = nv.DieuKienHoaDon
                         };
            // Lọc các sản phẩm theo tên
            
            return result.ToListAsync();
        }

        public async Task<PagedResult<KhuyenMai>> SearchKhuyenMaiPaging([FromBody] Dictionary<string, object> formData)
        {
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";
            var result = from nv in _context.KhuyenMais
                         select new { IdKhuyenMai = nv.IdKhuyenMai, MaKhuyenMai = nv.MaKhuyenMai, TenKhuyenMai = nv.TenKhuyenMai, MoTa = nv.MoTa,
                             PhanTramGiam = nv.PhanTramGiam, NgayBatDau = nv.NgayBatDau,GiaTienGiam=nv.GiaTienGiam,
                             NgayKetThuc = nv.NgayKetThuc,TrangThai=nv.TrangThai,DieuKienHoaDon=nv.DieuKienHoaDon };
            // Lọc các sản phẩm theo tên
            result = result.Where(x => x.TenKhuyenMai.Contains(ten)||x.MaKhuyenMai.Contains(ten));

            // Lấy tổng số sản phẩm thỏa mãn
            int totalItems = await result.CountAsync();

            var kq = await result.Skip(pageSize * (page - 1)).Take(pageSize).Select(x => new KhuyenMai()
            {
                IdKhuyenMai = x.IdKhuyenMai,
                TenKhuyenMai = x.TenKhuyenMai,
                MaKhuyenMai = x.MaKhuyenMai,
                MoTa = x.MoTa,
                PhanTramGiam = x.PhanTramGiam,
                NgayBatDau = x.NgayBatDau,
                NgayKetThuc = x.NgayKetThuc,
                TrangThai = x.TrangThai,
                GiaTienGiam = x.GiaTienGiam,
                DieuKienHoaDon =x.DieuKienHoaDon


            }).ToListAsync();

            var pageResult = new PagedResult<KhuyenMai>()
            {
                totalItem = totalItems,
                page = page,
                pageSize = pageSize,
                data = kq

            };

            return pageResult;
        }

        public async Task<int> Update(KhuyenMaiModel km)
        {
            var khuyenmai = await _context.KhuyenMais.FindAsync(km.khuyenMai.IdKhuyenMai);

            khuyenmai.MaKhuyenMai = km.khuyenMai.MaKhuyenMai;
            khuyenmai.TenKhuyenMai = km.khuyenMai.TenKhuyenMai;
            khuyenmai.MoTa = km.khuyenMai.MoTa;
            khuyenmai.PhanTramGiam = km.khuyenMai.PhanTramGiam;
            khuyenmai.NgayBatDau = km.khuyenMai.NgayBatDau;
            khuyenmai.NgayKetThuc = km.khuyenMai.NgayKetThuc;
            khuyenmai.TrangThai = km.khuyenMai.TrangThai;
            khuyenmai.GiaTienGiam = km.khuyenMai.GiaTienGiam;
            khuyenmai.DieuKienHoaDon = km.khuyenMai.DieuKienHoaDon;

            await _context.SaveChangesAsync();

            return 1;
        }
    }
}
