using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Home.DTO
{
    public class EntityDanhMuc
    {
        public int IdDanhMuc { get; set; }
        public int? IdDanhMucCha { get; set; }
        public string TenDanhMuc { get; set; }
        public string type { get; set; }
        public List<EntityDanhMuc> DanhMucCon { get; set; }
    }
}
