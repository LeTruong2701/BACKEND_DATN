using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Admin.DTO
{
    public class KhuyenMaiModel
    {
        public int IdKhuyenMai { get; set; }
        public string MaKhuyenMai { get; set; }
        public string TenKhuyenMai { get; set; }
        public string MoTa { get; set; }
        public decimal PhanTramGiam { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }

        public int TrangThai { get; set; }

        public KhuyenMai khuyenMai { get; set; }
    }
}
