
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.Authenticate
{
    public class UserViewModel
    {
        public int? IdNguoiDung { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string AnhDaiDien { get; set; }
        public int TrangThai { get; set; }
        public int IdAccount { get; set; }

        public int? IdKhachHang { get; set; }
  
       
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string LoaiQuyen { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
    }
}
