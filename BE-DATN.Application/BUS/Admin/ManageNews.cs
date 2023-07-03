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
    public class ManageNews : IManageNews
    {
        private readonly BEDATNDbContext _context;
        public ManageNews(BEDATNDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(NewsModel news)
        {
            _context.Newss.Add(news.news);
            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<int> Delete(int Id)
        {
            var tintuc = await _context.Newss.FindAsync(Id);

            _context.Newss.Remove(tintuc);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<News> GetById(int Id)
        {
            var tintuc = await _context.Newss.FindAsync(Id);

            return tintuc;
        }

        public Task<List<News>> GetNews()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<NewsModel>> SearchNewsPaging([FromBody] Dictionary<string, object> formData)
        {
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";
            var result = from n in _context.Newss
                         join us in _context.NguoiDungs on n.IdNguoiDung equals us.IdNguoiDung
                         select new { IdNews = n.IdNews, LoaiTin = n.LoaiTin, Title = n.Title,Anh=n.Anh, NoiDung = n.NoiDung, IdNguoiDung = n.IdNguoiDung,NgayDang=n.NgayDang,TrangThai=n.TrangThai,TenNguoiDung=us.HoTen };

            // Lọc các sản phẩm theo tên
            result = result.Where(x => x.LoaiTin.Contains(ten) || x.Title.Contains(ten));

            // Lấy tổng số sản phẩm thỏa mãn
            int totalItems = await result.CountAsync();

            var kq = await result.Skip(pageSize * (page - 1)).Take(pageSize).Select(x => new NewsModel()
            {
                IdNews = x.IdNews,
                LoaiTin = x.LoaiTin,
                Title = x.Title,
                NoiDung = x.NoiDung,
                Anh=x.Anh,
                IdNguoiDung = x.IdNguoiDung,
                NgayDang = x.NgayDang,
                TrangThai = x.TrangThai,
                TenNguoiDung=x.TenNguoiDung

            }).ToListAsync();

            var pageResult = new PagedResult<NewsModel>()
            {
                totalItem = totalItems,
                page = page,
                pageSize = pageSize,
                data = kq

            };
            return pageResult;
        }

        public async Task<int> Update(NewsModel news)
        {

            var tintuc = await _context.Newss.FindAsync(news.news.IdNews);

            tintuc.LoaiTin = news.news.LoaiTin;
            tintuc.Title = news.news.Title;
            tintuc.NoiDung = news.news.NoiDung;
            tintuc.Anh = news.news.Anh;
            tintuc.IdNguoiDung = news.news.IdNguoiDung;
            
            tintuc.TrangThai = news.news.TrangThai;

            await _context.SaveChangesAsync();

            return 1;
        }
    }
}
