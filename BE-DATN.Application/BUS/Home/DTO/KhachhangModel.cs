using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Home.DTO
{
    public class KhachhangModel
    {
        public int IdKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string AnhDaiDien { get; set; }
        public string DiaChiGiaoHang { get; set; }
    }
}
