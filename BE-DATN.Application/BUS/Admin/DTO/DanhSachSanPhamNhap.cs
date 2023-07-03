using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Admin.DTO
{
    public class DanhSachSanPhamNhap
    {

        public string TenMau { get; set; }
        public string Size { get; set; }
        public int SoLuong { get; set; }
        public MauSanPham mauSanPham { get; set; }
        public SizeSanPham sizeSanPham { get; set; }
    }
}
