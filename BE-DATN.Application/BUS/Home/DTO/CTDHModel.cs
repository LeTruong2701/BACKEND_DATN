using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Home.DTO
{
    public class CTDHModel
    {
        public int IdChiTietDonHang { get; set; }
        public int IdDonHang { get; set; }
        public int IdSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int IdMauSanPham { get; set; }
        public string AnhSanPham { get; set; }
        public string MaMau { get; set; }
        public int IdSizeSanPham { get; set; }
        public string Size { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaMua { get; set; }

    }
}
