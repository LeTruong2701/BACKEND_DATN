
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Entities
{
    public class SizeSanPham
    {
        public int IdSizeSanPham { get; set; }
        public int IdSanPham { get; set; }
        public int IdMauSanPham { get; set; }
        public string Size { get; set; }
        public int SoLuong { get; set; }
        public int TrangThai { get; set; }

        //public List<ChiTietDonHang> ChiTietDonHangs { get; set; }

        //public List<ChiTietHoaDonNhap> ChiTietHoaDonNhaps { get; set; }

        public MauSanPham MauSanPham { get; set; }
        //public SanPham SanPham { get; set; }



    }

}
