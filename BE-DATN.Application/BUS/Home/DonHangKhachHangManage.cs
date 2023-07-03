using BE_DATN.Application.BUS.Home.DTO;
using BE_DATN.Application.BUS.Home.Interfaces;
using BE_DATN.Application.Common;
using BE_DATN.Data.EF;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BE_DATN.Application.BUS.Home
{
    public class DonHangKhachHangManage:IDonHangKhachHang
    {
        private readonly BEDATNDbContext _context;
        public DonHangKhachHangManage(BEDATNDbContext context)
        {
            _context = context;
        }

        public async Task<List<CTDHModel>> GetChiTietDonHangByIdDonHang(int Id)
        {
            var result = from ct in _context.ChiTietDonHangs
                         join dh in _context.DonHangs on ct.IdDonHang equals dh.IdDonHang
                         join msp in _context.MauSanPhams on ct.IdMauSanPham equals msp.IdMauSanPham
                         join ssp in _context.SizeSanPhams on ct.IdSizeSanPham equals ssp.IdSizeSanPham
                         join sp in _context.SanPhams on ct.IdSanPham equals sp.IdSanPham
                         select new
                         {
                             IdChiTietDonHang = ct.IdChiTietDonHang,
                             TenSanPham = sp.TenSanPham,
                             IdDonHang = ct.IdDonHang,
                             IdSanPham = ct.IdSanPham,
                             IdMauSanPham = ct.IdMauSanPham,
                             MaMau = msp.MaMau,
                             IdSizeSanPham = ct.IdSizeSanPham,
                             Size = ssp.Size,
                             AnhSanPham = msp.AnhSanPham,
                             SoLuong = ct.SoLuong,
                             GiaMua = ct.GiaMua
                         };


            var kq = await result.Where(x => x.IdDonHang == Id).Select(x => new CTDHModel()
            {
                IdDonHang = x.IdDonHang,
                IdChiTietDonHang = x.IdChiTietDonHang,
                IdSanPham = x.IdSanPham,
                TenSanPham = x.TenSanPham,
                IdMauSanPham = x.IdMauSanPham,
                MaMau = x.MaMau,
                IdSizeSanPham = x.IdSizeSanPham,
                Size = x.Size,
                AnhSanPham = x.AnhSanPham,
                SoLuong = x.SoLuong,
                GiaMua = x.GiaMua,
            }).ToListAsync();
            return kq;
        }

        public async Task<List<DonHangMoMoModel>> GetDonHangMoMo([FromBody] DonHangMoMoModel model)
        {
            List<DonHangMoMoModel> donHangMoMoList = new List<DonHangMoMoModel>();

            foreach (var item in model.ctdh)
            {
                int idSanPham = item.IdSanPham;
                var sanPham = await _context.SanPhams.FirstOrDefaultAsync(s => s.IdSanPham == idSanPham);

                

                //Tạo một mục DonHangMoMoModel mới với các thuộc tính được cập nhật và thêm nó vào danh sách
                var donHangMoMoItem = new DonHangMoMoModel()
                {
                    IdSanPham = item.IdSanPham,
                    IdMauSanPham=item.IdMauSanPham,
                    IdSizeSanPham=item.IdSizeSanPham,
                    TenSanPham = sanPham.TenSanPham,
                    SoLuong = item.SoLuong,
                    GiaMua= item.GiaMua,
                    
                };

                donHangMoMoList.Add(donHangMoMoItem);
            }

            return donHangMoMoList;
        }

        public async Task<int> HuyDon(DonHangModel th)
        {
            var donhang = await _context.DonHangs.FindAsync(th.IdDonHang);

            donhang.TrangThaiDonHang = th.TrangThaiDonHang;

            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<PagedResult<DonHangModel>> SearchDonHangPaging([FromBody] Dictionary<string, object> formData)
        {
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            int idKhachHang = int.Parse(formData["idkhachhang"].ToString());


            var result = from dh in _context.DonHangs
                         where dh.IdKhachHang == idKhachHang
                         select new
                         {
                             IdDonHang = dh.IdDonHang,
                             IdKhachHang = dh.IdKhachHang,
                             TenKhachHang = dh.TenKhachHang,
                             GhiChu = dh.GhiChu,
                             SDT = dh.SDT,
                             DiaChi = dh.DiaChi,
                             TrangThaiDonHang = dh.TrangThaiDonHang,
                             DiaChiGiaoHang = dh.DiaChiGiaoHang,
                             NgayDat = dh.NgayDat,
                             TrangThaiThanhToan = dh.TrangThaiThanhToan,
                             TongTien = dh.TongTien,
                             MaKhuyenMai = dh.MaKhuyenMai,
                             PhuongThucThanhToan = dh.PhuongThucThanhToan
                         };

        
            

            // Lấy tổng số item thỏa mãn
            int totalItems = await result.CountAsync();

            var kq = await result.Skip(pageSize * (page - 1)).Take(pageSize).Select(x => new DonHangModel()
            {
                IdDonHang = x.IdDonHang,
                IdKhachHang = x.IdKhachHang,
                TenKhachHang = x.TenKhachHang,
                GhiChu = x.GhiChu,
                SDT = x.SDT,
                DiaChi = x.DiaChi,
                TrangThaiDonHang = x.TrangThaiDonHang,
                DiaChiGiaoHang = x.DiaChiGiaoHang,
                NgayDat = x.NgayDat,
                TrangThaiThanhToan = x.TrangThaiThanhToan,
                TongTien = x.TongTien,
                MaKhuyenMai = x.MaKhuyenMai,
                PhuongThucThanhToan = x.PhuongThucThanhToan

            }).ToListAsync();

            var pageResult = new PagedResult<DonHangModel>()
            {
                totalItem = totalItems,
                page = page,
                pageSize = pageSize,
                data = kq

            };

            return pageResult;
        }
    }
}
