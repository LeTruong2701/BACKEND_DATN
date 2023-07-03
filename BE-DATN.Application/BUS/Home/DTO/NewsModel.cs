using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Home.DTO
{
    public class NewsModel
    {
        public int IdNews { get; set; }
        public string LoaiTin { get; set; }
        public string Title { get; set; }
        public string NoiDung { get; set; }
        public string Anh { get; set; }
        public int IdNguoiDung { get; set; }
        public DateTime NgayDang { get; set; }

        public string TenNguoiDung { get; set; }

        public int TrangThai { get; set; }
        public News news { get; set; }
    }
}
