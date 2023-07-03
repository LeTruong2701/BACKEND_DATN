using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.BUS.Admin.Interfaces;
using BE_DATN.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using BE_DATN.Data.Entities;

namespace BE_DATN.Application.BUS.Admin
{
    public class ManageThongKe : IThongKe
    {
        private readonly BEDATNDbContext _context;
        public ManageThongKe(BEDATNDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DoanhThuModel> GetDoanhThuLoiNhuan(string fromDate, string toDate)
        {

            DateTime start, end;
            if (!DateTime.TryParseExact(fromDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"), DateTimeStyles.None, out start))
            {
                start = DateTime.Now.Date.AddDays(-15);
            }
            if (!DateTime.TryParseExact(toDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"), DateTimeStyles.None, out end))
            {
                end = DateTime.Now;
            }

            var query = from dh in _context.DonHangs
                        join ctdh in _context.ChiTietDonHangs on dh.IdDonHang equals ctdh.IdDonHang
                        join msp in _context.MauSanPhams on ctdh.IdMauSanPham equals msp.IdMauSanPham
                        where dh.NgayNhanHang.HasValue
                        && (dh.NgayNhanHang >= start && dh.NgayNhanHang <= end)
                        select new
                        {
                            NgayNhanHang = dh.NgayNhanHang,
                            SoLuong = ctdh.SoLuong,
                            GiaNhap = msp.GiaNhap,
                            GiaBan = msp.GiaBan,
                            TongTien=dh.TongTien

                        };

            var result = query.GroupBy(x => x.NgayNhanHang.Value.Date)
                 .Select(x => new
                 {
                     Date = x.Key,
                     TotalBuy = x.Sum(x => x.GiaNhap * x.SoLuong),
                     TotalSell = x.Sum(x => x.GiaBan * x.SoLuong),
                 }).Select(r => new DoanhThuModel()
                 {
                     Date = r.Date,
                     LoiNhuan = r.TotalSell - r.TotalBuy,
                     DoanhThu = r.TotalSell
                 });
            return result.ToList();

        }


        public int[] GetThongKeSoDon(string fromDate, string toDate)
        {
            


            DateTime start, end;
            if (!DateTime.TryParseExact(fromDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"), DateTimeStyles.None, out start))
            {
                start = DateTime.Now.Date.AddDays(-30);
            }
            if (!DateTime.TryParseExact(toDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"), DateTimeStyles.None, out end))
            {
                end = DateTime.Now;
            }

            var query = from dh in _context.DonHangs
                        where dh.NgayDat.Month== DateTime.Now.Month
                        select new
                        {
                            NgayNhanHang = dh.NgayNhanHang,
                            TrangThaiDonHang = dh.TrangThaiDonHang,
                        };

            var result = query.GroupBy(q => q.TrangThaiDonHang)
                              .Select(g => new
                              {
                                  TrangThaiDonHang = g.Key,
                                  Count = g.Count()
                              })
                              .ToDictionary(r => r.TrangThaiDonHang, r => r.Count);

            int[] thongKeSoDon = new int[3];
            thongKeSoDon[0] = result.ContainsKey("Giao hàng thành công") ? result["Giao hàng thành công"] : 0;
            thongKeSoDon[1] = result.ContainsKey("Đang vận chuyển") ? result["Đang vận chuyển"] : 0;
            thongKeSoDon[2] = result.ContainsKey("Đã hủy") ? result["Đã hủy"] : 0;

            return thongKeSoDon;
        }

        //public Task<int> GetKhachHangMoiTheoThang()
        //{
        //    var sokhachhangmoi = from th in _context.KhachHangs
        //                            where th.NgayTao.Month == DateTime.Now.Month
        //                            select new { th.IdDonHang, th.TongTien, th.NgayDat, th.NgayNhanHang };
        //}

        public async Task<decimal> GetLoiNhuanTheoThang()
        {
            var sodonhangtheongay = from th in _context.DonHangs
                                    where th.NgayNhanHang !=null && th.NgayNhanHang.Value.Month == DateTime.Now.Month
                                    select new { th.IdDonHang, th.TongTien, th.NgayDat, th.NgayNhanHang };
            var result = sodonhangtheongay.ToList();

            decimal sum = 0;
            foreach (var th in result)
            {
                sum =sum +th.TongTien;
            }
            return sum;
        }

        public Task<SoDonModel> GetSoDonHangTheoNgayThangNam(DateTime date)
        {
            var sodonhangtheongay = from th in _context.DonHangs
                                    where th.NgayDat == date
                                    select new { th.IdDonHang, th.TongTien, th.NgayDat,th.NgayNhanHang };

            var soDonTheoNgay = sodonhangtheongay.Count();

            var sodonhangtheothang = from th in _context.DonHangs
                                    where th.NgayDat.Month == date.Month
                                    select new { th.IdDonHang, th.TongTien, th.NgayDat, th.NgayNhanHang };

            var soDonTheoThang = sodonhangtheothang.Count();

            var sodonhangtheonam = from th in _context.DonHangs
                                     where th.NgayDat.Year == date.Year
                                     select new { th.IdDonHang, th.TongTien, th.NgayDat, th.NgayNhanHang };

            var soDonTheoNam = sodonhangtheonam.Count();

            var soDonTheoNgayThangNam = new SoDonModel
            {

                SoDonTheoNgay = soDonTheoNgay,
                SoDonTheoThang = soDonTheoThang,
                SoDonTheoNam = soDonTheoNam
            };


            return Task.FromResult(soDonTheoNgayThangNam);
        }

        public async Task<int> GetSoDonHangTheoThang()
        {
            var sodonhangtheongay = from th in _context.DonHangs
                                    where th.NgayDat.Month == DateTime.Now.Month
                                    select new { th.IdDonHang, th.TongTien, th.NgayDat, th.NgayNhanHang };
            var sodon = sodonhangtheongay.Count();
            return sodon;
        }

        public List<SanPhamModel> GetSanPhamListWithSoLuongBanTrongThang()
        {
            var sanPhams = from sp in _context.SanPhams
                           let sumSoLuongBan = (
                               from ctdh in _context.ChiTietDonHangs
                               join dh in _context.DonHangs on ctdh.IdDonHang equals dh.IdDonHang
                               where sp.IdSanPham == ctdh.IdSanPham && dh.NgayNhanHang != null && dh.NgayNhanHang.Value.Month == DateTime.Now.Month
                               select ctdh.SoLuong
                           ).Sum()
                           where sumSoLuongBan > 0
                           select new SanPhamModel
                           {
                               IdSanPham = sp.IdSanPham,
                               IdDanhMuc = sp.IdDanhMuc,
                               TenSanPham = sp.TenSanPham,
                               MoTaSanPham = sp.MoTaSanPham,
                               AnhSanPham = sp.AnhSanPham,
                               XuatXu = sp.XuatXu,
                               ChatLieu = sp.ChatLieu,
                               IdThuongHieu = sp.IdThuongHieu,
                               NgayTao = sp.NgayTao,
                               TrangThai = sp.TrangThai,
                               SoLuongBan = sumSoLuongBan
                           };

            return sanPhams.ToList();
        }
    }
}
