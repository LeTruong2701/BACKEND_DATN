using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Home.DTO
{
    public class DonHangModel
    {
        public int IdDonHang { get; set; }
        public int? IdKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string GhiChu { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public string PhuongThucThanhToan { get; set; }
        public DateTime NgayDat { get; set; }
        public string TrangThaiDonHang { get; set; }
        public string Email { get; set; }
        public int TrangThaiThanhToan { get; set; }


        public decimal TongTien { get; set; }
        public string MaKhuyenMai { get; set; }

        public List<ChiTietDonHang> ctdh { get; set; }

    }
}
