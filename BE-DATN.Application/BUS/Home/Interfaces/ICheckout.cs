using BE_DATN.Application.BUS.Home.DTO;
using BE_DATN.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Home.Interfaces
{
    public interface ICheckout
    {
        Task<KhachHang> GetKhachHangById(int Id);

        Task<KhuyenMai> GetKhuyenMaiByMaKhuyenMai(string makhuyenmai);

        Task<ActionResult> CreateDonHang([FromBody] DonHangModel model);

        Task<ActionResult> CreateDonHangVoiViMoMo([FromBody] DonHangModel model);

    }
}
