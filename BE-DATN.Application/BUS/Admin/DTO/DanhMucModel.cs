using BE_DATN.Data.Entities;
using BE_DATN.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Admin.DTO
{
    public class DanhMucModel
    {
        public int IdDanhMuc { get; set; }
        public int? IdDanhMucCha { get; set; }
        public string TenDanhMuc { get; set; }
        public string MoTa { get; set; }
        public int TrangThai { get; set; }

        public string TenDanhMucCha { get; set; }

        public DanhMuc danhmuc { get; set; }
    }
}
