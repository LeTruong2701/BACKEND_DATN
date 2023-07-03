using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Admin.DTO
{
    public class SizeSanPhamModel
    {
        public int IdSizeSanPham { get; set; }
        public int IdSanPham { get; set; }
        public int IdMauSanPham { get; set; }
        public string Size { get; set; }
        public int SoLuong { get; set; }
        public int TrangThai { get; set; }

        public SizeSanPham sizeSanPham{ get; set; }
    }
}
