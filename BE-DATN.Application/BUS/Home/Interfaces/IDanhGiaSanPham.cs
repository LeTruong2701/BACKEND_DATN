using BE_DATN.Application.BUS.Home.DTO;
using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Home.Interfaces
{
    public interface IDanhGiaSanPham
    {
        Task<List<SanPhamModel>> GetListSPKhachHangDaMua(int IdKH);
        Task<List<DanhGiaSanPhamModel>> GetListDanhGiaSanPham(int IdSanPham);

        Task<DanhGiaSanPham> GetById(int Id);
        Task<int> Create(DanhGiaSanPhamModel dgsp);
        Task<int> Update(DanhGiaSanPhamModel dgsp);
        Task<int> Delete(int Id);
    }
}
