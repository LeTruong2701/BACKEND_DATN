using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Admin.DTO
{
    public class MauSanPhamModel
    {
        public int IdMauSanPham { get; set; }
        public int IdSanPham { get; set; }
        public int IdDanhMuc { get; set; }
        public int IdThuongHieu { get; set; }
        public string TenSanPham { get; set; }
        public DateTime NgayTao { get; set; }
        public int TrangThai { get; set; }
        public string TenMau { get; set; }
        public string MaMau { get; set; }
        public decimal GiaNhap { get; set; }
        public decimal GiaBan { get; set; }
        public string AnhSanPham { get; set; }
        public int CheckThanhToan { get; set; }

        public MauSanPham mauSanPham { get; set; }
        public SanPham sanPham { get; set; }
     

        public List<SizeSanPham> sizeSanPham{ get; set; }
    }
}
