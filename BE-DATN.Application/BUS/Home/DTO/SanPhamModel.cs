using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Home.DTO
{
    public class SanPhamModel
    {
        public int IdSanPham { get; set; }
        public int IdDanhMuc { get; set; }
        public int? IdDanhMucCha { get; set; }
        public string TenSanPham { get; set; }
        public string MoTaSanPham { get; set; }
        public string AnhSanPham { get; set; }
        public string XuatXu { get; set; }
        public string ChatLieu { get; set; }
        public string TenThuongHieu { get; set; }
        public int IdThuongHieu { get; set; }
        public DateTime NgayTao { get; set; }
        public int TrangThai { get; set; }

        public dynamic minGiaSP { get; set; }
        public dynamic maxGiaSP { get; set; }

        public SanPham sanpham { get; set; }
        
    }
}
