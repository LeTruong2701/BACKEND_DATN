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
    public interface IManageSanPham
    {
        Task<PagedResult<SanPhamModel>> SearchSanPhamPaging([FromBody] Dictionary<string, object> formData);
        
        Task<SanPhamModel> GetById(int Id);
        Task<int> Create(SanPhamModel sp);
        Task<int> Update(SanPhamModel sp);
        Task<int> Delete(int Id);

        //Nhập màu sản phẩm

        Task<PagedResult<MauSanPham>> SearchMauSanPhamPaging([FromBody] Dictionary<string, object> formData);

        Task<PagedResult<MauSanPhamModel>> GetListMauSanPhamById([FromBody] Dictionary<string, object> formData);//trong này truyền idsanpham
        Task<MauSanPham> GetMauSanPhamById(int Id);
        Task<MauSanPhamModel> CreateMauSanPham(MauSanPhamModel msp);
        Task<int> UpdateMauSanPham(MauSanPhamModel msp);
        Task<int> DeleteMauSanPham(int Id);

        //Size sản phẩm

        Task<Result<SizeSanPhamModel>> GetListSizeSanPhamByIdMauSanPham([FromBody] Dictionary<string, object> formData);


    }
}
