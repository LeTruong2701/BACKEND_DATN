using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.BUS.Admin.Interfaces;
using BE_DATN.Application.Common;
using BE_DATN.Data.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Admin
{
    public class ManageKho : IManageKho
    {
        private readonly BEDATNDbContext _context;
        public ManageKho(BEDATNDbContext context)
        {
            _context = context;
        }
        public async Task<PagedResult<KhoModel>> SearchKhoPaging([FromBody] Dictionary<string, object> formData)
        {
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";
            var result = from k in _context.Khos
                         join sp in _context.SanPhams on k.IdSanPham equals sp.IdSanPham
                         join msp in _context.MauSanPhams on k.IdMauSanPham equals msp.IdMauSanPham
                         join ssp in _context.SizeSanPhams on k.IdSizeSanPham equals ssp.IdSizeSanPham
                         select new { IdKho = k.IdKho, IdSanPham = k.IdSanPham, IdMauSanPham = k.IdMauSanPham, IdSizeSanPham = k.IdSizeSanPham,
                             SoLuong = k.SoLuong,sp.TenSanPham,msp.TenMau,msp.MaMau,ssp.Size };

            // Lọc các sản phẩm theo tên
            result = result.Where(x => x.TenSanPham.Contains(ten) || x.TenMau.Contains(ten));

            // Lấy tổng số sản phẩm thỏa mãn
            int totalItems = await result.CountAsync();

            var kq = await result.Skip(pageSize * (page - 1)).Take(pageSize).Select(x => new KhoModel()
            {
                IdKho = x.IdKho,
                IdSanPham = x.IdSanPham,
                IdMauSanPham = x.IdMauSanPham,
                IdSizeSanPham = x.IdSizeSanPham,
                TenSanPham=x.TenSanPham,
                TenMau=x.TenMau,
                MaMau=x.MaMau,
                Size=x.Size,
                SoLuong = x.SoLuong,
            }).ToListAsync();

            var pageResult = new PagedResult<KhoModel>()
            {
                totalItem = totalItems,
                page = page,
                pageSize = pageSize,
                data = kq

            };

            return pageResult;
        }
    }
}
