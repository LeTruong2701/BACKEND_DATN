using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Home.DTO
{
    public class DonHangMoMoModel
    {
        public int IdSanPham { get; set; }
        public int IdSizeSanPham { set; get; }
        public int IdMauSanPham { set; get; }
        public int SoLuong { get; set; }
        public decimal GiaMua { get; set; }
        public string TenSanPham { get; set; }

        public List<ChiTietDonHang> ctdh { get; set; }
    }
}
