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
    public class ManageThuongHieu : IManageThuongHieu
    {
        private readonly BEDATNDbContext _context;
        public ManageThuongHieu(BEDATNDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(ThuongHieuModel th)
        {
            _context.ThuongHieus.Add(th.thuongHieu);
            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<int> Delete(int Id)
        {
            var thuonghieu = await _context.ThuongHieus.FindAsync(Id);

            _context.ThuongHieus.Remove(thuonghieu);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<ThuongHieu> GetById(int Id)
        {
            var thuonghieu = await _context.ThuongHieus.FindAsync(Id);
            return thuonghieu;
        }

        public async Task<List<ThuongHieuModel>> GetListThuongHieu()
        {
            var data = from th in _context.ThuongHieus
                       select new { th.IdThuongHieu, th.TenThuongHieu,th.TrangThai};
            return await data.Where(a => a.TrangThai == 1).Select(x => new ThuongHieuModel()
            {
                IdThuongHieu = x.IdThuongHieu,
                TenThuongHieu = x.TenThuongHieu,
               

            }).ToListAsync();
        }

        public async Task<PagedResult<ThuongHieu>> SearchThuongHieuPaging([FromBody] Dictionary<string, object> formData)
        {
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";
            var result = from nv in _context.ThuongHieus
                         select new { IdThuongHieu = nv.IdThuongHieu, TenThuongHieu = nv.TenThuongHieu, MoTa = nv.MoTa, TrangThai = nv.TrangThai };

            // Lọc các sản phẩm theo tên
            result = result.Where(x => x.TenThuongHieu.Contains(ten));

            // Lấy tổng số sản phẩm thỏa mãn
            int totalItems = await result.CountAsync();

            var kq = await result.Skip(pageSize * (page - 1)).Take(pageSize).Select(x => new ThuongHieu()
            {
                IdThuongHieu = x.IdThuongHieu,
                TenThuongHieu = x.TenThuongHieu,
            
                MoTa = x.MoTa,
                TrangThai = x.TrangThai


            }).ToListAsync();

            var pageResult = new PagedResult<ThuongHieu>()
            {
                totalItem = totalItems,
                page = page,
                pageSize = pageSize,
                data = kq

            };

            return pageResult;
        }

        public async Task<int> Update(ThuongHieuModel th)
        {
            var thuonghieu = await _context.ThuongHieus.FindAsync(th.thuongHieu.IdThuongHieu);

            thuonghieu.TenThuongHieu = th.thuongHieu.TenThuongHieu;
            thuonghieu.MoTa = th.thuongHieu.MoTa;
            thuonghieu.TrangThai = th.thuongHieu.TrangThai;
           



            await _context.SaveChangesAsync();

            return 1;
        }
    }
}
