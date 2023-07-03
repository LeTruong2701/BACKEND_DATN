using BE_DATN.Application.BUS.Home.DTO;
using BE_DATN.Application.BUS.Home.Interfaces;
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

namespace BE_DATN.Application.BUS.Home
{
    public class NewsManage : INews
    {
        private readonly BEDATNDbContext _context;
        public NewsManage(BEDATNDbContext context)
        {
            _context = context;
        }
        public async Task<News> GetById(int Id)
        {
            var tintuc = await _context.Newss.FindAsync(Id);

            return tintuc;
        }

        public async Task<List<NewsModel>> GetNewsGanDay()
        {
            var data = from sp in _context.Newss
                       orderby sp.NgayDang descending //sắp xếp sản phẩm theo ngày tạo giảm dần để lấy sản phẩm mới nhất
                       select new
                       {
                           IdNews = sp.IdNews,
                           LoaiTin = sp.LoaiTin,
                           Title = sp.Title,
                           Anh = sp.Anh,
                           NoiDung = sp.NoiDung,
                           IdNguoiDung = sp.IdNguoiDung,
                           NgayDang = sp.NgayDang,
                           TrangThai = sp.TrangThai
                       };
            var result = await data.Take(2).Select(x => new NewsModel()
            {
                IdNews = x.IdNews,
                LoaiTin = x.LoaiTin,
                Title = x.Title,
                Anh = x.Anh,
                NoiDung = x.NoiDung,
                IdNguoiDung = x.IdNguoiDung,
                NgayDang = x.NgayDang,
                TrangThai = x.TrangThai

            }).ToListAsync();

            var kq = new List<NewsModel>();
            foreach (var x in result)
            {
                var news = new NewsModel()
                {
                    IdNews = x.IdNews,
                    LoaiTin = x.LoaiTin,
                    Title = x.Title,
                    NoiDung = x.NoiDung,
                    Anh = x.Anh,
                    IdNguoiDung = x.IdNguoiDung,
                    NgayDang = x.NgayDang,
                    TrangThai = x.TrangThai,
                };

                var nguoidung = await _context.NguoiDungs.FindAsync(news.IdNguoiDung);


                news.TenNguoiDung = nguoidung.HoTen;

                // Thêm đối tượng UserModel vào danh sách kq
                kq.Add(news);
            }

            return kq;
        }

        public Task<List<NguoiDungModel>> GetNguoiDangBai()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<NewsModel>> SearchNewsPaging([FromBody] Dictionary<string, object> formData)
        {
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            string locnews = "";

            if (formData.Keys.Contains("locnews") && !string.IsNullOrEmpty(Convert.ToString(formData["locnews"])))
            { locnews = formData["locnews"].ToString(); }

            var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";
            var result = from n in _context.Newss
                         select new NewsModel
                         {
                             IdNews = n.IdNews, 
                             LoaiTin = n.LoaiTin, 
                             Title = n.Title, 
                             Anh = n.Anh, 
                             NoiDung = n.NoiDung,
                             IdNguoiDung = n.IdNguoiDung,
                             NgayDang = n.NgayDang, 
                             TrangThai = n.TrangThai
                         };

            // Lọc các sản phẩm theo tên title
            var resultkeyword = result.Where(x => x.Title.Contains(ten) || x.Title.Contains(ten));

            // Lọc các sản phẩm theo loại tin
            var resultloaitin = result;

            List<NewsModel> ketqua = new List<NewsModel>();
            int total = 0;
            if (locnews != null && ten == "")
            {
                total = resultloaitin.Count();

                switch (locnews)
                {
                    case "Kinh nghiệm":
                        resultloaitin = resultloaitin.Where(s => s.LoaiTin == locnews );

                        ketqua = resultloaitin.ToList();
                        break;
                    case "Thời trang":
                        resultloaitin = resultloaitin.Where(s => s.LoaiTin == locnews);

                        ketqua = resultloaitin.ToList();
                        break;
                    case "Sức khỏe":
                        resultloaitin = resultloaitin.Where(s => s.LoaiTin == locnews);

                        ketqua = resultloaitin.ToList();
                        break;
                    case "Mẹo":
                        resultloaitin = resultloaitin.Where(s => s.LoaiTin == locnews);

                        ketqua = resultloaitin.ToList();
                        break;


                  
                    default:
                        ketqua = resultloaitin.ToList();
                       
                        break;
                }
            }
            else
            {

                total = resultkeyword.Count();
                ketqua = resultkeyword.ToList();
            }



            // Lấy tổng số sản phẩm thỏa mãn
            int totalItems =total;

            var kq = new List<NewsModel>();
            foreach (var x in  ketqua.Skip(pageSize * (page - 1)).Take(pageSize).ToList())
            {
                var news = new NewsModel()
                {
                    IdNews = x.IdNews,
                    LoaiTin = x.LoaiTin,
                    Title = x.Title,
                    NoiDung = x.NoiDung,
                    Anh = x.Anh,
                    IdNguoiDung = x.IdNguoiDung,
                    NgayDang = x.NgayDang,
                    TrangThai = x.TrangThai,
                };

                var nguoidung = await _context.NguoiDungs.FindAsync(news.IdNguoiDung);
              
                
                news.TenNguoiDung = nguoidung.HoTen;

                // Thêm đối tượng UserModel vào danh sách kq
                kq.Add(news);
            }

            var pageResult = new PagedResult<NewsModel>()
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
