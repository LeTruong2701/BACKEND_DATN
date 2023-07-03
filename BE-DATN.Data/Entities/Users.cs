using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Entities
{
    public class Users: IdentityUser
    {
        public int? IdNguoiDung { get; set; }
        public int? IdKhachHang { get; set; }
        public int TrangThai { get; set; }
    }
}
