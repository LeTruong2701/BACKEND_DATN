using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Entities
{
    public class KhuyenMai
    {
        public int IdKhuyenMai { get; set; }
        public string MaKhuyenMai { get; set; }
        public string TenKhuyenMai { get; set; }
        public string MoTa { get; set; }
        public decimal? PhanTramGiam { get; set; }
        public decimal? GiaTienGiam { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public decimal? DieuKienHoaDon { get; set; }
        public int TrangThai { get; set; }

        //public SanPham SanPham { get; set; }

        //public MauSanPham MauSanPham { get; set; }


    }

}
