using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Admin.DTO
{
    public class DataNhapHoaDonNhap
    {

        public decimal GiaNhap { get; set; }
        public int IdSanPham { get; set; }
        public HoaDonNhapModel hoadonnhap { get; set; }
        public SizeSanPhamModel ssp { get; set; }

        public List<MauSanPhamModel> listmsp { get; set; }

        public List<SizeSanPham> sizesanpham { get; set; }
        public List<ChiTietHoaDonNhapModel> listCTHDN { get; set; }
    }
}
