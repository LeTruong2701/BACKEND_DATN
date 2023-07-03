using BE_DATN.Application.BUS.Home.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Home.Interfaces
{
    public interface ISanPham
    {
        //LẤY THÔNG TIN SẢN PHẨM CÓ GIÁ MIN-MAX
        Task<SanPhamModel> GetSanPhamByIdSanPham(int Id);
        //lấy danh sách sản phẩm cùng loại
        Task<List<SanPhamModel>> GetSanPhamCungLoai(int Id);


        //lấy màu sản phẩm đầu tiên để có idmausanpham lấy chi tiết thông tin màu sản phẩm
        Task<SanPhamTheoMauModel> GetSanPhamMauByIdSanPham(int Id);

        //lấy thông tin sản phẩm với màu đầu tiên, idmausanpham
        Task<SanPhamTheoMauModel> GetSanPhamTheoMauByIdMauSanPham(int Id);
        //lấy list size sản phẩm theo id màu sản phẩm
        Task<List<SizeSanPhamModel>> GetListSizeByIdMauSanPham(int Id);

        //lấy size đầu tiên
        Task<SizeSanPhamModel> GetSizeSanPhamDauTienByIdMauSanPham(int Id);
        //lấy thông tin size theo id sizesanpham
        Task<SizeSanPhamModel> GetSizeSanPhamByIdSizeSanPham(int Id);
        
        //lấy list màu  
        Task<List<SanPhamTheoMauModel>> GetListMauCuaSanPham(int Id);



    }
}
