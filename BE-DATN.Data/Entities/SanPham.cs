
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Entities
{
    public class SanPham
    {
        public int IdSanPham { get; set; }
        public int IdDanhMuc { get; set; }
        public string TenSanPham { get; set; }
        public string MoTaSanPham { get; set; }
        public string AnhSanPham { get; set; }
        public string XuatXu { get; set; }
        public string ChatLieu { get; set; }
        public int IdThuongHieu { get; set; }
        public DateTime NgayTao { get; set; }
        public int TrangThai { get; set; }

        //public List<ChiTietDonHang> ChiTietDonHangs { get; set; }

        //public List<ChiTietHoaDonNhap> ChiTietHoaDonNhaps { get; set; }

        //public List<MauSanPham> MauSanPhams { get; set; }

        //public SizeSanPham SizeSanPham { get; set; }

        public DanhMuc DanhMuc { get; set; }

        public ThuongHieu ThuongHieu { get;set; }

    }

}
