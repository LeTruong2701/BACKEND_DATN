
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Entities
{
    public class Account
    {
        public int IdAccount { get; set; }
        public int? IdNguoiDung { get; set; }
        public int? IdKhachHang { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string LoaiQuyen { get; set; }
        public int TrangThai { get; set; }

        public NguoiDung NguoiDung { get; set; }

        public KhachHang KhachHang { get; set;}
        
    }
}
