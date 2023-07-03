using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Admin.DTO
{
    public class KhoModel
    {
        public int IdKho { get; set; }
        public int IdSanPham { get; set; }
        public int IdMauSanPham { get; set; }
        public int IdSizeSanPham { get; set; }
        public int SoLuong { get; set; }

        public string TenSanPham { get;set; }
        public string TenMau { get;set; }
        public string MaMau { get;set; }
        public string Size { get;set; }

        public Kho kho { get; set; }
    }
}
