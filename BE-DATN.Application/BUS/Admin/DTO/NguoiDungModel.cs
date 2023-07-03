using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Admin.DTO
{
    public class NguoiDungModel
    {
        public int IdNguoiDung { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string AnhDaiDien { get; set; }
        public int TrangThai { get; set; }

        public NguoiDung nguoiDung { get; set; }
    }
}
