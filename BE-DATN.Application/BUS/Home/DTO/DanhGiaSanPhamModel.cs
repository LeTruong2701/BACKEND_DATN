using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Home.DTO
{
    public class DanhGiaSanPhamModel
    {
        public int IdDanhGia { get; set; }
        public int IdSanPham { get; set; }
        public int IdKhachHang { get; set; }

        public string NoiDungDanhGia { get; set; }
        public DateTime NgayDanhGia { get; set; }

        public string TenKhachHang { get; set; }
        public string AnhDaiDien { get; set; }
        public DanhGiaSanPham danhgia { get; set; }
    }
}
