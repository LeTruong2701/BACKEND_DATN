using BE_DATN.Application.BUS.Home.DTO;
using BE_DATN.Application.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Home.Interfaces
{
    public interface IDonHangKhachHang
    {
        Task<PagedResult<DonHangModel>> SearchDonHangPaging([FromBody] Dictionary<string, object> formData);
        Task<List<CTDHModel>> GetChiTietDonHangByIdDonHang(int Id);

        Task<List<DonHangMoMoModel>> GetDonHangMoMo([FromBody] DonHangMoMoModel model);
        Task<int> HuyDon(DonHangModel th);
    }
}
