using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.Common;
using BE_DATN.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Admin.Interfaces
{
    public interface IManageHoaDonNhap
    {
        Task<PagedResult<HoaDonNhapModel>> SearchHoaDonNhapPaging([FromBody] Dictionary<string, object> formData);
        Task<HoaDonNhapModel> GetById(int Id);
        Task<List<ChiTietHoaDonNhapModel>> GetChiTietHoaDonNhap(int IdHoaDonNhap);

        Task<int> CreateHoaDonNhap([FromBody] DataNhapHoaDonNhap model);
        Task<int> Update(HoaDonNhapModel sp);

        Task<List<MauSanPhamModel>> GetListMauSanPhamChuaThanhToan(int IdSanPham);

        //Task<List<DanhSachSanPhamNhap>> GetListSanPhamNhap(int IdSanPham);

        Task<List<ChiTietHoaDonNhapModel>> GetListSizeMauSanPham(int IdMauSanPham);

        //Task<List<ChiTietHoaDonNhap>> GetListSizeMauSanPham2(int IdMauSanPham);

    }
}
