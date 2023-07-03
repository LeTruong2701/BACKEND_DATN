using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Admin.Interfaces
{
    public interface IThongKe
    {
        Task<SoDonModel> GetSoDonHangTheoNgayThangNam(DateTime date);
        Task<int> GetSoDonHangTheoThang();
        Task<decimal> GetLoiNhuanTheoThang();

        IEnumerable<DoanhThuModel> GetDoanhThuLoiNhuan(string fromDate, string toDate);
        int[] GetThongKeSoDon(string fromDate, string toDate);

        List<SanPhamModel> GetSanPhamListWithSoLuongBanTrongThang();
        //Task<int> GetKhachHangMoiTheoThang();
    }
}
