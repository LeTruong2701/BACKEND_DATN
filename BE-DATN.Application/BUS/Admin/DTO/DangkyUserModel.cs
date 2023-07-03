using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Admin.DTO
{
    public class DangkyUserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int? IdNguoiDung { get; set; }
        public int? IdKhachHang { get; set; }
        public int TrangThai { get; set; }
        public List<string> Roles { get; set; }
        public bool IsAdmin { get; set; }
    }
}
