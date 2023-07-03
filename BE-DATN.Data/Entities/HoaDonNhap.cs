
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Entities
{
    public class HoaDonNhap
    {
        public int IdHoaDonNhap { get; set; }
        public int IdNhaCungCap { get; set; }
        public int IdNguoiDung { get; set; }
        public string GhiChu { get; set; }
        public decimal TongTien { get; set; }
        public DateTime NgayNhap { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public int TrangThaiHoaDonNhap { get; set; }

        //public List<ChiTietHoaDonNhap> ChiTietHoaDonNhaps { get; set; }

        public NhaCungCap NhaCungCap { get; set; }
        public NguoiDung NguoiDung { get; set;}
    }

}
