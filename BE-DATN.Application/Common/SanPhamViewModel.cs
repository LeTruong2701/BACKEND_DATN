
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.Common
{
    public class SanPhamViewModel
    {
        public int IdSanPham { get; set; }
        public int IdDanhMuc { get; set; }
        public string TenSanPham { get; set; }
        public string MoTaSanPham { get; set; }
        public string AnhSanPham { get; set; }
        public int IdThuongHieu { get; set; }
        public DateTime NgayTao { get; set; }
        public int TrangThai { get; set; }

    }
}
