using BE_DATN.Application.BUS.Admin.Interfaces;
using BE_DATN.Data.EF;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Data.Entities;
using BE_DATN.Application.Common;

namespace BE_DATN.Application.BUS.Admin
{
    public class ManageDanhMuc : IManageDanhMuc
    {

        private readonly BEDATNDbContext _context;
        public ManageDanhMuc(BEDATNDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(DanhMucModel dm)
        {

            _context.DanhMucs.Add(dm.danhmuc);
            await _context.SaveChangesAsync();

            return 1;
            //var danhmuc = new DanhMuc()
            //{

            //    IdDanhMuc = dm.IdDanhMuc,
            //    IdDanhMucCha = dm.IdDanhMucCha,
            //    TenDanhMuc = dm.TenDanhMuc,
            //    MoTa = dm.MoTa,
            //    TrangThai = dm.TrangThai

            //};

            //_context.DanhMucs.Add(danhmuc);

            // await _context.SaveChangesAsync();
            //return 1;
        }

        public async Task<int> Delete(int Id)
        {
            var danhmuc = await _context.DanhMucs.FindAsync(Id);

            _context.DanhMucs.Remove(danhmuc);
            await _context.SaveChangesAsync();
            return 1;
        }


        public async Task<DanhMuc> GetById(int Id)
        {
            var danhmuc = await _context.DanhMucs.FindAsync(Id);

            return danhmuc;
        }


        public async Task<List<DanhMucModel>> GetDanhMucLon()
        {
            var data = from dm in _context.DanhMucs
                       select new { dm.IdDanhMuc, dm.IdDanhMucCha, dm.TenDanhMuc };
            return await data.Where(a=>a.IdDanhMucCha==null).Select(x => new DanhMucModel()
            {
                IdDanhMuc = x.IdDanhMuc,
                IdDanhMucCha=x.IdDanhMucCha,
                TenDanhMuc=x.TenDanhMuc,
               
            }).ToListAsync();
        }

        public async Task<List<DanhMucModel>> GetDanhMucNho(int IdDanhMuc)
        {
            var data = from dm in _context.DanhMucs
                       select new { dm.IdDanhMuc, dm.IdDanhMucCha, dm.TenDanhMuc };
            return await data.Where(a => a.IdDanhMucCha == IdDanhMuc).Select(x => new DanhMucModel()
            {
                IdDanhMuc = x.IdDanhMuc,
                IdDanhMucCha = x.IdDanhMucCha,
                TenDanhMuc = x.TenDanhMuc,

            }).ToListAsync();
        }

        public async Task<List<DanhMucModel>> GetListDanhMucCon()
        {
            var data = from dm in _context.DanhMucs
                       select new { dm.IdDanhMuc, dm.IdDanhMucCha, dm.TenDanhMuc };
            return await data.Where(a => a.IdDanhMucCha != null).Select(x => new DanhMucModel()
            {
                IdDanhMuc = x.IdDanhMuc,
                IdDanhMucCha = x.IdDanhMucCha,
                TenDanhMuc = x.TenDanhMuc,

            }).ToListAsync();
        }

        public async Task<PagedResult<DanhMucModel>> SearchDanhMucPaging([FromBody] Dictionary<string, object> formData)
        {
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";
            var result = from dm in _context.DanhMucs
                         join dmCha in _context.DanhMucs on dm.IdDanhMucCha equals dmCha.IdDanhMuc into gj
                         from subDmCha in gj.DefaultIfEmpty()
                         select new
                         {
                             IdDanhMuc = dm.IdDanhMuc,
                             IdDanhMucCha = dm.IdDanhMucCha,
                             TenDanhMuc = dm.TenDanhMuc,
                             TrangThai = dm.TrangThai,
                             TenDanhMucCha = subDmCha != null ? subDmCha.TenDanhMuc : ""
                         };

            // Lọc các sản phẩm theo tên
            result = result.Where(x => x.TenDanhMuc.Contains(ten));

            // Lấy tổng số sản phẩm thỏa mãn
            int totalItems = await result.CountAsync();

            var kq = await result.Skip(pageSize * (page - 1)).Take(pageSize).Select(x => new DanhMucModel()
            {
                IdDanhMuc = x.IdDanhMuc,
                IdDanhMucCha = x.IdDanhMucCha,
                TenDanhMuc = x.TenDanhMuc,
                TrangThai = x.TrangThai,
                TenDanhMucCha = x.TenDanhMucCha
            }).ToListAsync();


            var pageResult = new PagedResult<DanhMucModel>()
            {
                totalItem = totalItems,
                page = page,
                pageSize = pageSize,
                data = kq

            };

            return pageResult;
        }


        //public async Task<PagedResult<DanhMuc>> SearchDanhMucPaging([FromBody] Dictionary<string, object> formData)
        //{
        //    var page = int.Parse(formData["page"].ToString());
        //    var pageSize = int.Parse(formData["pageSize"].ToString());

        //    var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";
        //    var result = from dm in _context.DanhMucs
        //                 select new { IdDanhMuc = dm.IdDanhMuc, IdDanhMucCha = dm.IdDanhMucCha, TenDanhMuc = dm.TenDanhMuc, TrangThai = dm.TrangThai};

        //    // Lọc các sản phẩm theo tên
        //    result = result.Where(x => x.TenDanhMuc.Contains(ten));

        //    // Lấy tổng số sản phẩm thỏa mãn
        //    int totalItems = await result.CountAsync();



        //    var kq =await result.Skip(pageSize * (page - 1)).Take(pageSize).Select(x => new DanhMuc()
        //    {
        //        IdDanhMuc=x.IdDanhMuc,
        //        IdDanhMucCha=x.IdDanhMucCha,
        //        TenDanhMuc=x.TenDanhMuc,
        //        TrangThai=x.TrangThai
        //    }).ToListAsync();

        //    var pageResult = new PagedResult<DanhMuc>()
        //    {
        //        totalItem = totalItems,
        //        page = page,
        //        pageSize = pageSize,
        //        data = kq

        //    };

        //    return pageResult;

        //}

        public async Task<int> Update(DanhMucModel dm)
        {
            var danhmuc = await _context.DanhMucs.FindAsync(dm.danhmuc.IdDanhMuc);

            danhmuc.IdDanhMucCha = dm.danhmuc.IdDanhMucCha;
            danhmuc.TenDanhMuc = dm.danhmuc.TenDanhMuc;
            danhmuc.MoTa = dm.danhmuc.MoTa;
            danhmuc.TrangThai = dm.danhmuc.TrangThai;

            await _context.SaveChangesAsync();

            return 1;
        }

        //public Task<int> Update(DanhMucModel danhmuc)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
