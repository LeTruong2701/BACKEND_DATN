using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Home.DTO
{
    public class SizeSanPhamModel
    {
       
        public int IdMauSanPham { get; set; }
        public int IdSizeSanPham { get; set; }
        public string MaMau { get; set; }

        public string TenMau { get; set; }

        public string Size { get; set; }
        public int SoLuong { get; set; }
        public int IdSanPham { get; set; }

    }
}
