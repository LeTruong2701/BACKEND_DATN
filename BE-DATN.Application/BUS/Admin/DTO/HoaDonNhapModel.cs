using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Admin.DTO
{
    public class HoaDonNhapModel
    {
        public int IdHoaDonNhap { get; set; }
        public int IdNhaCungCap { get; set; }
        public int IdNguoiDung { get; set; }
        public string GhiChu { get; set; }
        public decimal TongTien { get; set; }
        public DateTime NgayNhap { get; set; }
        public int TrangThaiHoaDonNhap { get; set; }
        public HoaDonNhap hoadonnhap { get; set; }

        public List<MauSanPhamModel> listmsp { get; set; }

        public List<SizeSanPhamModel> listssp { get; set; }
        public List<ChiTietHoaDonNhapModel> listCTHDN { get; set; }



        public string TenSanPham { get;set; }
        public string TenNhaCungCap { get;set; }
        public string TenNguoiDung { get;set; }
        public DateTime NgayCapNhat { get; set; }

    }
}
