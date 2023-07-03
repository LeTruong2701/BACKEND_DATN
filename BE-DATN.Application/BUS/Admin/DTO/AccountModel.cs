using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Admin.DTO
{
    public class AccountModel
    {
        public int IdAccount { get; set; }
        public int? IdNguoiDung { get; set; }
        public int? IdKhachHang { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string LoaiQuyen { get; set; }
        public int TrangThai { get; set; }

        public Account account { get; set; }
    }
}
