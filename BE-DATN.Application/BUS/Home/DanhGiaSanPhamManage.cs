using BE_DATN.Application.BUS.Home.DTO;
using BE_DATN.Application.BUS.Home.Interfaces;
using BE_DATN.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BE_DATN.Data.Entities;

namespace BE_DATN.Application.BUS.Home
{
    public class DanhGiaSanPhamManage : IDanhGiaSanPham
    {
        private readonly BEDATNDbContext _context;
        public DanhGiaSanPhamManage(BEDATNDbContext context)
        {
            _context = context;
        }


        public async Task<List<SanPhamModel>> GetListSPKhachHangDaMua(int IdKH)
        {
           
                var donhang = from dh in _context.DonHangs
                          
                           where dh.IdKhachHang == IdKH  && dh.TrangThaiThanhToan==1
                           select new
                           {
                              IdDonHang=dh.IdDonHang
                           };
            var resultdonhang = donhang.ToList();

            List<SanPhamModel> listsp=new List<SanPhamModel>();
            foreach (var item in resultdonhang)
            {
                var idDonHang=item.IdDonHang;
                var list = from ct in _context.ChiTietDonHangs
                             where ct.IdDonHang == idDonHang
                             select new SanPhamModel()
                             {
                                 IdSanPham = ct.IdSanPham
                             };

                listsp.AddRange(await list.ToListAsync());
            }

            return listsp;
        }

        public async Task<List<DanhGiaSanPhamModel>> GetListDanhGiaSanPham(int IdSanPham)
        {
            var data = from sp in _context.DanhGiaSanPhams
                       join kh in _context.KhachHangs on sp.IdKhachHang equals kh.IdKhachHang
                       where sp.IdSanPham == IdSanPham
                       orderby sp.NgayDanhGia descending
                       select new DanhGiaSanPhamModel
                       {
                           IdDanhGia = sp.IdDanhGia,
                           IdSanPham=sp.IdSanPham,
                           IdKhachHang=sp.IdKhachHang,
                           NoiDungDanhGia=sp.NoiDungDanhGia,
                           NgayDanhGia= sp.NgayDanhGia,
                           TenKhachHang=kh.TenKhachHang,
                           AnhDaiDien=kh.AnhDaiDien
                           
                       };
            var result = await data.ToListAsync();
            return result;
        }


        public async Task<int> Create(DanhGiaSanPhamModel dgsp)
        {
            _context.DanhGiaSanPhams.Add(dgsp.danhgia);
            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<int> Delete(int Id)
        {
            var danhgia = await _context.DanhGiaSanPhams.FindAsync(Id);

            _context.DanhGiaSanPhams.Remove(danhgia);
            await _context.SaveChangesAsync();
            return 1;
        }

        

        public async Task<int> Update(DanhGiaSanPhamModel dgsp)
        {
            var danhgia = await _context.DanhGiaSanPhams.FindAsync(dgsp.danhgia.IdDanhGia);

            danhgia.NoiDungDanhGia = dgsp.danhgia.NoiDungDanhGia;
           

            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<DanhGiaSanPham> GetById(int Id)
        {
            var danhgia = await _context.DanhGiaSanPhams.FindAsync(Id);

            return danhgia;
        }
    }
}
