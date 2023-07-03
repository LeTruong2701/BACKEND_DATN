using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Admin.DTO
{
    public class NhanVienModel
    {
        public int IdNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public DateTime? NgayVaoLam { get; set; }
        public int TrangThai { get; set; }

        public NhanVien nhanVien { get; set; }
    }
}
