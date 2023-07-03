using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Admin.DTO
{
    public class ThuongHieuModel
    {
        public int IdThuongHieu { get; set; }
        public string TenThuongHieu { get; set; }
        public string MoTa { get; set; }
        public int TrangThai { get; set; }

        public ThuongHieu thuongHieu { get; set; }
    }
}
