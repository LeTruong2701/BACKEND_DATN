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
    public class ManageNhaCungCap : IManageNhaCungCap
    {

        private readonly BEDATNDbContext _context;
        public ManageNhaCungCap(BEDATNDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(NhaCungCapModel ncc)
        {
            //var Nhacungcap = new NhaCungCap()
            //{
            //    Name = request.Name,
            //    Image = request.Image,
            //    ParentId = request.ParentId
            //};

            _context.NhaCungCaps.Add(ncc.nhacungcap);
            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<int> Delete(int Id)
        {
            var nhacungcap = await _context.NhaCungCaps.FindAsync(Id);

            _context.NhaCungCaps.Remove(nhacungcap);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<NhaCungCap> GetById(int Id)
        {
            var nhacungcap = await _context.NhaCungCaps.FindAsync(Id);

            return nhacungcap;
        }

    

        public async Task<PagedResult<NhaCungCap>> SearchNhaCungCapPaging([FromBody] Dictionary<string, object> formData)
        {
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";
            var result = from ncc in _context.NhaCungCaps
                         select new { IdNhaCungCap = ncc.IdNhaCungCap, TenNhaCungCap = ncc.TenNhaCungCap, DiaChi = ncc.DiaChi, SDT = ncc.SDT,Email=ncc.Email};

            // Lọc các sản phẩm theo tên
            result = result.Where(x => x.TenNhaCungCap.Contains(ten)||x.DiaChi.Contains(ten));

            // Lấy tổng số thỏa mãn
            int totalItems = await result.CountAsync();

            var kq = await result.Skip(pageSize * (page - 1)).Take(pageSize).Select(x => new NhaCungCap()
            {
                IdNhaCungCap = x.IdNhaCungCap,
                TenNhaCungCap = x.TenNhaCungCap,
                DiaChi = x.DiaChi,
                SDT = x.SDT,
                Email = x.Email
              
            }).ToListAsync();

            var pageResult = new PagedResult<NhaCungCap>()
            {
                totalItem =totalItems,
                page = page,
                pageSize = pageSize,
                data = kq

            };

            return pageResult;
        }

        public async Task<int> Update(NhaCungCapModel ncc)
        {
            var nhacungcap = await _context.NhaCungCaps.FindAsync(ncc.nhacungcap.IdNhaCungCap);

            nhacungcap.TenNhaCungCap = ncc.nhacungcap.TenNhaCungCap;
            nhacungcap.DiaChi = ncc.nhacungcap.DiaChi;
            nhacungcap.SDT = ncc.nhacungcap.SDT;
            nhacungcap.Email = ncc.nhacungcap.Email;

            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<List<NhaCungCapModel>> GetNhaCungCap()
        {
            var data = from ncc in _context.NhaCungCaps
                       select new { ncc.IdNhaCungCap, ncc.TenNhaCungCap,ncc.SDT,ncc.Email,ncc.DiaChi};
            return await data.Select(x => new NhaCungCapModel()
            {
                IdNhaCungCap=x.IdNhaCungCap,
                TenNhaCungCap=x.TenNhaCungCap

            }).ToListAsync();
        }

        public async Task<List<DanhMucModel>> GetDanhMucLon()
        {
            var data = from dm in _context.DanhMucs
                       select new { dm.IdDanhMuc, dm.IdDanhMucCha, dm.TenDanhMuc };
            return await data.Where(a => a.IdDanhMucCha == null).Select(x => new DanhMucModel()
            {
                IdDanhMuc = x.IdDanhMuc,
                IdDanhMucCha = x.IdDanhMucCha,
                TenDanhMuc = x.TenDanhMuc,

            }).ToListAsync();
        }


    }
}
