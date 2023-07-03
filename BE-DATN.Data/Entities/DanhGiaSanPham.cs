using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Entities
{
    public class DanhGiaSanPham
    {
        public int IdDanhGia { get; set; }
        public int IdSanPham { get; set; }
        public int IdKhachHang { get; set; }

        public string NoiDungDanhGia { get;set; }
        public DateTime NgayDanhGia { get;set; }
    }
}
