using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Admin.DTO
{
    public class ChiTietHoaDonNhapModel
    {
        public int IdChiTietHoaDonNhap { get; set; }
        public int IdHoaDonNhap { get; set; }
        public int IdSanPham { get; set; }
        public int IdMauSanPham { get; set; }
        public int IdSizeSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }

        public string TenMau { get; set; }

        public string Size { get; set; }

        public ChiTietHoaDonNhap chiTietHoaDonNhap { get; set; }


        public string TenSanPham { get; set; }
    }
}
